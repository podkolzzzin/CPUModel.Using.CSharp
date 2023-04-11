namespace DevJunglesAssembler;

public interface ICommand
{
    void Execute(ExecutionContext executionContext);

    void Dump(ExecutionContext context);
}