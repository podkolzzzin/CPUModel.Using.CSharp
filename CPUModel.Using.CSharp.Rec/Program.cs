int[] registers = new int[2];
// int i = 0;
// while (i < 10) 
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
}.Concat(new IncrementCommand("i").Compile()).ToArray();

var commands = declarations.Concat(new WhileCommand(condition, body)
    .Compile()).ToArray();

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

class WhileCommand
{
    private readonly ICommand[] _condition;
    private readonly ICommand[] _body;

    public WhileCommand(ICommand[] condition, ICommand[] body)
    {
        _condition = condition;
        _body = body;
    }

    public IEnumerable<ICommand> Compile()
    {
        var realBody = _body.Concat(new ICommand[]
        {
                new PutConstantToRegisterCommand(0, int.MaxValue), // Stub
                new JumpCommand()
        }).ToArray();
        var ifCommandsCount = new IfCommand(_condition, realBody, Array.Empty<ICommand>())
            .Compile().Count() - 3;
        realBody[^2] = new PutConstantToRegisterCommand(0, -ifCommandsCount);

        return new IfCommand(_condition, realBody, Array.Empty<ICommand>()).Compile();
    }
}

class IncrementCommand
{
    private readonly string _address;

    public IncrementCommand(string address)
    {
        _address = address;
    }

    public IEnumerable<ICommand> Compile()
    {
        yield return new ReadCommand(_address, 0);
        yield return new PutConstantToRegisterCommand(1, 1);
        yield return new AddCommand(0);
        yield return new WriteCommand(_address, 0);
    }
}

class IfCommand
{
    private readonly ICommand[] _condition, _ifClause, _elseClause;

    public IfCommand(ICommand[] condition, ICommand[] ifClause, ICommand[] elseClause)
    {
        _condition = condition;
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    public IEnumerable<ICommand> Compile()
    {
        foreach (var command in _condition)
            yield return command;

        yield return new PutConstantToRegisterCommand(1, 1);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        yield return new PutConstantToRegisterCommand(1, _ifClause.Length + 3);
        yield return new PutConstantToRegisterCommand(0, 0);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        foreach (var command in _ifClause)
            yield return command;

        yield return new PutConstantToRegisterCommand(0, _elseClause.Length + 1);
        yield return new JumpCommand();

        foreach (var command in _elseClause)
            yield return command;
    }
}