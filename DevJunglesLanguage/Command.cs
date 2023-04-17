using DevJunglesAssembler;
using DevJunglesVirtualMachine;

namespace DevJunglesLanguage;

public class Command : ISimpleCommand
{
  private readonly AsmCommand _cmd;
  private Command(AsmCommand cmd)
  {
    _cmd = cmd;
  }

  public AsmCommand AsBytes() => _cmd;

  public static ISimpleCommand Put(byte regNumber, int constant) => new Command(new () { Command = Commands.Put, Register1 = regNumber, LeftOperand = constant });
  
  public static ISimpleCommand Push(byte regNumber) => new Command(new () { Command = Commands.Push, Register1 = regNumber });
  public static ISimpleCommand Pop(int count) => new Command(new () { Command = Commands.Pop, LeftOperand = count });

  public static ISimpleCommand Print(string text) => new Command(new () { Command = Commands.Print });
  public static ISimpleCommand Add(byte regNumber) => new Command(new () { Command = Commands.Add, Register1 = regNumber });
  public static ISimpleCommand Sub(byte regNumber) => new Command(new () { Command = Commands.Sub, Register1 = regNumber });
  public static ISimpleCommand Lt(byte regNumber) => new Command(new () { Command = Commands.Lt, Register1 = regNumber });
  public static ISimpleCommand Gt(byte regNumber) => new Command(new () { Command = Commands.Gt, Register1 = regNumber });
  
  public static ISimpleCommand Jmp() => new Command(new () { Command = Commands.Jmp });
  public static ISimpleCommand JmpTo(byte regNumber) => new Command(new () { Command = Commands.JmpTo, Register1 = regNumber });
  
  public static ISimpleCommand Read(byte regNumber, int stackAddress) => new Command(new () { Command = Commands.Read, Register1 = regNumber, LeftOperand = stackAddress });
  public static ISimpleCommand Write(byte regNumber, int stackAddress) => new Command(new () { Command = Commands.Write, Register1 = regNumber, LeftOperand = stackAddress });
}