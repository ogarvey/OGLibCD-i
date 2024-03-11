using OGLibCDi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Models.Bolt
{
  public class BoltOffset
  {
    public uint Offset { get; }
    public uint NameHash { get; }
    public uint UncompressedSize { get; }
    public uint Flags { get; }
    public List<BoltOffset> Entries { get; set; }
    public int FileCount => (int)(Flags & 0xFF);
    public bool IsFolder => NameHash == 0;
    public bool IsCompressed => (Flags & BoltFileHelper.FLAG_UNCOMPRESSED) == 0;

    public BoltOffset(byte[] data)
    {
      Flags = BitConverter.ToUInt32(data.Take(4).Reverse().ToArray(), 0);
      UncompressedSize = BitConverter.ToUInt32(data.Skip(0x4).Take(4).Reverse().ToArray(), 0);
      Offset = BitConverter.ToUInt32(data.Skip(0x8).Take(4).Reverse().ToArray(), 0);
      NameHash = BitConverter.ToUInt32(data.Skip(0xC).Take(4).Reverse().ToArray(), 0);
    }

    public override string ToString()
    {
      return $"Offset: {Offset}, Entries: {Entries}, NameHash: {NameHash}, UncompressedSize: {UncompressedSize}, Flags: {Flags}";
    }
  }
}
