namespace DevJunglesAssembler;

public class DeclareCommand : ICommand
{
  public void Execute(ExecutionContext executionContext)
  {
    executionContext.Stack.Push(0);
    executionContext.CurrentCommandIndex++;
  }
  public void Dump(ExecutionContext context)
  {
    Console.Write("push 0");
  }
}