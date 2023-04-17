using static DevJunglesLanguage.Command;

namespace DevJunglesLanguage;

public class IncrementCommand
{
    public IncrementCommand()
    {
    }

    public IEnumerable<ISimpleCommand> Compile(int stackAddress)
    {
        yield return Read(0, stackAddress);
        yield return Put(1, 1);
        yield return Add(0);
        yield return Write(0, stackAddress);
    }
}