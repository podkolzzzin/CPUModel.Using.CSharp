using static DevJunglesLanguage.Command;

namespace DevJunglesLanguage;

public class WhileCommand : IHighLevelCommand
{
    private readonly IfCommand _ifCommand;

    public WhileCommand(ICommand[] condition, ICommand[] body)
    {
        var realBody = body.Concat(new ISimpleCommand[]
        {
            Put(0, int.MaxValue), // Stub
            Jmp()
        }).ToArray();
        _ifCommand = new IfCommand(condition, realBody, Array.Empty<ICommand>());
        var ifCommandsCount = _ifCommand.Size - 3;
        realBody[^2] = Put(0, -ifCommandsCount);
    }

    public void Compile(IEmitter emitter)
    {
        _ifCommand.Compile(emitter);
    }
    
    public int Size => _ifCommand.Size;
}