
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DevJunglesAssembler;
using ExecutionContext = DevJunglesAssembler.ExecutionContext;
using static DevJunglesAssembler.Commands;
namespace DevJunglesVirtualMachine;

public class MemoryManager
{
  private int _offset;
  private readonly byte[] _memory;
  public MemoryManager(byte[] memory)
  {
    _memory = memory;
  }
    
  public int Malloc(int amountOfBytes)
  {
    return FindFree(amountOfBytes);
  }
    
  public void Free(int address)
  {
    // TODO: Make more complex logic of memory management
    _offset = address;
  }

  private int FindFree(int amountOfBytes)
  {
    int result = _offset;
    _offset += amountOfBytes;
    return result;
  }
  public void Copy(int address, byte[] assemblyData)
  {
    assemblyData.CopyTo(_memory, address);
  }
}

public class OperationSystem
{
  private readonly List<Process> _processes = new();
  private readonly byte[] _memory = new byte[10 * 1024 * 1024];
  private readonly MemoryManager _memoryManager;

  public OperationSystem()
  {
    _memoryManager = new(_memory);
  }
  
  public Process CreateProcess(Assembly assembly)
  {
    var address = _memoryManager.Malloc(assembly.Size);
    _memoryManager.Copy(address, assembly.Data);
    var result = new Process(_memoryManager, _memory, address, assembly.Size);
    _processes.Add(result);
    return result;
  }
}

public class Process
{
  private readonly MemoryManager _manager;
  private readonly byte[] _memory;
  private readonly int _address;
  private readonly int _size;

  public class Thread
  {
    private readonly Process _parent;
    public Thread(Process parent)
    {
      _parent = parent;
    }
    
    public void Execute(Span<AsmCommand> commands)
    {
      int address = _parent.Malloc(256);
      var stackByteMemory = _parent._memory.AsMemory(address, 256);
      var stackMemory = Unsafe.As<Memory<byte>, Memory<int>>(ref stackByteMemory).Slice(0, 256 / 4);
      
      var context = new ExecutionContext(new int[2], stackMemory, 0);

      while (context.CurrentCommandIndex < commands.Length)
      {
        if (context.CurrentCommandIndex == -1)
          break;
        
        Console.Write($"[{context.CurrentCommandIndex.ToString().PadLeft(3, '0')}]");
        var currentCommand = commands[context.CurrentCommandIndex];
        currentCommand.Dump(context);
        Console.CursorLeft = 60;
    
        currentCommand.Execute(context);
        Console.CursorLeft = 20;
        context.Registers.Dump();
        Console.CursorLeft = 30;
        context.Stack.Dump();
        Console.WriteLine();
      }
    }
  }

  public Thread MainThread { get; }
    
  public Process(MemoryManager manager, byte[] memory, int address, int size)
  {
    _manager = manager;
    _memory = memory;
    _address = address;
    _size = size;

    MainThread = new Thread(this);
  }

  public int Malloc(int amountOfBytes) => _manager.Malloc(amountOfBytes);
  public void Start()
  {
    var commands = GetEntryPoint(_memory, _address, _size);
    MainThread.Execute(commands);
  }
  
  private Span<AsmCommand> GetEntryPoint(byte[] memory, int address, int size)
  {
    // TODO: Logic for finding the entry point will be implemented later
    return MemoryMarshal.Cast<byte, AsmCommand>(memory.AsSpan(address, size));
  }
}

public static class AsmCommandExtensions 
{
  public static void Dump(this AsmCommand command, ExecutionContext context)
  {
      var dump = command.Command switch {
        Add => $"add {context.Registers[0]} {context.Registers[1]} r{command.Register1}",
        Sub => $"sub {context.Registers[0]} {context.Registers[1]} r{command.Register1}",
        Lt => $"lt {context.Registers[0]} {context.Registers[1]} r{command.Register1}",
        Gt => $"gt {context.Registers[0]} {context.Registers[1]} r{command.Register1}",

        Jmp => $"jmp {context.Registers[0]}",
        JmpTo => $"jmpt r{command.Register1} {context.Registers[command.Register1]}",
      
        Read => $"read r{command.Register1} {command.LeftOperand}",
        Write => $"write r{command.Register1} {command.LeftOperand}",
        Put => $"put r{command.Register1} {command.LeftOperand}",
        Push => $"push {command.Register1}",
        Pop => $"pop {command.LeftOperand}",
        Print => "print",
        _ => throw new NotImplementedException(),
      };
      Console.Write(dump);
  }
  
