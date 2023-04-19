using CPUModel.Using.CSharp.Rec;
using static Commands;

public ref struct ExecutionContext
{
  public int[] Registers;
  public Stack Stack;
  public int CurrentCommandIndex;
}

public static class AsmCommandExtensions // using static Commands;
{
  public static void Dump(this AsmCommand command)
  {
    var dump = command.Command switch {
      Add => $"add r0 r1 r{command.Register1}",
      Sub => $"sub r0 r1 r{command.Register1}",
      Lt => $"lt r0 r1 r{command.Register1}",
      Gt => $"gt r0 r1 r{command.Register1}",

      Jmp => $"jmp r0",

      Push => $"push",
      Read => $"read r{command.Register1} {command.LeftOperand}",
      Write => $"write r{command.Register1} {command.LeftOperand}",
      Put => $"put r{command.Register1} {command.LeftOperand}",
      Print => "print",
      _ => throw new NotImplementedException(),
    };
    Console.Write(dump);
  }
    
  public static void Execute(this AsmCommand cmd, ref ExecutionContext context)
  {
    switch (cmd.Command)
    {
      case Add or Sub or Lt or Gt:
        var left = context.Registers[0];
        var right = context.Registers[1];
        context.Registers[cmd.Register1] = cmd.Command switch {
          Add => left + right,
          Sub => left - right,
          Gt => left > right ? 1 : 0,
          Lt => left < right ? 1 : 0,
          _ => throw new InvalidOperationException(cmd.Command.ToString()),
        };
        break;
      case Put:
        context.Registers[cmd.Register1] = cmd.LeftOperand;
        break;
      case Push:
        context.Stack.Push(0);
        break;
      case Write:
        context.Stack.Set(cmd.LeftOperand, context.Registers[cmd.Register1]);
        break;
      case Read:
        context.Registers[cmd.Register1] = context.Stack.Get(cmd.LeftOperand);
        break;
      case Print:
        Console.Write("PRINT");
        break;
      //throw new NotImplementedException(); // TODO: Implement
      case Jmp:
        context.CurrentCommandIndex += context.Registers[0];
        break;
      default:
        throw new InvalidOperationException();
    }
    if (cmd.Command is not Jmp) context.CurrentCommandIndex++;
  }
}