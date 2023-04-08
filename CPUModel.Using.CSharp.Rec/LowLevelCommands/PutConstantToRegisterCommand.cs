

using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

class PutConstantToRegisterCommand : ICommand
{
    private readonly int _regNumberToWrite, _constant;

    public PutConstantToRegisterCommand(int regNumberToWrite, int constant)
    {
        _regNumberToWrite = regNumberToWrite;
        _constant = constant;   
    }

    public void Dump(ExecutionContext context)
    {
        Console.Write($"put r{_regNumberToWrite} {_constant}");
    }

    public void Execute(ExecutionContext executionContext)
    {
        executionContext.Registers[_regNumberToWrite] = _constant;
        executionContext.CurrentCommandIndex++;
    }
}
