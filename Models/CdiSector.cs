using OGLibCDi.Enums;
using OGLibCDi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Models
{
  public class CdiSector
  {
    private const byte HEADER_SIZE = 16;
    private const short SUB_HEADER_DATA_SIZE = 4;
    
    private const short DATA_SECTOR_SIZE = 2048;
    private const short VIDEO_SECTOR_SIZE = 2324;
    private const short AUDIO_SECTOR_SIZE = 2304;

    private byte[] _subHeaderData; // 4 bytes, technically but that's only because the 4 bytes are duplicated
    private byte[] _sectorData; // 2352 bytes
    private byte _sectorType;

    public int SectorIndex { get; private set; }
    public int FileNumber { get => _subHeaderData[(int)SubHeaderByte.FileNumber]; }
    public int Channel { get => _subHeaderData[(int)SubHeaderByte.ChannelNumber]; }

    public CodingInfo Coding { get; private set; }
    public SubModeInfo SubMode { get; private set; }

    public string SectorTypeString { get => GetSectorType().ToString(); }

    public CdiSector(byte[] sectorData, int sectorIndex) 
    {
      SectorIndex = sectorIndex;
      _sectorData = sectorData;
      _subHeaderData = _sectorData.Skip(HEADER_SIZE).Take(SUB_HEADER_DATA_SIZE).ToArray();
      Coding = new CodingInfo(_subHeaderData[(int)SubHeaderByte.CodingInfo]);
      SubMode = new SubModeInfo(_subHeaderData[(int)SubHeaderByte.Submode], _subHeaderData[(int)SubHeaderByte.ChannelNumber], _subHeaderData[(int)SubHeaderByte.CodingInfo]);
      _sectorType = _subHeaderData[(int)SubHeaderByte.Submode] switch
      {
        var sub when (sub & (1 << 1)) != 0 => 0b00000010,
        var sub when (sub & (1 << 2)) != 0 => 0b00000100,
        var sub when (sub & (1 << 3)) != 0 => 0b00001000,
        _ => 0b00000000
      };
    }

    public CdiSectorType GetSectorType()
    {
      return _sectorType switch
      {
        0b00000010 => CdiSectorType.Video,
        0b00000100 => CdiSectorType.Audio,
        0b00001000 => CdiSectorType.Data,
        _ => CdiSectorType.Empty
      };
    }

    public byte[] GetSectorData(bool unparsed = false, CdiSectorType? typeOverride = null)
    {
      var bytes = _sectorData.Skip(HEADER_SIZE + (2 * SUB_HEADER_DATA_SIZE)).ToArray();

      if (unparsed) return _sectorData;

      var type = typeOverride != null ? typeOverride : GetSectorType();

      switch (type)
      {
        case CdiSectorType.Audio:
          return bytes.Take(AUDIO_SECTOR_SIZE).ToArray();
        case CdiSectorType.Video:
          return bytes.Take(VIDEO_SECTOR_SIZE).ToArray();
        case CdiSectorType.Data:
          return bytes.Take(DATA_SECTOR_SIZE).ToArray();
        default:
          return bytes;
      }
    }
  }
}
