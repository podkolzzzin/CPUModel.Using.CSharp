int[] registers = new int[2];
//
// for (int i = 0; i < 10; i++) 
// {
//     Console.WriteLine("#StopRussianAgression");
//     i++;
// }

var declarations = new ICommand[]
{
    new PutConstantToRegisterCommand(0, 0),
    new WriteCommand("i", 0),
};

var condition = new ICommand[]
{
    new PutConstantToRegisterCommand(1, 10),
    new ReadCommand("i", 0),
    new LtCommand(0)
};

var body = new ICommand[]
{
    new OutputCommand("#StopRussianAgression")
};

var commands = new ForCommand(
        preCondition: declarations,
        condition: condition,
        postCondition: new IncrementCommand("i").Compile().ToArray(),
        body: body)
    .Compile()
    .ToArray();

for (int i = 0; i < commands.Length;)
{
    Console.Write($"[{i.ToString().PadLeft(3, '0')}]");
    var currentCommand = commands[i];
    currentCommand.Dump();
    Console.CursorLeft = 60;
    currentCommand.Execute(registers, ref i);
    Console.CursorLeft = 20;
    registers.Dump();
    Console.CursorLeft = 30;
    Memory.Dump();
    Console.WriteLine();
}

Console.ReadLine();