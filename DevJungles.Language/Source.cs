using DevJungles.Assembler;
using DevJungles.Language.Commands;
using static DevJungles.Language.Commands.Command;

namespace DevJungles.Language;

public class Source
{
  public AsmCommand[] Commands { get; }
  public Source(AsmCommand[] commands)
  {
    Commands = commands;
  }
}



public class SourceBuilder
{
  private readonly List<FunctionCommand> _functions = new();
  private readonly List<ICommand> _commands = new();
  
  public void Add(ICommand command)
  {
    _commands.Add(command);
  }

  public void AddFunction(FunctionCommand command)
  {
    _functions.Add(command);
  }

  public Source Build()
  {
    var commands = _commands.ToList();
    commands.Add(Put(1, -1));
    commands.Add(JmpTo(1));

    var functionSection = commands.Sum(x => x.Size);
    var emitter = new Emitter(functionSection + _functions.Sum(x => x.Size));
    emitter.Position = functionSection;
    foreach (var function in _functions)
    {
      function.Compile(emitter);
    }
    
    emitter.Position = 0;
    foreach (var command in commands)
    {
      emitter.Emit(command);
    }
    
    return new(emitter.ToArray());
  }
}