namespace DevJunglesAssembler;

public class ExecutionContext
{
  public ExecutionContext(int[] registers, Memory<int> stack, int currentCommandIndex)
  {
    Registers = registers;
    CurrentCommandIndex = currentCommandIndex;
    Stack = new(stack);
  }
  public int[] Registers { get; }
  public Stack Stack { get; }
  public int CurrentCommandIndex { get; set; }
}


public enum Commands : byte
{
  Add,
  Sub,
  Lt,
  Gt,
  Eq,
  Jmp,
  JmpTo,
  Read, 
  Write,
  Push,
  Pop,
  Put,
  Print,
}