  public static void Execute(this AsmCommand cmd, ExecutionContext context)
  {
    switch (cmd.Command)
    {
      case Add or Sub or Lt or Gt:
        var left = context.Registers[0];
        var right = context.Registers[1];
        context.Registers[cmd.Register1] = cmd.Command switch {
          Add => left + right,
          Sub => left - right,
          Gt => left > right ? 1 : 0,
          Lt => left < right ? 1 : 0,
          _ => throw new InvalidOperationException(cmd.Command.ToString()),
        };
        break;
      case Put:
        context.Registers[cmd.Register1] = cmd.LeftOperand;
        break;
      case Write:
        context.Stack.Set(cmd.LeftOperand, context.Registers[cmd.Register1]);
        break;
      case Read:
        context.Registers[cmd.Register1] = context.Stack.Get(cmd.LeftOperand);
        break;
      case Push:
        context.Stack.Push(context.Registers[cmd.Register1]);
        break;
      case Pop:
        context.Stack.Pop(cmd.LeftOperand);
        break;
      case Print:
        Console.Write("PRINT");
        break;
      //throw new NotImplementedException(); // TODO: Implement
      case Jmp:
        context.CurrentCommandIndex += context.Registers[0];
        break;
      case JmpTo:
        context.CurrentCommandIndex = context.Registers[cmd.Register1];
        break;
      default:
        throw new InvalidOperationException();
    }
    if (cmd.Command is not Jmp and not JmpTo) context.CurrentCommandIndex++;
  }
}


[StructLayout(LayoutKind.Explicit, Size = 12)]
public struct AsmCommand
{
  [FieldOffset(0)]
  public Commands Command;

  [FieldOffset(1)]
  public byte Register1;

  [FieldOffset(2)]
  public byte Register2;
  
  [FieldOffset(3)]
  public byte Register3;

  [FieldOffset(4)]
  public int LeftOperand;
  [FieldOffset(8)]
  public int RightOperand;
}

public static class CommandBuilder 
{
  public static AsmCommand Put(byte regNumber, int constant) => new () { Command = Commands.Put, Register1 = regNumber, LeftOperand = constant };
  
  public static AsmCommand Push(byte regNumber) =>new () { Command = Commands.Push, Register1 = regNumber };
  public static AsmCommand Pop(int count) =>new () { Command = Commands.Pop, LeftOperand = count };
  
  public static AsmCommand Print() =>new () { Command = Commands.Print };
  public static AsmCommand Add(byte regNumber) => new () { Command = Commands.Add, Register1 = regNumber };
  public static AsmCommand Sub(byte regNumber) => new () { Command = Commands.Sub, Register1 = regNumber };
  public static AsmCommand Lt(byte regNumber) => new () { Command = Commands.Lt, Register1 = regNumber };
  public static AsmCommand Gt(byte regNumber) => new () { Command = Commands.Gt, Register1 = regNumber };
  
  public static AsmCommand Jmp() => new () { Command = Commands.Jmp };
  public static AsmCommand JmpTo(byte regNumber) => new () { Command = Commands.JmpTo, Register1 = regNumber };
  
  public static AsmCommand Read(byte regNumber, int stackAddress) => new () { Command = Commands.Read, Register1 = regNumber, LeftOperand = stackAddress };
  public static AsmCommand Write(byte regNumber, int stackAddress) =>new () { Command = Commands.Write, Register1 = regNumber, LeftOperand = stackAddress };
  
  public static AsmCommand Bin(Commands command, byte regNumber) => new () { Command = command, Register1 = regNumber };
}


public class Assembly
{
  private readonly byte[] _bytes;
  
  public int Size => _bytes.Length;
  public byte[] Data => _bytes;

  public Assembly(string file)
  {
    _bytes = File.ReadAllBytes(file);
  }
}