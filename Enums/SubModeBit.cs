using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Enums
{
  internal enum SubModeBit
  {
    EOR = 0b00000001,
    Video = 0b00000010,
    Audio = 0b00000100,
    Data = 0b00001000,
    Trigger = 0b00010000,
    Form = 0b00100000,
    RealTime = 0b01000000,
    EOF = 0b10000000,
    Any = 0b00001110
  }
}
