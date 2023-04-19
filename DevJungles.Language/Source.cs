using DevJungles.Assembler;
using DevJungles.Language.Commands;

namespace DevJungles.Language;

public class Source
{
  public AsmCommand[] Commands { get; }
  public Source(AsmCommand[] commands)
  {
    Commands = commands;
  }
}



public class SourceBuilder
{
  private readonly Emitter _emitter = new();
  public void Add(ISimpleCommand command)
  {
    _emitter.Emit(command);
  }
  public void Add(IHighLevelCommand command)
  {
    _emitter.Emit(command);
  }

  public Source Build() => new (_emitter.ToArray());
}