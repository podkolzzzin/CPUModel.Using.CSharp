using DevJungles.VirtualMachine;

namespace DevJungles.Language;

public class Compiler
{
  public Assembly Compile(Source source)
  {
    return new Assembly(source.Commands);
  }
}