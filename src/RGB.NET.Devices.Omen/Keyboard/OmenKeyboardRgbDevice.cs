using RGB.NET.Core;
using RGB.NET.Devices.Omen.Generic;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    internal class OmenKeyboardRgbDevice : OmenRgbDevice
    {
        public OmenKeyboardRgbDevice(OmenRgbDeviceInfo deviceInfo, IDeviceUpdateTrigger updateTrigger)
            : base(deviceInfo, new OmenKeyboardUpdateQueue(updateTrigger, deviceInfo.Handle))
        {
            InitializeLayout();
        }

        protected virtual void InitializeLayout()
        {
            int x = 0;
            foreach (var item in Omen.LedMapping.Sequencer)
            {
                base.AddLed(item.ledId, new Point(x, 0), new Size(19), item.mapping);
                x += 20;
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            //we need to dispose of the UpdateQueue before we close the handle.
            _OmenLightingSDK.OmenKeyboardClose(DeviceInfo.Handle);
        }
    }
}