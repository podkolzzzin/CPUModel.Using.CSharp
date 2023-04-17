using DevJunglesAssembler;
using DevJunglesLanguage;

namespace DevJunglesCompiler;

public class Source
{
  public ICommand[] Commands { get; }
  
  public Source(ICommand[] commands)
  {
    Commands = commands;
  }
}