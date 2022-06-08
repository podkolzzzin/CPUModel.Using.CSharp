// if (10 > 40) 
// {
// 
// }
// else 
// {
// 
// }






class OutputCommand : ICommand
{
    private readonly string _text;

    public OutputCommand(string text)
    {
        _text = text;
    }

    public void Dump()
    {
        Console.Write("out");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        Console.Write(_text);
        currentCommandIndex++;
    }
}
