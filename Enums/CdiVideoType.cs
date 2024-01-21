using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Enums
{
  public enum CdiVideoType
  {
    CLUT4,
    CLUT7,
    CLUT8,
    RL3,
    RL7,
    DYUV,
    RGB555L,
    RGB555H,
    QHY,
    MPEG = 0xF,
    Reserved
  }
}
