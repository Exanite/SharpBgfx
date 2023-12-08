using System;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a buffer that can contain indirect drawing commands created and processed entirely on the GPU.
/// </summary>
public struct IndirectBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly IndirectBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="IndirectBuffer"/> struct.
    /// </summary>
    /// <param name="size">The number of commands that can fit in the buffer.</param>
    public IndirectBuffer(int size)
    {
        handle = NativeMethods.bgfx_create_indirect_buffer(size);
    }

    /// <summary>
    /// Releases the index buffer.
    /// </summary>
    public void Dispose()
    {
        NativeMethods.bgfx_destroy_indirect_buffer(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
