

public interface ICommand
{
    void Execute(int[] registers, ref int currentCommandIndex);

    void Dump();
}