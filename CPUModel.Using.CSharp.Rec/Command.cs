public class Command : ICommand
{
  private readonly AsmCommand _cmd;
  private Command(AsmCommand cmd)
  {
    _cmd = cmd;
  }

  public AsmCommand ToAsmCommand() => _cmd;

  public static ICommand Push() => new Command(new() { Command = Commands.Push});

  public static ICommand Put(byte regNumber, int constant) => new Command(new () { Command = Commands.Put, Register1 = regNumber, LeftOperand = constant });

  public static ICommand Print(string text) => new Command(new () { Command = Commands.Print });
  public static ICommand Add(byte regNumber) => new Command(new () { Command = Commands.Add, Register1 = regNumber });
  public static ICommand Sub(byte regNumber) => new Command(new () { Command = Commands.Sub, Register1 = regNumber });
  public static ICommand Lt(byte regNumber) => new Command(new () { Command = Commands.Lt, Register1 = regNumber });
  public static ICommand Gt(byte regNumber) => new Command(new () { Command = Commands.Gt, Register1 = regNumber });
  
  public static ICommand Jmp() => new Command(new () { Command = Commands.Jmp });
    
  public static ICommand Read(byte regNumber, int stackAddress) => new Command(new () { Command = Commands.Read, Register1 = regNumber, LeftOperand = stackAddress });
  public static ICommand Write(byte regNumber, int stackAddress) => new Command(new () { Command = Commands.Write, Register1 = regNumber, LeftOperand = stackAddress });
}