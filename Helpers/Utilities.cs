using System.Drawing;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace OGLibCDi.Helpers
{
  public static class Utilities
  {
    public static bool MatchesSequence(BinaryReader reader, byte[] sequence)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        byte nextByte = reader.ReadByte();
        if (nextByte != sequence[i])
        {
          // rewind to the start of the sequence (including the byte we just read)
          reader.BaseStream.Position -= i + 1;
          return false;
        }
      }

      return true;
    }
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
      return source.Select((item, index) => (item, index));
    }

    public static Image CreateImage(byte[] imageBin, List<Color> colors, int Width, int Height, bool useTransparency = false)
    {
      // convert each byte of imageBin to an int and use that as an index into the colors array to create a 384 pixel wide image
      var image = new Bitmap(Width, Height);
      var graphics = Graphics.FromImage(image);
      var brush = new SolidBrush(Color.Black);
      var x = 0;
      var y = 0;
      var width = 1;
      var height = 1;
      if (useTransparency)
      {
        colors[0] = Color.Transparent;
      }
      foreach (var b in imageBin)
      {
        if (b >= colors.Count)
        {
          brush.Color = colors[0];
        }
        else
        {
          brush.Color = colors[b];
        }
        graphics.FillRectangle(brush, x, y, width, height);
        x += width;
        if (x >= Width)
        {
          x = 0;
          y += height;
        }
      }

      return image;

    }

    public static byte? PeekByte(this BinaryReader reader)
    {
      if (reader.BaseStream.Position >= reader.BaseStream.Length)
      {
        return null;
      }

      byte nextByte = reader.ReadByte();
      reader.BaseStream.Seek(-1, SeekOrigin.Current);
      return nextByte;
    }

  }
}
