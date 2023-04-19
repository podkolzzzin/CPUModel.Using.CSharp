using static Command;

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

        yield return Put(1, 1);
        yield return Add(0);
        yield return Jmp();

        yield return Put(1, _ifClause.Length + 3);
        yield return Put(0, 0);
        yield return Add(0);
        yield return Jmp();

        foreach (var command in _ifClause)
            yield return command;

        yield return Put(0, _elseClause.Length + 1);
        yield return Jmp();

        foreach (var command in _elseClause)
            yield return command;
    }
}