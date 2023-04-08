

using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

interface ICommand
{
    void Execute(ExecutionContext executionContext);

    void Dump(ExecutionContext context);
}