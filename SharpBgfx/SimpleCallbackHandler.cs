using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpBgfx.Bindings;

namespace SharpBgfx;

public unsafe class SimpleCallbackHandler : ICallbackHandler
{
    public void ReportError(string fileName, int line, ErrorType errorType, string message)
    {
        if (errorType == ErrorType.DebugCheck)
        {
            Debug.Write(message);
        }
        else
        {
            Debug.Write(string.Format("{0} ({1})  {2}: {3}", fileName, line, errorType, message));
            Debugger.Break();
            Environment.Exit(1);
        }
    }

    public void ReportDebug(string fileName, int line, string format, IntPtr args)
    {
        var buffer = stackalloc sbyte[1024];
        bgfx.bgfx_vsnprintf(buffer, new IntPtr(1024), format, args);
        Debug.Write(Marshal.PtrToStringAnsi(new IntPtr(buffer)));
    }

    public void ProfilerBegin(string name, int color, string filePath, int line) {}
    public void ProfilerEnd() {}

    public int GetCachedSize(long id) => 0;
    public bool GetCacheEntry(long id, IntPtr data, int size) => false;
    public void SetCacheEntry(long id, IntPtr data, int size) {}

    public void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical) {}

    public void CaptureStarted(int width, int height, int pitch, bgfx.TextureFormat format, bool flipVertical) {}
    public void CaptureFrame(IntPtr data, int size) {}
    public void CaptureFinished() {}
}
