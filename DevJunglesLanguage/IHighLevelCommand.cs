using DevJunglesAssembler;
using DevJunglesVirtualMachine;
using ExecutionContext = DevJunglesAssembler.ExecutionContext;

namespace DevJunglesLanguage;

public interface IHighLevelCommand : ISomeCommand
{
  void Compile(IEmitter emitter);
}

public interface IEmitter
{
  int Position { get; }
  void Emit(ISomeCommand command, Action<ICommand>? processor = null);
}

public interface ICommand : ISomeCommand
{
  int ISomeCommand.Size => 1;

  AsmCommand AsBytes();
}

public interface ISomeCommand
{
  int Size { get; }
}