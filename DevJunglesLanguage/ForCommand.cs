using DevJunglesAssembler;

namespace DevJunglesLanguage;

public class ForCommand : IHighLevelCommand
{
    private readonly ICommand[] _declarations;
    private readonly ICommand[] _condition;
    private readonly ICommand[] _increment;
    private readonly ICommand[] _body;

    public ForCommand(ICommand[] declarations, ICommand[] condition, ICommand[] increment, ICommand[] body)
    {
        _declarations = declarations;
        _condition = condition;
        _increment = increment;
        _body = body;
    }

    public IEnumerable<ICommand> Compile()
    {
        var body = _body.Concat(_increment).ToArray();

        var loop = new WhileCommand(_condition, body).Compile();
        
        return _declarations.Concat(loop);
    }
}