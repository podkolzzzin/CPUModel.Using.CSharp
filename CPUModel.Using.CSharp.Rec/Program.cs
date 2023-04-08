using CPUModel.Using.CSharp.Rec.LowLevelCommands;
using ExecutionContext = CPUModel.Using.CSharp.Rec.LowLevelCommands.ExecutionContext;

var registers = new int[2];

// for (var i = 0; i < 3; i++) 
// {
//     Console.WriteLine("#StopRussianAggression");
// }

var declarations = new ICommand[]
{
    new DeclareCommand(),
    new PutConstantToRegisterCommand(0, 0),
    new WriteCommand(0, 0),
};

var condition = new ICommand[]
{
    new PutConstantToRegisterCommand(1, 3),
    new ReadCommand(0, 0),
    new LtCommand(0)
};

var body = new ICommand[]
{
    new OutputCommand("#StopRussianAggression")
}.ToArray();

var increment = new IncrementCommand().Compile(0).ToArray();

var commands = new ForCommand(declarations, condition, increment, body).Compile().ToArray();
var execute = new ExecutionContext(registers, 0);
while (execute.CurrentCommandIndex < commands.Length)
{
    Console.Write($"[{execute.CurrentCommandIndex.ToString().PadLeft(3, '0')}]");
    var currentCommand = commands[execute.CurrentCommandIndex];
    currentCommand.Dump(execute);
    Console.CursorLeft = 60;
    
    currentCommand.Execute(execute);
    Console.CursorLeft = 20;
    registers.Dump();
    Console.CursorLeft = 30;
    Console.WriteLine();
}

Console.ReadLine();