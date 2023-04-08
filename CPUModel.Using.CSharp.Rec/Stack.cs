namespace CPUModel.Using.CSharp.Rec;

public class Stack
{
    private readonly int[] _data = new int[256];
    private int _top = -1;

    public void Push(int value)
    {
        _top++;
        _data[_top] = value;
    }

    public int Pop()
    {
        var value = _data[_top];
        _top--;
        return value;
    }
    public void Set(int stackAddress, int val)
    {
        _data[stackAddress] = val;
    }
    public int Get(int stackAddress) => _data[stackAddress];
}