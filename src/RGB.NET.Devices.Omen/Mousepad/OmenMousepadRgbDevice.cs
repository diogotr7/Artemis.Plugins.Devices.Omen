using RGB.NET.Core;
using RGB.NET.Devices.Omen.Generic;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    internal class OmenMousepadRgbDevice : OmenRgbDevice
    {
        public OmenMousepadRgbDevice(OmenRgbDeviceInfo deviceInfo, IDeviceUpdateTrigger updateTrigger)
            : base(deviceInfo, new OmenMousepadUpdateQueue(updateTrigger, deviceInfo.Handle))
        {
            InitializeLayout();
        }

        protected virtual void InitializeLayout()
        {
            int x = 0;
            foreach (var item in OmenMousepadLedMapping.Default)
            {
                AddLed(item.ledId, new Point(x, 0), new Size(19), item.mapping);
                x += 20;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            //we need to dispose of the UpdateQueue before we close the handle.
            _OmenLightingSDK.OmenMousepadClose(DeviceInfo.Handle);
        }
    }
}