using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.NET.Core;

namespace RGB.NET.Devices.Omen
{
    internal static class OmenMousepadLedMapping
    {
        public static LedMapping<int> Default { get; } = new()
        {
            [LedId.Mousepad1] = 1,
            [LedId.Mousepad2] = 2,
            [LedId.Mousepad3] = 3,
            [LedId.Mousepad4] = 4,
            [LedId.Mousepad5] = 5,
            [LedId.Mousepad6] = 6,
        };
    }
}
