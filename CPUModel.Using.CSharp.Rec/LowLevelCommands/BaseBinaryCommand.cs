

abstract class BaseBinaryCommand : ICommand
{
    private readonly int _regNumberForResult;
    private readonly string _command;

    public BaseBinaryCommand(int regNumberForResult, string command)
    {
        _regNumberForResult = regNumberForResult;
        _command = command;
    }

    public void Dump()
    {
        Console.Write($"{_command} r{_regNumberForResult}");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_regNumberForResult] = ExecuteBinaryCommand(registers[0], registers[1]);
        currentCommandIndex++;
    }

    protected abstract int ExecuteBinaryCommand(int left, int right);
}
