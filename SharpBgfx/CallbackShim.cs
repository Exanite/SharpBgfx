using System;
using System.Runtime.InteropServices;

namespace SharpBgfx;

internal unsafe partial struct CallbackShim
{
    private static IntPtr ShimMemory;
    private static DelegateSaver SavedDelegates;

    private IntPtr vtbl;
    private IntPtr reportError;
    private IntPtr reportDebug;
    private IntPtr profilerBegin;
    private IntPtr profilerBeginLiteral;
    private IntPtr profilerEnd;
    private IntPtr getCachedSize;
    private IntPtr getCacheEntry;
    private IntPtr setCacheEntry;
    private IntPtr saveScreenShot;
    private IntPtr captureStarted;
    private IntPtr captureFinished;
    private IntPtr captureFrame;

    public static IntPtr CreateShim(ICallbackHandler handler)
    {
        if (handler == null)
        {
            return IntPtr.Zero;
        }

        if (SavedDelegates != null)
        {
            throw new InvalidOperationException("Callbacks should only be initialized once; bgfx can only deal with one set at a time.");
        }

        var memory = Marshal.AllocHGlobal(Marshal.SizeOf<CallbackShim>());
        var shim = (CallbackShim*)memory;
        var saver = new DelegateSaver(handler, shim);

        // the shim uses the unnecessary ctor slot to act as a vtbl pointer to itself,
        // so that the same block of memory can act as both bgfx_callback_interface_t and bgfx_callback_vtbl_t
        shim->vtbl = memory + IntPtr.Size;

        // cache the data so we can free it later
        ShimMemory = memory;
        SavedDelegates = saver;

        return memory;
    }

    public static void FreeShim()
    {
        if (SavedDelegates == null)
        {
            return;
        }

        SavedDelegates = null;
        Marshal.FreeHGlobal(ShimMemory);
    }
}
