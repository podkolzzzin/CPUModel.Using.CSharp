using CPUModel.Using.CSharp.Rec;
using static Command;

var registers = new int[2];

// for (var i = 0; i < 3; i++) 
// {
//     Console.WriteLine("#StopRussianAggression");
// }

var declarations = new ICommand[]
{
    Put(0, 0),
    Write(0, "i"),
};

var condition = new []
{
    Put(1, 3),
    Read(0, "i"),
    Lt(0)
};

var body = new []
{
    Print("#StopRussianAggression")
}.ToArray();

var increment = new IncrementCommand("i").Compile().ToArray();

var commands = new ForCommand(declarations, condition, increment, body).Compile().ToArray();
var asmCommands = commands.Select(x => x.ToAsmCommand()).ToArray();

var ctx = new ExecutionContext() 
{
    Registers = new int[2], 
    Stack = new Stack(), 
    CurrentCommandIndex = 0
};

while (ctx.CurrentCommandIndex < asmCommands.Length)
{
    Console.Write($"[{ctx.CurrentCommandIndex.ToString().PadLeft(3, '0')}]");
    var currentCommand = asmCommands[ctx.CurrentCommandIndex];
    currentCommand.Dump();
    Console.CursorLeft = 60;
    currentCommand.Execute(ref ctx);
    Console.CursorLeft = 20;
    registers.Dump();
    Console.CursorLeft = 30;
    Memory.Dump();
    Console.WriteLine();
}

Console.ReadLine();

public enum Commands : byte
{
    Add,
    Sub,
    Lt,
    Gt,
    Jmp,
    Read, 
    Write,
    Put,
    Print,
}

public struct AsmCommand // 12 bytes like Arm x32
{
    public Commands Command;
    public byte Register1;
    public byte Register2;
    public byte Register3;

    public int LeftOperand;
    public int RightOperand;

    public string Variable; // TEMP SOLUTION. Will be removed
}