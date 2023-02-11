using RGB.NET.Core;
using RGB.NET.Devices.Omen.Generic;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    internal class OmenChassisRgbDevice : OmenRgbDevice
    {
        public OmenChassisRgbDevice(OmenRgbDeviceInfo deviceInfo, IDeviceUpdateTrigger updateTrigger)
            : base(deviceInfo, new OmenChassisUpdateQueue(updateTrigger, deviceInfo.Handle))
        {
            InitializeLayout();
        }

        protected virtual void InitializeLayout()
        {
            //Note: Chassis LedId 0 means all LEDS. Start from 1.
            for(var i = 1; i < 10; i++)
            {
                AddLed(LedId.Custom1 + i, new Point(i * 20, 0), new Size(19), i);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            //we need to dispose of the UpdateQueue before we close the handle.
            _OmenLightingSDK.OmenChassisClose(DeviceInfo.Handle);
        }
    }
}