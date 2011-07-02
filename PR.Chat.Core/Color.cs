using System;

namespace PR.Chat.Core
{
    [Serializable]
    public struct Color
    {
        private const byte RedShift     = 3;
        private const byte GreenShift   = 2;
        private const byte BlueShift    = 1;


        public byte Blue;
        public byte Green;
        public byte Red;

        public uint ToUInt32()
        {
            return
                (Convert.ToUInt32(Red) << RedShift) +
                (Convert.ToUInt32(Green) << GreenShift) +
                (Convert.ToUInt32(Blue) << BlueShift);
        }

        public static Color FromUInt32(uint colorNumber)
        {
            return new Color
                       {
                           Red = Convert.ToByte((colorNumber & (0xFF << RedShift)) >> RedShift),
                           Green = Convert.ToByte((colorNumber & (0xFF << GreenShift)) >> GreenShift),
                           Blue = Convert.ToByte((colorNumber & (0xFF << BlueShift)) >> BlueShift)
                       };
        }
    }
}