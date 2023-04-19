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
var asm = compiler.Compile(MyProgram());


var vm = new OperationSystem();
var process = vm.CreateProcess(asm);
process.Start();

Console.ReadLine();

Source MyProgram()
{
    var builder = new SourceBuilder();
    var fibonachi = new FunctionCommand(4);
    fibonachi.Body = new ICommand[] {
        // a:4, b:3, i:2, j:1
        Read(0, 4),
        Read(1, 3),
        Add(0),
        Write(0, 4),
        Read(0, 2),
        Put(1, 1),
        Add(0),
        Write(0, 2),
    }.Concat(new ICommand[] {
        new IfCommand(new ICommand[] {
                Read(0, 2),
                Read(1, 1),
                Lt(0),
            }, new ICommand[] {
                new CallCommand(
                    new [] {
                        Read(0, 3),
                        Read(0, 4),
                        Read(0, 2),
                        Read(0, 1),
                    }, fibonachi),
                Write(0, 4),
            },
            Array.Empty<ICommand>()),
        Read(0, 4),
    }).ToArray();

    builder.Add(Print("#StopRussianAggression"));
    builder.Add(new CallCommand(new [] { // fib(1, 1, 2, 7) //  1 1 2 3 5
        Put(0, 1),
        Put(0, 1),
        Put(0, 2),
        Put(0, 7),
    }, fibonachi));
    builder.AddFunction(fibonachi);
    
    return builder.Build();
}
