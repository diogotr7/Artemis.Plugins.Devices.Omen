using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.NET.Devices.Omen.Generic
{
    public class OmenRgbDeviceInfo : IRGBDeviceInfo
    {
        internal IntPtr Handle { get; }

        public RGBDeviceType DeviceType { get; }

        public string DeviceName { get; }

        public string Manufacturer => "Omen";

        public string Model { get; }

        public object? LayoutMetadata { get; set; }

        public OmenRgbDeviceInfo(RGBDeviceType type, string model, IntPtr handle)
        {
            DeviceType = type;
            Model = model;
            DeviceName = DeviceHelper.CreateDeviceName(Manufacturer, Model);
            Handle = handle;
        }
    }
}
