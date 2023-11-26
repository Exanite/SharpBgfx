using System;
using System.Runtime.InteropServices;
using System.Security;
using SharpBgfx.Bindings;

namespace SharpBgfx;

internal unsafe partial struct CallbackShim
{
    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    internal delegate void ReportErrorHandler(IntPtr thisPtr, string filePath, ushort line, ErrorType error, string message);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    internal delegate void ReportDebugHandler(IntPtr thisPtr, string filePath, ushort line, string format, IntPtr args);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    internal delegate void ProfilerBeginHandler(IntPtr thisPtr, sbyte* name, int abgr, sbyte* filePath, ushort line);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void ProfilerEndHandler(IntPtr thisPtr);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int GetCachedSizeHandler(IntPtr thisPtr, long id);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate bool GetCacheEntryHandler(IntPtr thisPtr, long id, IntPtr data, int size);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void SetCacheEntryHandler(IntPtr thisPtr, long id, IntPtr data, int size);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    internal delegate void SaveScreenShotHandler(IntPtr thisPtr, string filePath, int width, int height, int pitch, IntPtr data, int size, [MarshalAs(UnmanagedType.U1)] bool flipVertical);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void CaptureStartedHandler(IntPtr thisPtr, int width, int height, int pitch, bgfx.TextureFormat format, [MarshalAs(UnmanagedType.U1)] bool flipVertical);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void CaptureFinishedHandler(IntPtr thisPtr);

    [SuppressUnmanagedCodeSecurity]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void CaptureFrameHandler(IntPtr thisPtr, IntPtr data, int size);
}
