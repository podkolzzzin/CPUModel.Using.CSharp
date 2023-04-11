using DevJunglesAssembler;
using DevJunglesLanguage;

namespace DevJunglesCompiler;

public class SourceBuilder
{
  private readonly List<ICommand> _commands = new();
  
  public SourceBuilder Add(ICommand command)
  {
    _commands.Add(command);
    return this;
  }

  public SourceBuilder Add(IHighLevelCommand command)
  {
    _commands.AddRange(command.Compile());
    return this;
  }

  public Source Build() => new (_commands.ToArray());
}