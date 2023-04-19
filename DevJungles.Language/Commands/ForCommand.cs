using DevJungles.Assembler;

namespace DevJungles.Language.Commands;

public class ForCommand : IHighLevelCommand
{
    private readonly ISimpleCommand[] _declarations;
    private readonly ISimpleCommand[] _condition;
    private readonly ISimpleCommand[] _increment;
    private readonly ISimpleCommand[] _body;

    public ForCommand(ISimpleCommand[] declarations, ISimpleCommand[] condition, ISimpleCommand[] increment, ISimpleCommand[] body)
    {
        _declarations = declarations;
        _condition = condition;
        _increment = increment;
        _body = body;
    }

    public IEnumerable<AsmCommand> Compile()
    {
        var body = _body.Concat(_increment).ToArray();

        var loop = new WhileCommand(_condition, body).Compile();
        
        return _declarations.Select(x => x.ToAsmCommand())
            .Concat(loop.Select(x => x));
    }
}
