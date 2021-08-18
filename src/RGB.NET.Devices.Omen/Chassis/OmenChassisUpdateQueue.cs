using RGB.NET.Core;
using RGB.NET.Devices.Omen.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.NET.Devices.Omen
{
    public class OmenChassisUpdateQueue : UpdateQueue
    {
        private readonly IntPtr _chassisHandle;

        public OmenChassisUpdateQueue(IDeviceUpdateTrigger trigger, IntPtr handle) : base(trigger)
        {
            _chassisHandle = handle;
        }

        protected override void Update(in ReadOnlySpan<(object key, Color color)> dataSet)
        {
            for (var i = 0; i < dataSet.Length; i++)
            {
                var (key, color) = dataSet[i];

                _OmenLightingSDK.OmenChassisSetStatic(_chassisHandle, (int)key, LightingColor.FromColor(color), IntPtr.Zero);
            }
        }
    }
}
