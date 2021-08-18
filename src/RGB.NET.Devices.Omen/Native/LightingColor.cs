using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGB.NET.Devices.Omen.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct LightingColor
    {
        public byte r;
        public byte g;
        public byte b;

        public static LightingColor FromColor(Color c)
        {
            return new LightingColor()
            {
                r = c.GetR(),
                g = c.GetG(),
                b = c.GetB()
            };
        }
    }
}
