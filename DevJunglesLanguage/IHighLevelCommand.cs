using DevJunglesVirtualMachine;

namespace DevJunglesLanguage;

public interface IHighLevelCommand : ICommand
{
  void Compile(IEmitter emitter);
}

public interface ISimpleCommand : ICommand
{
  int ICommand.Size => 1;

  AsmCommand AsBytes();
}

public interface ICommand
{
  int Size { get; }
}

public interface IEmitter
{
  int Position { get; }
  void Emit(ICommand command, Action<ISimpleCommand>? processor = null);
}