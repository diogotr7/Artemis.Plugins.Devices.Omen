using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.NET.Core;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    public class OmenSpeakerUpdateQueue : UpdateQueue
    {
        private readonly IntPtr _speakerHandle;

        public OmenSpeakerUpdateQueue(IDeviceUpdateTrigger trigger, IntPtr handle) : base(trigger)
        {
            _speakerHandle = handle;
        }

        protected override bool Update(in ReadOnlySpan<(object key, Color color)> dataSet)
        {
            var color = dataSet[0].color;

            _OmenLightingSDK.OmenSpeakerSetStatic(_speakerHandle, LightingColor.FromColor(color), IntPtr.Zero);
            
            return true;
        }
    }
}
