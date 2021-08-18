using RGB.NET.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RGB.NET.Devices.Omen.Native
{
    internal static class _OmenLightingSDK
    {
        #region Libary Management

        private static IntPtr _dllHandle = IntPtr.Zero;

        /// <summary>
        /// Reloads the SDK.
        /// </summary>
        internal static void Reload()
        {
            UnloadOmenSDK();
            LoadOmenSDK();
        }

        private static void LoadOmenSDK()
        {
            if (_dllHandle != IntPtr.Zero) return;

            // HACK: Load library at runtime to support both, x86 and x64 with one managed dll
            List<string> possiblePathList = Environment.Is64BitProcess ? OmenDeviceProvider.PossibleX64NativePaths : OmenDeviceProvider.PossibleX86NativePaths;
            string? dllPath = possiblePathList.FirstOrDefault(File.Exists);
            if (dllPath == null) throw new RGBDeviceException($"Can't find the OmenLightingSDK at one of the expected locations:\r\n '{string.Join("\r\n", possiblePathList.Select(Path.GetFullPath))}'");

            _dllHandle = LoadLibrary(dllPath);
            if (_dllHandle == IntPtr.Zero) throw new RGBDeviceException($"Corsair LoadLibrary failed with error code {Marshal.GetLastWin32Error()}");

            _omenSpeakerOpenPointer = (OmenSpeakerOpenPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Speaker_Open"), typeof(OmenSpeakerOpenPointer));
            _omenSpeakerClosePointer = (OmenSpeakerClosePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Speaker_Close"), typeof(OmenSpeakerClosePointer));
            _omenSpeakerSetStaticPointer = (OmenSpeakerSetStaticPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Speaker_SetStatic"), typeof(OmenSpeakerSetStaticPointer));

            _omenMousepadOpenPointer = (OmenMousepadOpenPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_MousePad_Open"), typeof(OmenMousepadOpenPointer));
            _omenMousepadOpenByNamePointer = (OmenMousepadOpenByNamePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_MousePad_OpenByName"), typeof(OmenMousepadOpenByNamePointer));
            _omenMousepadClosePointer = (OmenMousepadClosePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_MousePad_Close"), typeof(OmenMousepadClosePointer));
            _omenMousepadSetStaticPointer = (OmenMousepadSetStaticPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_MousePad_SetStatic"), typeof(OmenMousepadSetStaticPointer));

            _omenMouseOpenPointer = (OmenMouseOpenPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Mouse_Open"), typeof(OmenMouseOpenPointer));
            _omenMouseOpenByNamePointer = (OmenMouseOpenByNamePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Mouse_OpenByName"), typeof(OmenMouseOpenByNamePointer));
            _omenMouseClosePointer = (OmenMouseClosePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Mouse_Close"), typeof(OmenMouseClosePointer));
            _omenMouseSetStaticPointer = (OmenMouseSetStaticPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Mouse_SetStatic"), typeof(OmenMouseSetStaticPointer));

            _omenKeyboardOpenPointer = (OmenKeyboardOpenPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Keyboard_Open"), typeof(OmenKeyboardOpenPointer));
            _omenKeyboardOpenByNamePointer = (OmenKeyboardOpenByNamePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Keyboard_OpenByName"), typeof(OmenKeyboardOpenByNamePointer));
            _omenKeyboardClosePointer = (OmenKeyboardClosePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Keyboard_Close"), typeof(OmenKeyboardClosePointer));
            _omenKeyboardSetStaticPointer = (OmenKeyboardSetStaticPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Keyboard_SetStatic"), typeof(OmenKeyboardSetStaticPointer));

            _omenChassisOpenPointer = (OmenChassisOpenPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Chassis_Open"), typeof(OmenChassisOpenPointer));
            _omenChassisClosePointer = (OmenChassisClosePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Chassis_Close"), typeof(OmenChassisClosePointer));
            _omenChassisSetStaticPointer = (OmenChassisSetStaticPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "OmenLighting_Chassis_SetStatic"), typeof(OmenChassisSetStaticPointer));
        }

        internal static void UnloadOmenSDK()
        {
            if (_dllHandle == IntPtr.Zero) return;

            // ReSharper disable once EmptyEmbeddedStatement - DarthAffe 20.02.2016: We might need to reduce the internal reference counter more than once to set the library free
            while (FreeLibrary(_dllHandle)) ;
            _dllHandle = IntPtr.Zero;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr dllHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr dllHandle, string name);

        #endregion

        #region SDK-METHODS

        #region Pointers

        private static OmenSpeakerOpenPointer? _omenSpeakerOpenPointer;
        private static OmenSpeakerClosePointer? _omenSpeakerClosePointer;
        private static OmenSpeakerSetStaticPointer? _omenSpeakerSetStaticPointer;

        private static OmenMousepadOpenPointer? _omenMousepadOpenPointer;
        private static OmenMousepadOpenByNamePointer? _omenMousepadOpenByNamePointer;
        private static OmenMousepadClosePointer? _omenMousepadClosePointer;
        private static OmenMousepadSetStaticPointer? _omenMousepadSetStaticPointer;

        private static OmenMouseOpenPointer? _omenMouseOpenPointer;
        private static OmenMouseOpenByNamePointer? _omenMouseOpenByNamePointer;
        private static OmenMouseClosePointer? _omenMouseClosePointer;
        private static OmenMouseSetStaticPointer? _omenMouseSetStaticPointer;

        private static OmenKeyboardOpenPointer? _omenKeyboardOpenPointer;
        private static OmenKeyboardOpenByNamePointer? _omenKeyboardOpenByNamePointer;
        private static OmenKeyboardClosePointer? _omenKeyboardClosePointer;
        private static OmenKeyboardSetStaticPointer? _omenKeyboardSetStaticPointer;

        private static OmenChassisOpenPointer? _omenChassisOpenPointer;
        private static OmenChassisClosePointer? _omenChassisClosePointer;
        private static OmenChassisSetStaticPointer? _omenChassisSetStaticPointer;

        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenSpeakerOpenPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OmenSpeakerClosePointer(IntPtr hSpeaker);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OmenSpeakerSetStaticPointer(IntPtr hSpeaker, LightingColor color, IntPtr property);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenMousepadOpenPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenMousepadOpenByNamePointer([MarshalAs(UnmanagedType.LPWStr)] string deviceName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OmenMousepadClosePointer(IntPtr hMousePad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OmenMousepadSetStaticPointer(IntPtr hMousePad, int zone, LightingColor color, IntPtr property);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenMouseOpenPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenMouseOpenByNamePointer([MarshalAs(UnmanagedType.LPWStr)] string deviceName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OmenMouseClosePointer(IntPtr hMouse);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OmenMouseSetStaticPointer(IntPtr hMouse, int zone, LightingColor color, IntPtr property);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenKeyboardOpenPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenKeyboardOpenByNamePointer([MarshalAs(UnmanagedType.LPWStr)] string deviceName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OmenKeyboardClosePointer(IntPtr hKeyboard);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OmenKeyboardSetStaticPointer(IntPtr hKeyboard, StaticKeyEffect[] staticEffect, int count, IntPtr keyboardLightingEffectProperty);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OmenChassisOpenPointer();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OmenChassisClosePointer(IntPtr hChassis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OmenChassisSetStaticPointer(IntPtr hChassis, int zone, LightingColor color, IntPtr property);
        #endregion

        internal static IntPtr OmenSpeakerOpen()
            => (_omenSpeakerOpenPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke();

        internal static void OmenSpeakerClose(IntPtr hSpeaker)
            => (_omenSpeakerClosePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hSpeaker);

        internal static void OmenSpeakerSetStatic(IntPtr hSpeaker, LightingColor color, IntPtr property)
            => (_omenSpeakerSetStaticPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hSpeaker, color, property);


        internal static IntPtr OmenMousepadOpen()
            => (_omenMousepadOpenPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke();

        internal static IntPtr OmenMousepadOpenByName(string deviceName)
            => (_omenMousepadOpenByNamePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(deviceName);

        internal static void OmenMousepadClose(IntPtr hMousePad)
            => (_omenMousepadClosePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hMousePad);

        internal static void OmenMousepadSetStatic(IntPtr hMousePad, int zone, LightingColor color, IntPtr property)
            => (_omenMousepadSetStaticPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hMousePad, zone, color, property);


        internal static IntPtr OmenMouseOpen()
            => (_omenMouseOpenPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke();

        internal static IntPtr OmenMouseOpenByName(string deviceName)
            => (_omenMouseOpenByNamePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(deviceName);

        internal static void OmenMouseClose(IntPtr hMouse)
            => (_omenMouseClosePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hMouse);

        internal static void OmenMouseSetStatic(IntPtr hMouse, int zone, LightingColor color, IntPtr property)
            => (_omenMouseSetStaticPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hMouse, zone, color, property);


        internal static IntPtr OmenKeyboardOpen()
            => (_omenKeyboardOpenPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke();

        internal static IntPtr OmenKeyboardOpenByName(string deviceName)
            => (_omenKeyboardOpenByNamePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(deviceName);

        internal static void OmenKeyboardClose(IntPtr hKeyboard)
            => (_omenKeyboardClosePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hKeyboard);

        internal static void OmenKeyboardSetStatic(IntPtr hKeyboard, StaticKeyEffect[] staticEffect, int count, IntPtr keyboardLightingEffectProperty)
            => (_omenKeyboardSetStaticPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hKeyboard, staticEffect, count, keyboardLightingEffectProperty);


        internal static IntPtr OmenChassisOpen()
            => (_omenChassisOpenPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke();

        internal static void OmenChassisClose(IntPtr hSpeaker)
            => (_omenChassisClosePointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hSpeaker);

        internal static void OmenChassisSetStatic(IntPtr hChassis, int zone, LightingColor color, IntPtr property)
            => (_omenChassisSetStaticPointer ?? throw new RGBDeviceException("OmenLightingSDK is not initialized")).Invoke(hChassis, zone, color, property);
        #endregion
    }
}
