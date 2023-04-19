using DevJungles.Language;
using static DevJungles.Language.Command;

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
        var realBody = _body.Concat(new ICommand[]
        {
            Put(0, int.MaxValue), // Stub
            Jmp()
        }).ToArray();
        var ifCommandsCount = new IfCommand(_condition, realBody, Array.Empty<ICommand>())
            .Compile().Count() - 3;
        realBody[^2] = Put(0, -ifCommandsCount);

        return new IfCommand(_condition, realBody, Array.Empty<ICommand>()).Compile();
    }
}