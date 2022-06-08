// 20 + 30
// 20 > 30


class WriteCommand : ICommand
{
    private readonly int _regNumberToWriteFrom;
    private readonly string _address;

    public WriteCommand(string address, int regNumberToWriteFrom)
    {
        _address = address;
        _regNumberToWriteFrom = regNumberToWriteFrom;
    }

    public void Dump()
    {
        Console.Write($"{_address} = r{_regNumberToWriteFrom}");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        Memory.Write(_address, registers[_regNumberToWriteFrom]);
        currentCommandIndex++;
    }
}
