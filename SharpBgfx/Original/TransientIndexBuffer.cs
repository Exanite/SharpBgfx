using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Maintains a transient index buffer.
/// </summary>
/// <remarks>
/// The contents of the buffer are valid for the current frame only.
/// You must call SetVertexBuffer with the buffer or a leak could occur.
/// </remarks>
public struct TransientIndexBuffer
{
    private int startIndex;
    private readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly TransientIndexBuffer Invalid = new();

    /// <summary>
    /// A pointer that can be filled with index data.
    /// </summary>
    public IntPtr Data { get; }

    /// <summary>
    /// The size of the buffer.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransientIndexBuffer"/> struct.
    /// </summary>
    /// <param name="indexCount">The number of 16-bit indices that fit in the buffer.</param>
    public TransientIndexBuffer(int indexCount)
    {
        bgfx.alloc_transient_index_buffer(out this, indexCount);
    }

    /// <summary>
    /// Gets the available space in the global transient index buffer.
    /// </summary>
    /// <param name="count">The number of 16-bit indices required.</param>
    /// <returns>The number of available indices.</returns>
    public static int GetAvailableSpace(int count)
    {
        return bgfx.get_avail_transient_index_buffer(count);
    }

    public override string ToString()
    {
        return string.Format("Count: {0}", Count);
    }
}
