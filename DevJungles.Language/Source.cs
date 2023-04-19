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
  public void Add(ISimpleCommand command) {}
  public void Add(IHighLevelCommand command) {}
}