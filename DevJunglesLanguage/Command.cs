using DevJunglesVirtualMachine;

namespace DevJunglesLanguage;

public class Command : ICommand
{
  private readonly AsmCommand _cmd;
  private Command(AsmCommand cmd)
  {
    _cmd = cmd;
  }

  public AsmCommand AsBytes() => _cmd;

  public static ICommand Put(byte regNumber, int constant) => new Command(CommandBuilder.Put(regNumber, constant));
  
  public static ICommand Push(byte regNumber) => new Command(CommandBuilder.Push(regNumber));
  public static ICommand Pop(int count) => new Command(CommandBuilder.Pop(count));

  public static ICommand Print(string text) => new Command(CommandBuilder.Print());
  public static ICommand Add(byte regNumber) => new Command(CommandBuilder.Add(regNumber));
  public static ICommand Sub(byte regNumber) => new Command(CommandBuilder.Sub(regNumber));
  public static ICommand Lt(byte regNumber) => new Command(CommandBuilder.Lt(regNumber));
  public static ICommand Gt(byte regNumber) => new Command(CommandBuilder.Gt(regNumber));
  
  public static ICommand Jmp() => new Command(CommandBuilder.Jmp());
  public static ICommand JmpTo(byte regNumber) => new Command(CommandBuilder.JmpTo(regNumber));
  
  public static ICommand Read(byte regNumber, int stackAddress) => new Command(CommandBuilder.Read(regNumber, stackAddress));
  public static ICommand Write(byte regNumber, int stackAddress) => new Command(CommandBuilder.Write(regNumber, stackAddress));
}