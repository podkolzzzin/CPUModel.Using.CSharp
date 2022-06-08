// int a = 50 + 40;
// int b = 30 + 20;
// a > b


class JumpCommand : ICommand
{
    public void Dump()
    {
        Console.Write("jmp");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        currentCommandIndex += registers[0];
    }
}