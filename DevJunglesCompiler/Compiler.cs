using DevJunglesVirtualMachine;

namespace DevJunglesCompiler;

public class Compiler
{
  public Assembly Compile(Source source)
  {
    return new Assembly(source.Commands);
  }
}