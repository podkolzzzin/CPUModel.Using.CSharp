using static Command;

class IncrementCommand
{
    private readonly string _address;

    public IncrementCommand(string address)
    {
        _address = address;
    }

    public IEnumerable<ICommand> Compile()
    {
        yield return Read(0, _address);
        yield return Put(1, 1);
        yield return Add(0);
        yield return Write(0, _address);
    }
}