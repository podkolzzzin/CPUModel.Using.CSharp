using DevJungles.Assembler;

namespace DevJungles.Language.Commands;

public class ForCommand : IHighLevelCommand
{
    private readonly ICommand[] _declarations;
    private readonly WhileCommand _loop;

    public ForCommand(ICommand[] declarations, ISimpleCommand[] condition, ISimpleCommand[] increment, ISimpleCommand[] body)
    {
        _declarations = declarations;
        _loop = new WhileCommand(condition, body.Concat(increment).ToArray());
    }

    public int Size => _loop.Size + _declarations.Sum(x => x.Size);

    public void Compile(Emitter emitter)
    {
        emitter.Emit(_declarations);
        emitter.Emit(_loop);
    }
}
