namespace DevJunglesAssembler;

public class ExecutionContext
{
  public ExecutionContext(int[] registers, int currentCommandIndex)
  {
    Registers = registers;
    CurrentCommandIndex = currentCommandIndex;
  }
  public int[] Registers { get; }
  public Stack Stack { get; } = new();
  public int CurrentCommandIndex { get; set; }
}