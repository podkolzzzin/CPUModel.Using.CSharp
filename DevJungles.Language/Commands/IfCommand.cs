using DevJungles.Assembler;
using static DevJungles.Language.Commands.Command;

namespace DevJungles.Language.Commands;

public class IfCommand : IHighLevelCommand
{
    private readonly ICommand[] _condition, _ifClause, _elseClause;

    public IfCommand(ICommand[] condition, ICommand[] ifClause, ICommand[] elseClause)
    {
        _condition = condition;
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    public void Compile(Emitter emitter)
    {
        foreach (var command in _condition)
            emitter.Emit(command);

        emitter.Emit(Put(1, 1));
        emitter.Emit(Add(0));
        emitter.Emit(Jmp());

        var ifClause = _ifClause.Sum(x => x.Size);
        var elseClause = _elseClause.Sum(x => x.Size);
        emitter.Emit(Put(1, ifClause + 3));
        emitter.Emit(Put(0, 0));
        emitter.Emit(Add(0));
        emitter.Emit(Jmp());

        foreach (var command in _ifClause)
            emitter.Emit(command);

        emitter.Emit(Put(0, elseClause + 1));
        emitter.Emit(Jmp());

        foreach (var command in _elseClause)
            emitter.Emit(command);
    }
    public int Size => _condition.Sum(x => x.Size) + _ifClause.Sum(x => x.Size) + _elseClause.Sum(x => x.Size) + 9;
}