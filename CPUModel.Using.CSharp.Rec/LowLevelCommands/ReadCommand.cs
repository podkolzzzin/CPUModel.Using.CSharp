// 20 + 30
// 20 > 30


class ReadCommand : ICommand
{
    private readonly int _regNumberToSaveReadValue;
    private readonly string _address;

    public ReadCommand(string address, int regNumberToSaveReadValue)
    {
        _regNumberToSaveReadValue = regNumberToSaveReadValue;
        _address = address;
    }

    public void Dump()
    {
        Console.Write($"r{_regNumberToSaveReadValue} = {Memory.Read(_address)} {_address}");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_regNumberToSaveReadValue] = Memory.Read(_address);
        currentCommandIndex++;
    }
}
