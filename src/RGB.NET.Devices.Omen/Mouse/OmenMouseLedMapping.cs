using RGB.NET.Core;

namespace RGB.NET.Devices.Omen
{
    internal static class OmenMouseLedMapping
    {
        public static LedMapping<int> Default { get; } = new()
        {
            [LedId.Mouse1] = 0,
            [LedId.Mouse2] = 1
        };
    }
}