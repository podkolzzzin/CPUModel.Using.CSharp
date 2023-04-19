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
  private object GetEntryPoint(Assembly assembly)
  {
    return new object();
  }
}

public class Thread
{
  public Thread(object entryPoint)
  {
    
  }
}

public class Compiler
{
  public Assembly Compile(Source source)
  {
    return new Assembly();
  }
}

public class Assembly {}

public class Source {}