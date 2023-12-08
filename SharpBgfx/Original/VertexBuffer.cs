using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a static vertex buffer.
/// </summary>
public unsafe struct VertexBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly VertexBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="VertexBuffer"/> struct.
    /// </summary>
    /// <param name="memory">The vertex data with which to populate the buffer.</param>
    /// <param name="layout">The layout of the vertex data.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public VertexBuffer(MemoryBlock memory, VertexLayout layout, BufferFlags flags = BufferFlags.None)
    {
        handle = bgfx.create_vertex_buffer(memory.ptr, ref layout.data, flags);
    }

    /// <summary>
    /// Sets the name of the vertex buffer, for debug display purposes.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    public void SetName(string name)
    {
        bgfx.set_vertex_buffer_name(handle, name, int.MaxValue);
    }

    /// <summary>
    /// Releases the vertex buffer.
    /// </summary>
    public void Dispose()
    {
        bgfx.destroy_vertex_buffer(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
