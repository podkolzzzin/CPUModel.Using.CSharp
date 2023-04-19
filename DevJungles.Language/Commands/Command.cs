using DevJungles.Assembler;

namespace DevJungles.Language.Commands;

public interface ICommand
{
  
}

public interface ISimpleCommand : ICommand
{
  AsmCommand ToAsmCommand();
}

public interface IHighLevelCommand : ICommand
{
  IEnumerable<AsmCommand> Compile();
}


public class Command : ISimpleCommand
{
  private readonly AsmCommand _cmd;
  private Command(AsmCommand cmd)
  {
    _cmd = cmd;
  }

  public AsmCommand ToAsmCommand() => _cmd;

  public static ISimpleCommand Push() => new Command(new() { Command = Assembler.Commands.Push});

  public static ISimpleCommand Put(byte regNumber, int constant) => new Command(new () { Command = Assembler.Commands.Put, Register1 = regNumber, LeftOperand = constant });

  public static ISimpleCommand Print(string text) => new Command(new () { Command = Assembler.Commands.Print });
  public static ISimpleCommand Add(byte regNumber) => new Command(new () { Command = Assembler.Commands.Add, Register1 = regNumber });
  public static ISimpleCommand Sub(byte regNumber) => new Command(new () { Command = Assembler.Commands.Sub, Register1 = regNumber });
  public static ISimpleCommand Lt(byte regNumber) => new Command(new () { Command = Assembler.Commands.Lt, Register1 = regNumber });
  public static ISimpleCommand Gt(byte regNumber) => new Command(new () { Command = Assembler.Commands.Gt, Register1 = regNumber });
  
  public static ISimpleCommand Jmp() => new Command(new () { Command = Assembler.Commands.Jmp });
    
  public static ISimpleCommand Read(byte regNumber, int stackAddress) => new Command(new () { Command = Assembler.Commands.Read, Register1 = regNumber, LeftOperand = stackAddress });
  public static ISimpleCommand Write(byte regNumber, int stackAddress) => new Command(new () { Command = Assembler.Commands.Write, Register1 = regNumber, LeftOperand = stackAddress });
}