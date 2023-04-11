namespace DevJunglesAssembler;

public class SubtractCommand : BaseBinaryCommand
{
    public SubtractCommand(int regNumberForResult)
        : base(regNumberForResult, "sub") { }

    protected override int ExecuteBinaryCommand(int left, int right) => left - right;
}
