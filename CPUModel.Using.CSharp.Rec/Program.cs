using DevJungles.Language;
using DevJungles.Language.Commands;
using DevJungles.VirtualMachine;
using static DevJungles.Language.Commands.Command;
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

var builder = new SourceBuilder();
builder.Add(new ForCommand(declarations, condition, increment, body));

var compiler = new Compiler();
var asm = compiler.Compile(builder.Build());


var vm = new OperationSystem();
var process = vm.CreateProcess(asm);
process.Start();

Console.ReadLine();

