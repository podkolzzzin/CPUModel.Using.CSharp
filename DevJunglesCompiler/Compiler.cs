using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DevJunglesVirtualMachine;

namespace DevJunglesCompiler;

public class Compiler
{
  public Assembly Compile(Source source)
  {
    var arrOfStructs = source.Commands.Select(x => x.AsBytes())
      .ToArray();
    var data = MemoryMarshal.AsBytes(arrOfStructs.AsSpan());
    using (var writer = new BinaryWriter(File.OpenWrite("assembly.jungle")))
    {
      writer.Write(data);
    }
    return new Assembly("assembly.jungle");
  }
}