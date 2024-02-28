using OGLibCDi.Models.Bolt;

namespace OGLibCDi.Helpers;

public static class BoltFileHelper
{
  public static int GetEndOfBoltData(byte[] data)
  {
    return BitConverter.ToInt32(data.Skip(0xC).Take(4).Reverse().ToArray(), 0);
  }

  public static int GetStartOfBoltData(byte[] data)
  {
    return BitConverter.ToInt32(data.Skip(0x18).Take(4).Reverse().ToArray(), 0);
  }

  public static List<int> GetBoltOffsets(byte[] data)
  {
    var offsets = new List<int>();

    var value = GetStartOfBoltData(data);
    offsets.Add(value);
    var index = 0x20;
    while (index < offsets[0])
    {
      value = BitConverter.ToInt32(data.Skip(index + 8).Take(4).Reverse().ToArray(), 0);
      offsets.Add(value);
      index += 16;
    }

    return offsets;
  }

  public static List<BoltOffset> GetBoltOffsetData(byte[] data)
  {
    var offsets = new List<BoltOffset>();

    var dataStart = GetStartOfBoltData(data);
    var offsetData = new BoltOffset
    {
      Offset = dataStart,
      InitialDataLength = data.Skip(0x13).Take(1).First() * 0x10,
      SecondaryDataLength = BitConverter.ToInt32(data.Skip(0x14).Take(4).Reverse().ToArray(), 0)
    };
    offsets.Add(offsetData);
    var index = 0x20;
    while (index < dataStart)
    {
      var initDataLen = BitConverter.ToInt32(data.Skip(index).Take(4).Reverse().ToArray(), 0) * 0x10;
      var secDataLen = BitConverter.ToInt32(data.Skip(index + 4).Take(4).Reverse().ToArray(), 0);
      var offset = BitConverter.ToInt32(data.Skip(index + 8).Take(4).Reverse().ToArray(), 0);
      offsetData = new BoltOffset
      {
        Offset = offset,
        InitialDataLength = initDataLen,
        SecondaryDataLength = secDataLen
      };
      offsets.Add(offsetData);
      index += 16;
    }

    return offsets;
  }
}


