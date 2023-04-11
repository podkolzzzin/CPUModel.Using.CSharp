namespace DevJunglesAssembler;

public abstract class BaseBinaryCommand : ICommand
{
    private readonly int _regNumberForResult;
    private readonly string _command;

    public BaseBinaryCommand(int regNumberForResult, string command)
    {
        _regNumberForResult = regNumberForResult;
        _command = command;
    }

    public void Dump(ExecutionContext context)
    {
        Console.Write($"{_command} r{_regNumberForResult}");
    }

    public void Execute(ExecutionContext executionContext)
    {
        executionContext.Registers[_regNumberForResult] = ExecuteBinaryCommand(executionContext.Registers[0], executionContext.Registers[1]);
        executionContext.CurrentCommandIndex++;
    }

    protected abstract int ExecuteBinaryCommand(int left, int right);
}
