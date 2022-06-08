

class GtCommand : BaseBinaryCommand
{
    public GtCommand(int regNumberForResult)
        : base(regNumberForResult, "gt") { }

    protected override int ExecuteBinaryCommand(int left, int right) => Convert.ToInt32(left > right);
}
