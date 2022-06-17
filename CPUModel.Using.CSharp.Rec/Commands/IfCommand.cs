class IfCommand
{
    private readonly ICommand[] _condition, _ifClause, _elseClause;

    public IfCommand(ICommand[] condition, ICommand[] ifClause, ICommand[] elseClause)
    {
        _condition = condition;
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    public IEnumerable<ICommand> Compile()
    {
        foreach (var command in _condition)
            yield return command;

        yield return new PutConstantToRegisterCommand(1, 1);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        yield return new PutConstantToRegisterCommand(1, _ifClause.Length + 3);
        yield return new PutConstantToRegisterCommand(0, 0);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        foreach (var command in _ifClause)
            yield return command;

        yield return new PutConstantToRegisterCommand(0, _elseClause.Length + 1);
        yield return new JumpCommand();

        foreach (var command in _elseClause)
            yield return command;
    }
}