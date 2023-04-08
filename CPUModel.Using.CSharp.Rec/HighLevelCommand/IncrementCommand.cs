class IncrementCommand
{
    public IncrementCommand()
    {
    }

    public IEnumerable<ICommand> Compile(int stackAddress)
    {
        yield return new ReadCommand(stackAddress, 0);
        yield return new PutConstantToRegisterCommand(1, 1);
        yield return new AddCommand(0);
        yield return new WriteCommand(stackAddress, 0);
    }
}