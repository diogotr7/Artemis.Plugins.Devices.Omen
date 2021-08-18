using RGB.NET.Core;
using RGB.NET.Devices.Omen.Generic;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    internal class OmenSpeakerRgbDevice : OmenRgbDevice
    {
        public OmenSpeakerRgbDevice(OmenRgbDeviceInfo deviceInfo, IDeviceUpdateTrigger updateTrigger)
            : base(deviceInfo, new OmenSpeakerUpdateQueue(updateTrigger, deviceInfo.Handle))
        {
            InitializeLayout();
        }

        protected virtual void InitializeLayout()
        {
            AddLed(LedId.Speaker1, new Point(0, 0), new Size(20));
        }

        public override void Dispose()
        {
            base.Dispose();

            //we need to dispose of the UpdateQueue before we close the handle.
            _OmenLightingSDK.OmenSpeakerClose(DeviceInfo.Handle);
        }
    }
}