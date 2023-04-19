using static Commands;

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

      Read => $"read r{command.Register1} {command.LeftOperand}",
      Write => $"write r{command.Register1} {command.LeftOperand}",
      Put => $"put r{command.Register1} {command.LeftOperand}",
      Print => "print",
      _ => throw new NotImplementedException(),
    };
    Console.Write(dump);
  }
    
  public static void Execute(this AsmCommand cmd, int[] registers, ref int commandIndex)
  {
    switch (cmd.Command)
    {
      case Add or Sub or Lt or Gt:
        var left = registers[0];
        var right = registers[1];
        registers[cmd.Register1] = cmd.Command switch {
          Add => left + right,
          Sub => left - right,
          Gt => left > right ? 1 : 0,
          Lt => left < right ? 1 : 0,
          _ => throw new InvalidOperationException(cmd.Command.ToString()),
        };
        break;
      case Put:
        registers[cmd.Register1] = cmd.LeftOperand;
        break;
      case Write:
        Memory.Write(cmd.Variable, registers[cmd.Register1]);
        break;
      case Read:
        registers[cmd.Register1] = Memory.Read(cmd.Variable);
        break;
      case Print:
        Console.Write("PRINT");
        break;
      //throw new NotImplementedException(); // TODO: Implement
      case Jmp:
        commandIndex += registers[0];
        break;
      default:
        throw new InvalidOperationException();
    }
    if (cmd.Command is not Jmp) commandIndex++;
  }
}