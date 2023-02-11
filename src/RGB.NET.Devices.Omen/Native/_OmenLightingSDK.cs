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

            List<string> possiblePathList = Environment.Is64BitProcess ? OmenDeviceProvider.PossibleX64NativePaths : OmenDeviceProvider.PossibleX86NativePaths;
            string? dllPath = possiblePathList.FirstOrDefault(File.Exists);
            if (dllPath == null) throw new RGBDeviceException($"Can't find the OmenLightingSDK at one of the expected locations:\r\n '{string.Join("\r\n", possiblePathList.Select(Path.GetFullPath))}'");

            if (!NativeLibrary.TryLoad(dllPath, out _dllHandle))
                throw new RGBDeviceException($"Omen LoadLibrary failed with error code {Marshal.GetLastPInvokeError()}");

            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Speaker_Open", out _omenSpeakerOpenPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Speaker_Open not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Speaker_Close", out _omenSpeakerClosePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Speaker_Close not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Speaker_SetStatic", out _omenSpeakerSetStaticPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Speaker_SetStatic not found");
            
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_MousePad_Open", out _omenMousepadOpenPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_MousePad_Open not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_MousePad_OpenByName", out _omenMousepadOpenByNamePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_MousePad_OpenByName not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_MousePad_Close", out _omenMousepadClosePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_MousePad_Close not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_MousePad_SetStatic", out _omenMousepadSetStaticPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_MousePad_SetStatic not found");
            
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Mouse_Open", out _omenMouseOpenPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Mouse_Open not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Mouse_OpenByName", out _omenMouseOpenByNamePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Mouse_OpenByName not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Mouse_Close", out _omenMouseClosePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Mouse_Close not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Mouse_SetStatic", out _omenMouseSetStaticPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Mouse_SetStatic not found");
            
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Keyboard_Open", out _omenKeyboardOpenPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Keyboard_Open not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Keyboard_OpenByName", out _omenKeyboardOpenByNamePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Keyboard_OpenByName not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Keyboard_Close", out _omenKeyboardClosePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Keyboard_Close not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Keyboard_SetStatic", out _omenKeyboardSetStaticPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Keyboard_SetStatic not found");
            
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Chassis_Open", out _omenChassisOpenPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Chassis_Open not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Chassis_OpenByName", out _omenChassisOpenByNamePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Chassis_OpenByName not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Chassis_Close", out _omenChassisClosePointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Chassis_Close not found");
            if (!NativeLibrary.TryGetExport(_dllHandle, "OmenLighting_Chassis_SetStatic", out _omenChassisSetStaticPointer)) throw new RGBDeviceException($"Failed to load omen function OmenLighting_Chassis_SetStatic not found");
        }

        internal static void UnloadOmenSDK()
        {
            if (_dllHandle == IntPtr.Zero) return;
            
            _omenSpeakerOpenPointer = IntPtr.Zero;
            _omenSpeakerClosePointer = IntPtr.Zero;
            _omenSpeakerSetStaticPointer = IntPtr.Zero;
            
            _omenMousepadOpenPointer = IntPtr.Zero;
            _omenMousepadOpenByNamePointer = IntPtr.Zero;
            _omenMousepadClosePointer = IntPtr.Zero;
            _omenMousepadSetStaticPointer = IntPtr.Zero;
            
            _omenMouseOpenPointer = IntPtr.Zero;
            _omenMouseOpenByNamePointer = IntPtr.Zero;
            _omenMouseClosePointer = IntPtr.Zero;
            _omenMouseSetStaticPointer = IntPtr.Zero;
            
            _omenKeyboardOpenPointer = IntPtr.Zero;
            _omenKeyboardOpenByNamePointer = IntPtr.Zero;
            _omenKeyboardClosePointer = IntPtr.Zero;
            _omenKeyboardSetStaticPointer = IntPtr.Zero;
            
            _omenChassisOpenPointer = IntPtr.Zero;
            _omenChassisOpenByNamePointer = IntPtr.Zero;
            _omenChassisClosePointer = IntPtr.Zero;
            _omenChassisSetStaticPointer = IntPtr.Zero;
            
            NativeLibrary.Free(_dllHandle);
            _dllHandle = IntPtr.Zero;
        }

        #endregion

        #region SDK-METHODS

        #region Pointers

        private static IntPtr _omenSpeakerOpenPointer;
        private static IntPtr _omenSpeakerClosePointer;
        private static IntPtr _omenSpeakerSetStaticPointer;

        private static IntPtr _omenMousepadOpenPointer;
        private static IntPtr _omenMousepadOpenByNamePointer;
        private static IntPtr _omenMousepadClosePointer;
        private static IntPtr _omenMousepadSetStaticPointer;

        private static IntPtr _omenMouseOpenPointer;
        private static IntPtr _omenMouseOpenByNamePointer;
        private static IntPtr _omenMouseClosePointer;
        private static IntPtr _omenMouseSetStaticPointer;

        private static IntPtr _omenKeyboardOpenPointer;
        private static IntPtr _omenKeyboardOpenByNamePointer;
        private static IntPtr _omenKeyboardClosePointer;
        private static IntPtr _omenKeyboardSetStaticPointer;

        private static IntPtr _omenChassisOpenPointer;
        private static IntPtr _omenChassisOpenByNamePointer;
        private static IntPtr _omenChassisClosePointer;
        private static IntPtr _omenChassisSetStaticPointer;

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
        
        internal static unsafe IntPtr OmenSpeakerOpen() => ((delegate* unmanaged[Cdecl]<IntPtr>)ThrowIfZero(_omenSpeakerOpenPointer))();
        
        internal static unsafe void OmenSpeakerClose(IntPtr hSpeaker) => ((delegate* unmanaged[Cdecl]<IntPtr, void>)ThrowIfZero(_omenSpeakerClosePointer))(hSpeaker);
        
        internal static unsafe int OmenSpeakerSetStatic(IntPtr hSpeaker, LightingColor color, IntPtr property) => ((delegate* unmanaged[Cdecl]<IntPtr, LightingColor, IntPtr, int>)ThrowIfZero(_omenSpeakerSetStaticPointer))(hSpeaker, color, property);
        
        internal static unsafe IntPtr OmenMousepadOpen() => ((delegate* unmanaged[Cdecl]<IntPtr>)ThrowIfZero(_omenMousepadOpenPointer))();
        
        internal static unsafe IntPtr OmenMousepadOpenByName(string deviceName) => ((delegate* unmanaged[Cdecl]<string, IntPtr>)ThrowIfZero(_omenMousepadOpenByNamePointer))(deviceName);
        
        internal static unsafe void OmenMousepadClose(IntPtr hMousePad) => ((delegate* unmanaged[Cdecl]<IntPtr, void>)ThrowIfZero(_omenMousepadClosePointer))(hMousePad);
        
        internal static unsafe int OmenMousepadSetStatic(IntPtr hMousePad, int zone, LightingColor color, IntPtr property) => ((delegate* unmanaged[Cdecl]<IntPtr, int, LightingColor, IntPtr, int>)ThrowIfZero(_omenMousepadSetStaticPointer))(hMousePad, zone, color, property);
        
        internal static unsafe IntPtr OmenMouseOpen() => ((delegate* unmanaged[Cdecl]<IntPtr>)ThrowIfZero(_omenMouseOpenPointer))();
        
        internal static unsafe IntPtr OmenMouseOpenByName(string deviceName) => ((delegate* unmanaged[Cdecl]<string, IntPtr>)ThrowIfZero(_omenMouseOpenByNamePointer))(deviceName);
        
        internal static unsafe void OmenMouseClose(IntPtr hMouse) => ((delegate* unmanaged[Cdecl]<IntPtr, void>)ThrowIfZero(_omenMouseClosePointer))(hMouse);
        
        internal static unsafe int OmenMouseSetStatic(IntPtr hMouse, int zone, LightingColor color, IntPtr property) => ((delegate* unmanaged[Cdecl]<IntPtr, int, LightingColor, IntPtr, int>)ThrowIfZero(_omenMouseSetStaticPointer))(hMouse, zone, color, property);
        
        internal static unsafe IntPtr OmenKeyboardOpen() => ((delegate* unmanaged[Cdecl]<IntPtr>)ThrowIfZero(_omenKeyboardOpenPointer))();
        
        internal static unsafe IntPtr OmenKeyboardOpenByName(string deviceName) => ((delegate* unmanaged[Cdecl]<string, IntPtr>)ThrowIfZero(_omenKeyboardOpenByNamePointer))(deviceName);
        
        internal static unsafe void OmenKeyboardClose(IntPtr hKeyboard) => ((delegate* unmanaged[Cdecl]<IntPtr, void>)ThrowIfZero(_omenKeyboardClosePointer))(hKeyboard);
        
        internal static unsafe int OmenKeyboardSetStatic(IntPtr hKeyboard, StaticKeyEffect[] staticEffect, int count, IntPtr keyboardLightingEffectProperty) => ((delegate* unmanaged[Cdecl]<IntPtr, StaticKeyEffect[], int, IntPtr, int>)ThrowIfZero(_omenKeyboardSetStaticPointer))(hKeyboard, staticEffect, count, keyboardLightingEffectProperty);
        
        internal static unsafe IntPtr OmenChassisOpen() => ((delegate* unmanaged[Cdecl]<IntPtr>)ThrowIfZero(_omenChassisOpenPointer))();
        
        internal static unsafe void OmenChassisClose(IntPtr hChassis) => ((delegate* unmanaged[Cdecl]<IntPtr, void>)ThrowIfZero(_omenChassisClosePointer))(hChassis);
        
        internal static unsafe int OmenChassisSetStatic(IntPtr hChassis, int zone, LightingColor color, IntPtr property) => ((delegate* unmanaged[Cdecl]<IntPtr, int, LightingColor, IntPtr, int>)ThrowIfZero(_omenChassisSetStaticPointer))(hChassis, zone, color, property);

        private static IntPtr ThrowIfZero(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) throw new RGBDeviceException("The Omen is not initialized.");
            return ptr;
        }
        #endregion
    }
}
