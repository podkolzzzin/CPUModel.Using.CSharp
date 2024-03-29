﻿using DevJunglesAssembler;
using DevJunglesLanguage;
using static DevJunglesLanguage.Command;

namespace DevJunglesCompiler;

public class SourceBuilder
{
  private class Emitter : IEmitter
  {
    private readonly ISimpleCommand[] _commands;
    public int Position { get; set; }

    public Emitter(int size)
    {
      _commands = new ISimpleCommand[size];
    }
    
    private void Add(ISimpleCommand command)
    {
      _commands[Position] = command;
      Position++;
    }
    
    public void Emit(ICommand command, Action<ISimpleCommand>? processor = null)
    {
      if (command is ISimpleCommand cmd)
      {
        processor?.Invoke(cmd);
        Add(cmd);
      }
      else if (command is IHighLevelCommand highLevelCommand)
        highLevelCommand.Compile(this);
      else
        throw new Exception("Unknown command type");
    }
    public ISimpleCommand[] ToArray() => _commands;
  }
  
  private readonly List<FunctionCommand> _functions = new();
  private readonly List<ICommand> _commands = new();

  public SourceBuilder Add(ISimpleCommand command)
  {
    _commands.Add(command);
    return this;
  }

  public SourceBuilder Add(FunctionCommand command)
  {
    _functions.Add(command);
    return this;
  }

  public SourceBuilder Add(IHighLevelCommand command)
  {
    _commands.Add(command);
    return this;
  }

  public Source Build()
  {
    _commands.Add(Put(1, -1));
    _commands.Add(JmpTo(1));
    var functionSection = _commands.Sum(x => x.Size);
    var emitter = new Emitter(functionSection + _functions.Sum(x => x.Size));
    emitter.Position = functionSection;
    foreach (var function in _functions)
    {
      function.Compile(emitter);
    }
    emitter.Position = 0;
    foreach (var command in _commands)
    {
      emitter.Emit(command);
    }
    
    return new Source(emitter.ToArray());
  }
}