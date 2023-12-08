using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a static index buffer.
/// </summary>
public unsafe struct IndexBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly IndexBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexBuffer"/> struct.
    /// </summary>
    /// <param name="memory">The 16-bit index data used to populate the buffer.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public IndexBuffer(MemoryBlock memory, BufferFlags flags = BufferFlags.None)
    {
        handle = bgfx.create_index_buffer(memory.ptr, flags);
    }

    /// <summary>
    /// Sets the name of the index buffer, for debug display purposes.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    public void SetName(string name)
    {
        bgfx.set_index_buffer_name(handle, name, int.MaxValue);
    }

    /// <summary>
    /// Releases the index buffer.
    /// </summary>
    public void Dispose()
    {
        bgfx.destroy_index_buffer(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
