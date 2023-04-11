namespace DevJunglesAssembler;

public class OutputCommand : ICommand
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
