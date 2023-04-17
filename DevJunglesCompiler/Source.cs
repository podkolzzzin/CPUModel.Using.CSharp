using DevJunglesAssembler;
using DevJunglesLanguage;

namespace DevJunglesCompiler;

public class Source
{
  public ISimpleCommand[] Commands { get; }
  
  public Source(ISimpleCommand[] commands)
  {
    Commands = commands;
  }
}