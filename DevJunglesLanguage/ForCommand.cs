using DevJunglesAssembler;

namespace DevJunglesLanguage;

public class ForCommand : IHighLevelCommand
{
    private readonly ICommand[] _declarations;
    private readonly WhileCommand _whileCommand;

    public ForCommand(ICommand[] declarations, ICommand[] condition, ICommand[] increment, ICommand[] body)
    {
        _declarations = declarations;

        _whileCommand = new WhileCommand(condition, body.Concat(increment).ToArray());
    }

    public int Size => _declarations.Sum(x => x.Size) + _whileCommand.Size;

    public void Compile(IEmitter emitter)
    {
        foreach (var command in _declarations)
            emitter.Emit(command);

        _whileCommand.Compile(emitter);
    }
}