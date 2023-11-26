using System;
using System.Runtime.InteropServices;

namespace SharpBgfx.Bindings;

public unsafe partial class bgfx
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int bgfx_vsnprintf(sbyte* str, IntPtr count, [MarshalAs(UnmanagedType.LPStr)] string format, IntPtr argList);
}
