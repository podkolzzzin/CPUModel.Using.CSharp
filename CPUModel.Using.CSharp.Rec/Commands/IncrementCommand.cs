class IncrementCommand
{
    private readonly string _address;

    public IncrementCommand(string address)
    {
        _address = address;
    }

    public IEnumerable<ICommand> Compile()
    {
        yield return new ReadCommand(_address, 0);
        yield return new PutConstantToRegisterCommand(1, 1);
        yield return new AddCommand(0);
        yield return new WriteCommand(_address, 0);
    }
}