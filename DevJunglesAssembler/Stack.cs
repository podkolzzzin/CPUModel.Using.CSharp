using System.Buffers;

namespace DevJunglesAssembler;

public ref struct Stack
{
    private readonly Span<int> _data;
    private int _top = -1;

    public Stack(Span<int> data)
    {
        _data = data;
    }

    public void Push(int value)
    {
        _top++;
        _data[_top] = value;
    }

    public void Pop(int count)
    {
        _top -= count;
    }

    public void Dump()
    {
        for (int i = 0; i < _top; i++)
        {
            Console.Write(Get(i).ToString().PadLeft(2));
            Console.Write(" ");
        }
    }
    
    public void Set(int stackAddress, int val)
    {
        _data[_top - stackAddress] = val;
    }
    public int Get(int stackAddress) => _data[_top - stackAddress];
}