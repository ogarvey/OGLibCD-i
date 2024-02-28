using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Models.Bolt
{
  public class BoltFile
  {
    public required string Name { get; set; }
    public required List<BoltOffset> Offsets { get; set; }
    public required byte[] Data { get; set; }
  }
}
