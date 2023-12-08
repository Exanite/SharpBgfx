using System;

namespace SharpBgfx;

/// <summary>
/// Maintains a transient vertex buffer.
/// </summary>
/// <remarks>
/// The contents of the buffer are valid for the current frame only.
/// You must call SetVertexBuffer with the buffer or a leak could occur.
/// </remarks>
public struct TransientVertexBuffer
{
    private int startVertex;
    private ushort stride;
    private readonly ushort handle;
    private ushort decl;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly TransientVertexBuffer Invalid = new();

    /// <summary>
    /// A pointer that can be filled with vertex data.
    /// </summary>
    public IntPtr Data { get; }

    /// <summary>
    /// The size of the buffer.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransientVertexBuffer"/> struct.
    /// </summary>
    /// <param name="vertexCount">The number of vertices that fit in the buffer.</param>
    /// <param name="layout">The layout of the vertex data.</param>
    public TransientVertexBuffer(int vertexCount, VertexLayout layout)
    {
        NativeMethods.bgfx_alloc_transient_vertex_buffer(out this, vertexCount, ref layout.data);
    }

    /// <summary>
    /// Gets the available space in the global transient vertex buffer.
    /// </summary>
    /// <param name="count">The number of vertices required.</param>
    /// <param name="layout">The layout of each vertex.</param>
    /// <returns>The number of available vertices.</returns>
    public static int GetAvailableSpace(int count, VertexLayout layout)
    {
        return NativeMethods.bgfx_get_avail_transient_vertex_buffer(count, ref layout.data);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
