using OGLibCDi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OGLibCDi.Models
{
  public class SubModeInfo
  {
    private int _codingInfo;

    private byte _byte { get; set; }

    private int _channel;

    public SubModeInfo(byte b, int channel, int codingInfo)
    {
      _byte = b;
      _channel = channel;
      _codingInfo = codingInfo;
    }

    public bool IsEmptySector => (_byte & (byte)SubModeBit.Any) == 0 && _channel == 0 && _codingInfo == 0;
    public bool IsEOF => (_byte & (1 << 7)) != 0;
    public bool IsRTF => (_byte & (1 << 6)) != 0;
    public bool IsForm2 => (_byte & (1 << 5)) != 0;
    public bool IsTrigger => (_byte & (1 << 4)) != 0;
    public bool IsData => (_byte & (1 << 3)) != 0;
    public bool IsAudio => (_byte & (1 << 2)) != 0;
    public bool IsVideo => (_byte & (1 << 1)) != 0;
    public bool IsEOR => (_byte & 1) != 0;
  }
}
