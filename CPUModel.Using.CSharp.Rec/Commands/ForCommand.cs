class ForCommand
{
    private readonly ICommand[] _preCondition;
    private readonly ICommand[] _condition;
    private readonly ICommand[] _postCondition;
    private readonly ICommand[] _body;

    public ForCommand(
        ICommand[] preCondition,
        ICommand[] condition,
        ICommand[] postCondition,
        ICommand[] body)
    {
        _preCondition = preCondition;
        _condition = condition;
        _postCondition = postCondition;
        _body = body;
    }

    public IEnumerable<ICommand> Compile()
    {
        var realBody = _body
            .Concat(_postCondition)
            .Concat(new ICommand[]
            {
                new PutConstantToRegisterCommand(0, int.MaxValue), // Stub
                new JumpCommand()
            })
            .ToArray();
        var ifCommandsCount = new IfCommand(_condition, realBody.ToArray(), Array.Empty<ICommand>()).Compile().Count() - 3;
        realBody[^2] = new PutConstantToRegisterCommand(0, -ifCommandsCount);
        
        
        return _preCondition
            .Concat(new IfCommand(
                _condition,
                realBody,
                Array.Empty<ICommand>()).Compile());
    }
}