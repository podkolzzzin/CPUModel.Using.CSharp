namespace DevJunglesAssembler;

public class WriteCommand : ICommand
{
    private readonly int _regNumberToWriteFrom;
    private readonly int _stackAddress;

    public WriteCommand(int stackAddress, int regNumberToWriteFrom)
    {
        _stackAddress = stackAddress;
        _regNumberToWriteFrom = regNumberToWriteFrom;
    }

    public void Dump(ExecutionContext context)
    {
        Console.Write($"{_stackAddress} = r{_regNumberToWriteFrom}");
    }

    public void Execute(ExecutionContext executionContext)
    {
        executionContext.Stack.Set(_stackAddress, executionContext.Registers[_regNumberToWriteFrom]);
        executionContext.CurrentCommandIndex++;
    }
}
