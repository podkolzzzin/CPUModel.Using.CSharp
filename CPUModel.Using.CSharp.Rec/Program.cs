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

class ForCommand
{
    private readonly ICommand[] _preCondition;
    private readonly ICommand[] _condition;
    private readonly ICommand[] _postCondition;
    private readonly ICommand[] _body;

    public ForCommand(
        ICommand[] preCondition,
        ICommand[] condition,
        ICommand[] postCondition,
        ICommand[] body)
    {
        _preCondition = preCondition;
        _condition = condition;
        _postCondition = postCondition;
        _body = body;
    }

    public IEnumerable<ICommand> Compile()
    {
        var realBody = _body
            .Concat(_postCondition)
            .Concat(new ICommand[]
            {
                new PutConstantToRegisterCommand(0, int.MaxValue), // Stub
                new JumpCommand()
            })
            .ToArray();
        var ifCommandsCount = new IfCommand(_condition, realBody.ToArray(), Array.Empty<ICommand>()).Compile().Count() - 3;
        realBody[^2] = new PutConstantToRegisterCommand(0, -ifCommandsCount);
        
        
        return _preCondition
            .Concat(new IfCommand(
                _condition,
                realBody,
                Array.Empty<ICommand>()).Compile());
    }
}

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
        return new ForCommand(Array.Empty<ICommand>(), _condition, Array.Empty<ICommand>(), _body).Compile();
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