using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpBgfx.Bindings;

namespace SharpBgfx;

public unsafe partial struct CallbackShim
{
    private static Dictionary<IntPtr, DelegateSaver> Shims = new Dictionary<IntPtr, DelegateSaver>();

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

    /// <summary>
    /// Creates a shim that can be passed into <see cref="bgfx.Init.callback"/>.
    /// <para/>
    /// Pointer must be freed using <see cref="Free"/>.
    /// </summary>
    public static IntPtr Create(ICallbackHandler handler)
    {
        if (handler == null)
        {
            return IntPtr.Zero;
        }

        var shimMemory = Marshal.AllocHGlobal(Marshal.SizeOf<CallbackShim>());
        var shim = (CallbackShim*)shimMemory;
        var savedDelegates = new DelegateSaver(handler, shim);

        // The shim uses the unnecessary ctor slot to act as a vtbl pointer to itself,
        // so that the same block of memory can act as both bgfx_callback_interface_t and bgfx_callback_vtbl_t
        shim->vtbl = shimMemory + IntPtr.Size;


        // Cache the data so we can free it later
        Shims.Add(shimMemory, savedDelegates);

        return shimMemory;
    }

    /// <summary>
    /// Frees a shim created by <see cref="Create"/>.
    /// </summary>
    public static void Free(IntPtr shim)
    {
        if (Shims.Remove(shim, out var savedDelegates))
        {
            Marshal.FreeHGlobal(shim);
        }
    }
}
