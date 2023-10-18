using Artemis.Core.DeviceProviders;
using Artemis.Core.Services;
using RGB.NET.Devices.Omen;
using System.IO;
using RGB.NET.Core;

namespace Artemis.Plugins.Devices.Omen
{
    public class OmenDeviceProvider : DeviceProvider
    {
        private readonly IDeviceService _deviceService;

        public override IRGBDeviceProvider RgbDeviceProvider => RGB.NET.Devices.Omen.OmenDeviceProvider.Instance;

        public OmenDeviceProvider(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public override void Enable()
        {
            RGB.NET.Devices.Omen.OmenDeviceProvider.PossibleX64NativePaths.Add(Path.Combine(Plugin.Directory.FullName, "x64", "OmenLightingSDK.dll"));

            _deviceService.AddDeviceProvider(this);
        }

        public override void Disable()
        {
            _deviceService.RemoveDeviceProvider(this);
            RgbDeviceProvider.Dispose();
        }
    }
}