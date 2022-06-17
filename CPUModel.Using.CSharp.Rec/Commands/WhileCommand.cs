class WhileCommand
{
    private readonly ICommand[] _condition;
    private readonly ICommand[] _body;

    public WhileCommand(ICommand[] condition, ICommand[] body)
    {
        _condition = condition;
        _body = body;
    }

    public IEnumerable<ICommand> Compile()
    {
        return new ForCommand(Array.Empty<ICommand>(), _condition, Array.Empty<ICommand>(), _body).Compile();
    }
}