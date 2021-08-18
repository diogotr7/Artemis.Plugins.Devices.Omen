using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGB.NET.Devices.Omen.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StaticKeyEffect
    {
        [MarshalAs(UnmanagedType.Struct)]
        public LightingColor Color;
        public int Key;

        public StaticKeyEffect(int key, Color color)
        {
            Color = LightingColor.FromColor(color);
            Key = key;
        }
    }
}
