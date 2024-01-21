namespace OGLibCDi.Helpers
{
  public static class BitManipulationHelpers
  {
    public static byte BitReset(this byte value, int bitPosition)
    {
      return (byte)(value & ~(0b_0000_0001 << bitPosition));
    }
    
    public static bool IsBitSet(this byte value, int bitPosition)
    {
      return ((value >> bitPosition) & 0b_0000_0001) == 1;
    }
    
    public static byte SetBit(this byte value, int bitPosition)
    {
      return (byte)(value | (0b_0000_0001 << bitPosition));
    }

    public static byte UnsetBit(this byte value, int bitPosition)
    {
      return (byte)(value & (~(0x01 << bitPosition)));
    }
  }
}
