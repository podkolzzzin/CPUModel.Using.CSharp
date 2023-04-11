namespace DevJunglesAssembler;

public class JumpCommand : ICommand
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