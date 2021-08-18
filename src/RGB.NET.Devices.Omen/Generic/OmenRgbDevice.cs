using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.NET.Core;

namespace RGB.NET.Devices.Omen.Generic
{
    public abstract class OmenRgbDevice : AbstractRGBDevice<OmenRgbDeviceInfo>
    {
        protected OmenRgbDevice(OmenRgbDeviceInfo deviceInfo, IUpdateQueue updateQueue)
                                : base(deviceInfo, updateQueue)
        { }
    }
}
