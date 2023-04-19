namespace DevJungles.Assembler;

public enum Commands : byte
{
  Add,
  Sub,
  Lt,
  Gt,
  Jmp,
  Push,
  Read, 
  Write,
  Put,
  Print,
}

public struct AsmCommand // 12 bytes like Arm x32
{
  public Commands Command;
  public byte Register1;
  public byte Register2;
  public byte Register3;

  public int LeftOperand;
  public int RightOperand;

  public string Variable; // TEMP SOLUTION. Will be removed
}