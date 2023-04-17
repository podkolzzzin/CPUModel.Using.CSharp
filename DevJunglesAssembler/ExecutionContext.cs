namespace DevJunglesAssembler;

public ref struct ExecutionContext
{
  public ExecutionContext(int[] registers, Span<int> stack, int currentCommandIndex)
  {
    Registers = registers;
    CurrentCommandIndex = currentCommandIndex;
    Stack = new(stack);
  }
  public int[] Registers { get; }
  public Stack Stack;
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