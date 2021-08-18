using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RGB.NET.Devices.Omen.Native;

namespace RGB.NET.Devices.Omen
{
    public class OmenKeyboardUpdateQueue : UpdateQueue
    {
        private readonly IntPtr _keyboardHandle;

        public OmenKeyboardUpdateQueue(IDeviceUpdateTrigger trigger, IntPtr handle) : base(trigger)
        {
            _keyboardHandle = handle;
        }

        protected override void Update(in ReadOnlySpan<(object key, Color color)> dataSet)
        {
            StaticKeyEffect[] keys = new StaticKeyEffect[dataSet.Length];

            for (var i = 0; i< dataSet.Length; i++)
            {
                var (key, color) = dataSet[i];

                keys[i] = new StaticKeyEffect((int)key, color);
            }

            _OmenLightingSDK.OmenKeyboardSetStatic(_keyboardHandle, keys, keys.Length, IntPtr.Zero);
        }
    }
}
