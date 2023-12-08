using System;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a dynamically updateable index buffer.
/// </summary>
public unsafe struct DynamicIndexBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly DynamicIndexBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicIndexBuffer"/> struct.
    /// </summary>
    /// <param name="indexCount">The number of indices that can fit in the buffer.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public DynamicIndexBuffer(int indexCount, BufferFlags flags = BufferFlags.None)
    {
        handle = NativeMethods.bgfx_create_dynamic_index_buffer(indexCount, flags);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicIndexBuffer"/> struct.
    /// </summary>
    /// <param name="memory">The initial index data with which to populate the buffer.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public DynamicIndexBuffer(MemoryBlock memory, BufferFlags flags = BufferFlags.None)
    {
        handle = NativeMethods.bgfx_create_dynamic_index_buffer_mem(memory.ptr, flags);
    }

    /// <summary>
    /// Updates the data in the buffer.
    /// </summary>
    /// <param name="startIndex">Index of the first index to update.</param>
    /// <param name="memory">The new index data with which to fill the buffer.</param>
    public void Update(int startIndex, MemoryBlock memory)
    {
        NativeMethods.bgfx_update_dynamic_index_buffer(handle, startIndex, memory.ptr);
    }

    /// <summary>
    /// Releases the index buffer.
    /// </summary>
    public void Dispose()
    {
        NativeMethods.bgfx_destroy_dynamic_index_buffer(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
