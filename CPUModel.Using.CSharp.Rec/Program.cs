using DevJungles.Language;
using DevJungles.VirtualMachine;
using static DevJungles.Language.Command;
using ExecutionContext = DevJungles.VirtualMachine.ExecutionContext;

// for (var i = 0; i < 3; i++) 
// {
//     Console.WriteLine("#StopRussianAggression");
// }

var declarations = new ICommand[]
{
    Push(),
    Put(0, 0),
    Write(0, 0),
};

var condition = new []
{
    Put(1, 3),
    Read(0, 0),
    Lt(0)
};

var body = new []
{
    Print("#StopRussianAggression")
}.ToArray();

var increment = new IncrementCommand(0).Compile().ToArray();

var commands = new ForCommand(declarations, condition, increment, body).Compile().ToArray();
var asmCommands = commands.Select(x => x.ToAsmCommand()).ToArray();
var compiler = new Compiler();
var asm = compiler.Compile(new Source(asmCommands));


var vm = new OperationSystem();
var process = vm.CreateProcess(asm);
process.Start();

Console.ReadLine();

