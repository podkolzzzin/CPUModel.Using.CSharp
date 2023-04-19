using DevJungles.Assembler;

namespace DevJungles.VirtualMachine;

public class OperationSystem
{
  public Process CreateProcess(Assembly assembly)
  {
    return new Process(assembly);
  }
}

public class MemoryManager { }

public class Process
{
  public Thread MainThread { get; set; }
  public Process(Assembly assembly)
  {
    MainThread = new Thread(GetEntryPoint(assembly));
  }

  public void Start() => MainThread.Start();
  
  private AsmCommand[] GetEntryPoint(Assembly assembly)
  {
    return assembly.Commands;
  }
}

public class Thread
{
  private readonly AsmCommand[] _commands;
  public Thread(AsmCommand[] commands)
  {
    _commands = commands;

  }
  public void Start()
  {
    var ctx = new ExecutionContext() 
    {
      Registers = new int[2], 
      Stack = new Stack(), 
      CurrentCommandIndex = 0
    };

    while (ctx.CurrentCommandIndex < _commands.Length)
    {
      Console.Write($"[{ctx.CurrentCommandIndex.ToString().PadLeft(3, '0')}]");
      var currentCommand = _commands[ctx.CurrentCommandIndex];
      currentCommand.Dump();
      Console.CursorLeft = 60;
      currentCommand.Execute(ref ctx);
      Console.CursorLeft = 20;
      ctx.Registers.Dump();
      Console.CursorLeft = 30;
      ctx.Stack.Dump();
      Console.WriteLine();
    }
  }
}

public class Assembly
{
  public AsmCommand[] Commands { get; }

  public Assembly(AsmCommand[] commands)
  {
    Commands = commands;
  }
}