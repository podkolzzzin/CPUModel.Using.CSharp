// 20 + 30
// 20 > 30


using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

class ReadCommand : ICommand
{
    private readonly int _regNumberToSaveReadValue;
    private readonly int _stackAddress;

    public ReadCommand(int stackAddress, int regNumberToSaveReadValue)
    {
        _regNumberToSaveReadValue = regNumberToSaveReadValue;
        _stackAddress = stackAddress;
    }

    public void Dump(ExecutionContext executionContext)
    {
        Console.Write($"r{_regNumberToSaveReadValue} = {executionContext.Stack.Get(_stackAddress)}");
    }

    public void Execute(ExecutionContext executionContext)
    {
        executionContext.Registers[_regNumberToSaveReadValue] = executionContext.Stack.Get(_stackAddress);
        executionContext.CurrentCommandIndex++;
    }
}
