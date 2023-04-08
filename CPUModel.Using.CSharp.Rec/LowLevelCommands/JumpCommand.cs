// int a = 50 + 40;
// int b = 30 + 20;
// a > b


using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

class JumpCommand : ICommand
{
    public void Dump(ExecutionContext context)
    {
        Console.Write("jmp");
    }

    public void Execute(ExecutionContext executionContext)
    {
        executionContext.CurrentCommandIndex += executionContext.Registers[0];
    }
}