namespace DevJunglesAssembler;

public class LtCommand : BaseBinaryCommand
{
    public LtCommand(int regNumberForResult)
        : base(regNumberForResult, "lt") { }

    protected override int ExecuteBinaryCommand(int left, int right) => Convert.ToInt32(left < right);
}
