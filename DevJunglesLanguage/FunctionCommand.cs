using DevJunglesAssembler;
using static DevJunglesLanguage.Command;

namespace DevJunglesLanguage;

public class FunctionCommand : IHighLevelCommand
{
  private readonly int _argsCount;
  public FunctionCommand(int argsCount)
  {
    _argsCount = argsCount;
  }
  
  public ISomeCommand[] Body { get; set; }
  
  public int Position { get; private set; }
  
  public void Compile(IEmitter emitter)
  {
    Position = emitter.Position;
    int declarations = 0;
    foreach (var command in Body)
    {
      emitter.Emit(command, cmd => declarations += cmd.AsBytes().Command == Commands.Push ? 1 : 0);
    }
    emitter.Emit(Read(1, 0));
    emitter.Emit(Pop(declarations + _argsCount + 1));
    emitter.Emit(JmpTo(1));
  }
  
  public int Size => Body.Sum(x => x.Size) + 3;
}

public class CallCommand : IHighLevelCommand
{
  private readonly ICommand[] _arguments;
  private readonly FunctionCommand _functionCommand;
  public CallCommand(ICommand[] arguments, FunctionCommand functionCommand)
  {
    _arguments = arguments;
    _functionCommand = functionCommand;
  }

  public void Compile(IEmitter emitter)
  {
    for (int i = 0; i < _arguments.Length; i++)
    {
      emitter.Emit(Push(0));
      emitter.Emit(Offset(i + 1, _arguments[i]));
      emitter.Emit(Write(0, 0));
    }
    emitter.Emit(Push(0));
    emitter.Emit(Put(0, emitter.Position + 4));
    emitter.Emit(Write(0, 0));
    emitter.Emit(Put(1, _functionCommand.Position));
    emitter.Emit(JmpTo(1));
  }
  private ICommand Offset(int i, ICommand command)
  {
    var cmd = command.AsBytes();
    return cmd.Command == Commands.Read ? Read(cmd.Register1, cmd.LeftOperand + i) : command;
  }
  public int Size => _arguments.Sum(x => x.Size) + _arguments.Length * 2 + 5;
}