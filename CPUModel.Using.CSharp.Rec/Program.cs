using DevJunglesAssembler;
using DevJunglesCompiler;
using DevJunglesLanguage;
using static DevJunglesLanguage.Command;

var compiler = new Compiler();
var asm = compiler.Compile(MyProgram());

var os = new DevJunglesVirtualMachine.OperationSystem();
var process = os.CreateProcess(asm);
process.Start();



// for (var i = 0; i < 3; i++) 
// {
//     Console.WriteLine("#StopRussianAggression");
// }
Source MyProgram()
{
    var builder = new SourceBuilder();

    var function = new FunctionCommand(1);
    function.Body = new ICommand[] {
        Print("Does not matter"),
        Read(0, 0),
        Put(0, 10),
        Add(0)
    }; // f(x) -> print("Does not matter"); return x + 10;
    // fib(a, b, i, j) -> a = a + b; i = i + 1; if (i < j) a = fib(b, a, i, j); else return a;

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


    var declarations = new []
    {
        Push(0),
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

    var increment = new IncrementCommand().Compile(0).ToArray();
    var forCommand = new ForCommand(declarations, condition, increment, body);
    builder.Add(Print("#StopRussianAggression"));
    builder.Add(new CallCommand(new [] { // fib(1, 1, 2, 7) //  1 1 2 3 5
        Put(0, 1),
        Put(0, 1),
        Put(0, 2),
        Put(0, 7),
    }, fibonachi));
    builder.Add(fibonachi);
    
    return builder.Build();
}









Console.ReadLine();


