using DevJungles.Assembler;
using static DevJungles.Language.Commands.Command;

namespace DevJungles.Language.Commands;

class WhileCommand : IHighLevelCommand
{
    private readonly ISimpleCommand[] _condition;
    private readonly ISimpleCommand[] _body;

    public WhileCommand(ISimpleCommand[] condition, ISimpleCommand[] body)
    {
        _condition = condition;
        _body = body;
    }

    public IEnumerable<AsmCommand> Compile()
    {
        var realBody = _body.Concat(new ISimpleCommand[]
        {
            Put(0, int.MaxValue), // Stub
            Jmp()
        }).ToArray();
        var ifCommandsCount = new IfCommand(_condition, realBody, Array.Empty<ISimpleCommand>())
            .Compile().Count() - 3;
        realBody[^2] = Put(0, -ifCommandsCount);

        return new IfCommand(_condition, realBody, Array.Empty<ISimpleCommand>()).Compile();
    }
}