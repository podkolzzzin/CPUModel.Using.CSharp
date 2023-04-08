// if (10 > 40) 
// {
// 
// }
// else 
// {
// 
// }






using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

class OutputCommand : ICommand
{
    private readonly string _text;

    public OutputCommand(string text)
    {
        _text = text;
    }

    public void Dump(ExecutionContext context)
    {
        Console.Write("out");
    }

    public void Execute(ExecutionContext executionContext)
    {
        Console.Write(_text);
        executionContext.CurrentCommandIndex++;
    }
}
