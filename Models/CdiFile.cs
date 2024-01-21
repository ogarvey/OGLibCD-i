using OGLibCDi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGLibCDi.Models
{
  public class CdiFile
  {
    private const short SECTOR_SIZE = 2352;
    private byte[] _cdiFileData { get; set; }
    
    public string FileName { get; private set; }
    public string FilePath { get; private set; }
    public string FileExtension { get; private set; }
    public int FileSizeBytes { get; private set; }
    
    public List<CdiSector> Sectors { get; private set; }
    public List<CdiSector> DataSectors => Sectors.Where(s => s.GetSectorType() == CdiSectorType.Data).ToList();
    public List<CdiSector> VideoSectors => Sectors.Where(s => s.GetSectorType() == CdiSectorType.Video).ToList();
    public List<CdiSector> AudioSectors => Sectors.Where(s => s.GetSectorType() == CdiSectorType.Audio).ToList();

    public int SectorCount { get => Sectors.Count; }
    public int DataSectorCount => Sectors.Count(s => s.GetSectorType() == CdiSectorType.Data);
    public int VideoSectorCount => Sectors.Count(s => s.GetSectorType() == CdiSectorType.Video);
    public int AudioSectorCount => Sectors.Count(s => s.GetSectorType() == CdiSectorType.Audio);
    public int EmptySectorCount => Sectors.Count(s => s.GetSectorType() == CdiSectorType.Empty);
    
    public CdiFile(string filepath)
    {
      FilePath = filepath;
      FileName = Path.GetFileNameWithoutExtension(filepath);
      FileExtension = Path.GetExtension(filepath);
      _cdiFileData = File.ReadAllBytes(filepath);
      FileSizeBytes = _cdiFileData.Length;
      Sectors = new List<CdiSector>();
      for (int i = 0, j = 0; i < _cdiFileData.Length; i += SECTOR_SIZE, j++)
      {
        var sectorData = _cdiFileData.Skip(i).Take(SECTOR_SIZE).ToArray();
        var sector = new CdiSector(sectorData, j);
        if (sector.GetSectorType() != CdiSectorType.Empty)
        {
          Sectors.Add(sector);
        }
      }
    }

    public CdiFile(string filepath, byte[] data)
    {
      FilePath = filepath;
      FileName = Path.GetFileNameWithoutExtension(filepath);
      FileExtension = Path.GetExtension(filepath);
      _cdiFileData = data;
      FileSizeBytes = _cdiFileData.Length;
      Sectors = new List<CdiSector>();
      for (int i = 0, j =0; i < _cdiFileData.Length; i += SECTOR_SIZE, j++)
      {
        var sectorData = _cdiFileData.Skip(i).Take(SECTOR_SIZE).ToArray();
        var sector = new CdiSector(sectorData, j);
        if (sector.GetSectorType() != CdiSectorType.Empty)
        {
          Sectors.Add(sector);
        }
      }
    }
  }
}
