using DevJunglesAssembler;

namespace DevJunglesVirtualMachine;

public class OperationSystem
{
  class MemoryManager {}
    
  public Process CreateProcess(Assembly assembly)
  {
    return new Process(assembly);
  }
}

public class Process
{
  public class Thread
  {
    private readonly ICommand[] _commands;
    public Thread(ICommand[] commands)
    {
      _commands = commands;
    }
    
    public void Execute()
    {
      var context = new DevJunglesAssembler.ExecutionContext(new int[2], 0);

      while (context.CurrentCommandIndex < _commands.Length)
      {
        Console.Write($"[{context.CurrentCommandIndex.ToString().PadLeft(3, '0')}]");
        var currentCommand = _commands[context.CurrentCommandIndex];
        currentCommand.Dump(context);
        Console.CursorLeft = 60;
    
        currentCommand.Execute(context);
        Console.CursorLeft = 20;
        context.Registers.Dump();
        Console.CursorLeft = 30;
        Console.WriteLine();
      }
    }
  }

  public Thread MainThread { get; }
    
  public Process(Assembly assembly)
  {
    var commands = GetEntryPoint(assembly);
    MainThread = new Thread(commands);
  }

  public void Start()
  {
    MainThread.Execute();
  }
  
  private ICommand[] GetEntryPoint(Assembly assembly)
  {
    // TODO: Logic for finding the entry point will be implemented later
    return assembly.Commands;
  }
}

public class Assembly
{
  public ICommand[] Commands { get; }

  public Assembly(ICommand[] commands)
  {
    Commands = commands;
  }
}