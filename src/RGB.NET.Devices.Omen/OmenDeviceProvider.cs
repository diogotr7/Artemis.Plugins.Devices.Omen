using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using RGB.NET.Devices.Omen.Native;
using RGB.NET.Devices.Omen.Generic;

namespace RGB.NET.Devices.Omen
{
    public class OmenDeviceProvider : AbstractRGBDeviceProvider
    {
        private static OmenDeviceProvider? _instance;
        public static OmenDeviceProvider Instance => _instance ?? new OmenDeviceProvider();

        public OmenDeviceProvider()
        {
            if (_instance != null) throw new InvalidOperationException($"There can be only one instance of type {nameof(OmenDeviceProvider)}");
            _instance = this;
        }

        public static List<string> PossibleX86NativePaths { get; } = new();
        public static List<string> PossibleX64NativePaths { get; } = new() { "x64/OmenLightingSDK.dll" };

        #region Overrides of AbstractRGBDeviceProvider

        protected override void InitializeSDK()
        {
            _OmenLightingSDK.Reload();
        }

        /// <inheritdoc />
        protected override IEnumerable<IRGBDevice> LoadDevices()
        {
            var keyboard = _OmenLightingSDK.OmenKeyboardOpen();
            if (keyboard != IntPtr.Zero)
                yield return new OmenKeyboardRgbDevice(new OmenRgbDeviceInfo(RGBDeviceType.Keyboard, "Keyboard", keyboard), GetUpdateTrigger());

            var mouse = _OmenLightingSDK.OmenMouseOpen();
            if (mouse != IntPtr.Zero)
                yield return new OmenMouseRgbDevice(new OmenRgbDeviceInfo(RGBDeviceType.Mouse, "Mouse", mouse), GetUpdateTrigger());

            var mousepad = _OmenLightingSDK.OmenMousepadOpen();
            if (mousepad != IntPtr.Zero)
                yield return new OmenMousepadRgbDevice(new OmenRgbDeviceInfo(RGBDeviceType.Mousepad, "Mousepad", mousepad), GetUpdateTrigger());

            var speaker = _OmenLightingSDK.OmenSpeakerOpen();
            if (speaker != IntPtr.Zero)
                yield return new OmenSpeakerRgbDevice(new OmenRgbDeviceInfo(RGBDeviceType.Speaker, "Speaker", speaker), GetUpdateTrigger());

            var chassis = _OmenLightingSDK.OmenChassisOpen();
            if (chassis != IntPtr.Zero)
                yield return new OmenChassisRgbDevice(new OmenRgbDeviceInfo(RGBDeviceType.Unknown, "Chassis", chassis), GetUpdateTrigger());
        }

        public override void Dispose()
        {
            base.Dispose();

            try { _OmenLightingSDK.UnloadOmenSDK(); }
            catch { /* at least we tried */ }

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}