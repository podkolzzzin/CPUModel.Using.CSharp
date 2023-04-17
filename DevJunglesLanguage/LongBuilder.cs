using DevJunglesAssembler;

namespace DevJunglesLanguage;


public static class ByteHelper
{
  public static byte[] AsBytes(Commands command)
  {
    var arr = GetArray();
    arr[0] = (byte)command;
    return arr;
  }

  public static byte[] AsBytes(Commands command, byte regNumber)
  {
    var arr = AsBytes(command);
    arr[1] = regNumber;
    return arr;
  }
  
  public static byte[] AsBytes(Commands command, int stackAddress)
  {
    var arr = AsBytes(command);
    arr[4] = (byte)stackAddress;
    arr[5] = (byte)(stackAddress >> 8);
    arr[6] = (byte)(stackAddress >> 16);
    arr[7] = (byte)(stackAddress >> 24);
    return arr;
  }
  
  public static byte[] AsBytes(Commands command, byte regNumber, int constant)
  {
    var arr = AsBytes(command, regNumber);
    arr[4] = (byte)constant;
    arr[5] = (byte)(constant >> 8);
    arr[6] = (byte)(constant >> 16);
    arr[7] = (byte)(constant >> 24);
    return arr;
  }
  
  private static byte[] GetArray() => new byte[12];
}