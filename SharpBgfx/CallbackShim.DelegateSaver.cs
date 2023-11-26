using System;
using System.Runtime.InteropServices;
using SharpBgfx.Bindings;

namespace SharpBgfx;

public unsafe partial struct CallbackShim
{
    // We're creating delegates to a user's interface methods; we're then converting those delegates
    // to native pointers and passing them into native code. If we don't save the references to the
    // delegates in managed land somewhere, the GC will think they're unreferenced and clean them
    // up, leaving native holding a bag of pointers into nowhere land.
    private class DelegateSaver
    {
        private readonly ICallbackHandler handler;
        private readonly ReportErrorHandler reportError;
        private readonly ReportDebugHandler reportDebug;
        private readonly ProfilerBeginHandler profilerBegin;
        private readonly ProfilerBeginHandler profilerBeginLiteral;
        private readonly ProfilerEndHandler profilerEnd;
        private readonly GetCachedSizeHandler getCachedSize;
        private readonly GetCacheEntryHandler getCacheEntry;
        private readonly SetCacheEntryHandler setCacheEntry;
        private readonly SaveScreenShotHandler saveScreenShot;
        private readonly CaptureStartedHandler captureStarted;
        private readonly CaptureFinishedHandler captureFinished;
        private readonly CaptureFrameHandler captureFrame;

        public DelegateSaver(ICallbackHandler handler, CallbackShim* shim)
        {
            this.handler = handler;
            reportError = ReportError;
            reportDebug = ReportDebug;
            profilerBegin = ProfilerBegin;
            profilerBeginLiteral = ProfilerBegin;
            profilerEnd = ProfilerEnd;
            getCachedSize = GetCachedSize;
            getCacheEntry = GetCacheEntry;
            setCacheEntry = SetCacheEntry;
            saveScreenShot = SaveScreenShot;
            captureStarted = CaptureStarted;
            captureFinished = CaptureFinished;
            captureFrame = CaptureFrame;

            shim->reportError = Marshal.GetFunctionPointerForDelegate(reportError);
            shim->reportDebug = Marshal.GetFunctionPointerForDelegate(reportDebug);
            shim->profilerBegin = Marshal.GetFunctionPointerForDelegate(profilerBegin);
            shim->profilerBeginLiteral = Marshal.GetFunctionPointerForDelegate(profilerBeginLiteral);
            shim->profilerEnd = Marshal.GetFunctionPointerForDelegate(profilerEnd);
            shim->getCachedSize = Marshal.GetFunctionPointerForDelegate(getCachedSize);
            shim->getCacheEntry = Marshal.GetFunctionPointerForDelegate(getCacheEntry);
            shim->setCacheEntry = Marshal.GetFunctionPointerForDelegate(setCacheEntry);
            shim->saveScreenShot = Marshal.GetFunctionPointerForDelegate(saveScreenShot);
            shim->captureStarted = Marshal.GetFunctionPointerForDelegate(captureStarted);
            shim->captureFinished = Marshal.GetFunctionPointerForDelegate(captureFinished);
            shim->captureFrame = Marshal.GetFunctionPointerForDelegate(captureFrame);
        }

        private void ReportError(IntPtr thisPtr, string fileName, ushort line, ErrorType errorType, string message)
        {
            handler.ReportError(fileName, line, errorType, message);
        }

        private void ReportDebug(IntPtr thisPtr, string fileName, ushort line, string format, IntPtr args)
        {
            handler.ReportDebug(fileName, line, format, args);
        }

        private void ProfilerBegin(IntPtr thisPtr, sbyte* name, int color, sbyte* filePath, ushort line)
        {
            handler.ProfilerBegin(new string(name), color, new string(filePath), line);
        }

        private void ProfilerEnd(IntPtr thisPtr)
        {
            handler.ProfilerEnd();
        }

        private int GetCachedSize(IntPtr thisPtr, long id)
        {
            return handler.GetCachedSize(id);
        }

        private bool GetCacheEntry(IntPtr thisPtr, long id, IntPtr data, int size)
        {
            return handler.GetCacheEntry(id, data, size);
        }

        private void SetCacheEntry(IntPtr thisPtr, long id, IntPtr data, int size)
        {
            handler.SetCacheEntry(id, data, size);
        }

        private void SaveScreenShot(IntPtr thisPtr, string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical)
        {
            handler.SaveScreenShot(path, width, height, pitch, data, size, flipVertical);
        }

        private void CaptureStarted(IntPtr thisPtr, int width, int height, int pitch, bgfx.TextureFormat format, bool flipVertical)
        {
            handler.CaptureStarted(width, height, pitch, format, flipVertical);
        }

        private void CaptureFinished(IntPtr thisPtr)
        {
            handler.CaptureFinished();
        }

        private void CaptureFrame(IntPtr thisPtr, IntPtr data, int size)
        {
            handler.CaptureFrame(data, size);
        }
    }
}
