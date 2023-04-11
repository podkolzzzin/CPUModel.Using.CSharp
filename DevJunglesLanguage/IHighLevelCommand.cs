using DevJunglesAssembler;

namespace DevJunglesLanguage;

public interface IHighLevelCommand
{
  IEnumerable<ICommand> Compile();
}