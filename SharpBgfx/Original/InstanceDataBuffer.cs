using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Maintains a data buffer that contains instancing data.
/// </summary>
public struct InstanceDataBuffer
{
    internal NativeStruct data;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly InstanceDataBuffer Invalid = new();

    /// <summary>
    /// A pointer that can be filled with instance data.
    /// </summary>
    public IntPtr Data => data.data;

    /// <summary>
    /// The size of the data buffer.
    /// </summary>
    public int Size => data.size;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceDataBuffer"/> struct.
    /// </summary>
    /// <param name="count">The number of elements in the buffer.</param>
    /// <param name="stride">The stride of each element.</param>
    public InstanceDataBuffer(int count, int stride)
    {
        bgfx.alloc_instance_data_buffer(out data, count, (ushort)stride);
    }

    /// <summary>
    /// Gets the available space that can be used to allocate an instance buffer.
    /// </summary>
    /// <param name="count">The number of elements required.</param>
    /// <param name="stride">The stride of each element.</param>
    /// <returns>The number of available elements.</returns>
    public static int GetAvailableSpace(int count, int stride)
    {
        return bgfx.get_avail_instance_data_buffer(count, (ushort)stride);
    }

    public override string ToString()
    {
        return string.Format("Size: {0}", Size);
    }

#pragma warning disable 649
    internal struct NativeStruct
    {
        public IntPtr data;
        public int size;
        public int offset;
        public ushort stride;
        public ushort num;
        public ushort handle;
    }
#pragma warning restore 649
}
