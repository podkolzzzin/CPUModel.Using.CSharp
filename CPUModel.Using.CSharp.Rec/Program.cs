using DevJunglesAssembler;
using DevJunglesCompiler;
using DevJunglesLanguage;

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
    builder.Add(new ForCommand(declarations, condition, increment, body));
    return builder.Build();
}









Console.ReadLine();


