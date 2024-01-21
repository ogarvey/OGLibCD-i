using static OGLibCDi.Helpers.BitManipulationHelpers;

namespace OGLibCDi.Models
{
  public class CodingInfo
  {
    private byte _byte { get; set; }

    public CodingInfo(byte b)
    {
      _byte = b;
    }

    #region AudioProperties
    public bool Emphasis => _byte.IsBitSet(6);
    public int BitsPerSample => (_byte >> 4) & 0x3;
    public int SampleRate => (_byte >> 2) & 0x3;
    public string SampleRateString => SampleRate switch
    {
      0 => "37.8 kHz",
      1 => "18.9 kHz",
      _ => "Reserved"
    };

    public int SamplingFrequencyValue => SampleRate switch
    {
      0 => 37800,
      1 => 18900,
      _ => 0
    };

    public string BitsPerSampleString => BitsPerSample switch
    {
      0 => "4 bits",
      1 => "8 bits",
      _ => "Reserved"
    };

    public bool IsStereo => (_byte & 0x03) == 1;
    public bool IsMono => (_byte & 0x03) == 0;
    #endregion

    #region VideoProperties
    public bool IsASCF => _byte.IsBitSet(7);
    public bool IsOddLines => _byte.IsBitSet(6);
    public byte Resolution => (byte)((_byte >> 4) & 0x3);
    public string ResolutionString => Resolution switch
    {
      0 => "Normal",
      1 => "Double",
      2 => "Reserved",
      3 => "High",
      _ => "Reserved"
    };

    public int Coding => _byte & 0xF;
    public string VideoString => Coding switch
    {
      0x0 => "CLUT4",
      0x1 => "CLUT7",
      0x2 => "CLUT8",
      0x3 => "RL3",
      0x4 => "RL7",
      0x5 => "DYUV",
      0x6 => "RGB555 (lower)",
      0x7 => "RGB555 (upper)",
      0x8 => "QHY",
      0xF => "MPEG",
      _ => "Reserved"
    };
    #endregion
  }
}
