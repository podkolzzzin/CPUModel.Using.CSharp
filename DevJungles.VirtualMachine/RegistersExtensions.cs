

static class RegistersExtensions
{
    public static void Dump(this int[] registers)
    {
        foreach (var item in registers)
        {
            Console.Write($"{item,3}");
        }
    }
}
