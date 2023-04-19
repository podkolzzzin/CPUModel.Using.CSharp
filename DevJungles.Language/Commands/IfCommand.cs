using DevJungles.Assembler;
using static DevJungles.Language.Commands.Command;

namespace DevJungles.Language.Commands;

class IfCommand : IHighLevelCommand
{
    private readonly ISimpleCommand[] _condition, _ifClause, _elseClause;

    public IfCommand(ISimpleCommand[] condition, ISimpleCommand[] ifClause, ISimpleCommand[] elseClause)
    {
        _condition = condition;
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    public IEnumerable<AsmCommand> Compile()
    {
        foreach (var command in _condition)
            yield return command.ToAsmCommand();

        yield return Put(1, 1).ToAsmCommand();
        yield return Add(0).ToAsmCommand();
        yield return Jmp().ToAsmCommand();

        yield return Put(1, _ifClause.Length + 3).ToAsmCommand();
        yield return Put(0, 0).ToAsmCommand();
        yield return Add(0).ToAsmCommand();
        yield return Jmp().ToAsmCommand();

        foreach (var command in _ifClause)
            yield return command.ToAsmCommand();

        yield return Put(0, _elseClause.Length + 1).ToAsmCommand();
        yield return Jmp().ToAsmCommand();

        foreach (var command in _elseClause)
            yield return command.ToAsmCommand();
    }
}