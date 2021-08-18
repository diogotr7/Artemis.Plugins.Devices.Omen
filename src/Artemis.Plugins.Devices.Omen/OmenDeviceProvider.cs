using Artemis.Core.DeviceProviders;
using Artemis.Core.Services;
using RGB.NET.Devices.Omen;
using System.IO;

namespace Artemis.Plugins.Devices.Omen
{
    public class OmenDeviceProvider : DeviceProvider
    {
        private readonly IRgbService _rgbService;

        public OmenDeviceProvider(IRgbService rgbService) : base(RGB.NET.Devices.Omen.OmenDeviceProvider.Instance)
        {
            _rgbService = rgbService;
        }

        public override void Enable()
        {
            RGB.NET.Devices.Omen.OmenDeviceProvider.PossibleX64NativePaths.Add(Path.Combine(Plugin.Directory.FullName, "x64", "OmenLightingSDK.dll"));

            _rgbService.AddDeviceProvider(RgbDeviceProvider);
        }

        public override void Disable()
        {
            _rgbService.RemoveDeviceProvider(RgbDeviceProvider);
            RgbDeviceProvider.Dispose();
        }
    }
}