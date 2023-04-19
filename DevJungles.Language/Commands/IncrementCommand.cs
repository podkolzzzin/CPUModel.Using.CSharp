using static DevJungles.Language.Commands.Command;

namespace DevJungles.Language.Commands;

public class IncrementCommand
{
    private readonly int _address;

    public IncrementCommand(int address)
    {
        _address = address;
    }

    public IEnumerable<ISimpleCommand> Compile()
    {
        yield return Read(0, _address);
        yield return Put(1, 1);
        yield return Add(0);
        yield return Write(0, _address);
    }
}