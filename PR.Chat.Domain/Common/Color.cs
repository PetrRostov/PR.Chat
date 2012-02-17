using System;
using PR.Chat.Infrastructure;

namespace PR.Chat.Domain
{
    [Serializable]
    public struct Color : IValueObject<Color>
    {
        private const byte RedShift         = 3;
        private const byte GreenShift       = 2;
        private const byte BlueShift        = 1;
        private const string ColorFormat    = "Color: #{0}{1}{2}";


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



        public bool SameValueAs(Color other)
        {
            return Red == other.Red && Blue == other.Blue && Green == other.Green;
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return SameValueAs((Color) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(ColorFormat, Red, Green, Blue);
        }
    }
}