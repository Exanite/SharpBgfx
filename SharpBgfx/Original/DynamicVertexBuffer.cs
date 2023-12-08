using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a dynamically updateable vertex buffer.
/// </summary>
public unsafe struct DynamicVertexBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly DynamicVertexBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicVertexBuffer"/> struct.
    /// </summary>
    /// <param name="vertexCount">The number of vertices that fit in the buffer.</param>
    /// <param name="layout">The layout of the vertex data.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public DynamicVertexBuffer(int vertexCount, VertexLayout layout, BufferFlags flags = BufferFlags.None)
    {
        handle = bgfx.create_dynamic_vertex_buffer(vertexCount, ref layout.data, flags);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicVertexBuffer"/> struct.
    /// </summary>
    /// <param name="memory">The initial vertex data with which to populate the buffer.</param>
    /// <param name="layout">The layout of the vertex data.</param>
    /// <param name="flags">Flags used to control buffer behavior.</param>
    public DynamicVertexBuffer(MemoryBlock memory, VertexLayout layout, BufferFlags flags = BufferFlags.None)
    {
        handle = bgfx.create_dynamic_vertex_buffer_mem(memory.ptr, ref layout.data, flags);
    }

    /// <summary>
    /// Updates the data in the buffer.
    /// </summary>
    /// <param name="startVertex">Index of the first vertex to update.</param>
    /// <param name="memory">The new vertex data with which to fill the buffer.</param>
    public void Update(int startVertex, MemoryBlock memory)
    {
        bgfx.update_dynamic_vertex_buffer(handle, startVertex, memory.ptr);
    }

    /// <summary>
    /// Releases the vertex buffer.
    /// </summary>
    public void Dispose()
    {
        bgfx.destroy_dynamic_vertex_buffer(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
