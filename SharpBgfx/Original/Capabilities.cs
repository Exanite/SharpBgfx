using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SharpBgfx.Original;

/// <summary>
/// Contains information about the capabilities of the rendering device.
/// </summary>
public unsafe struct Capabilities
{
    private readonly Caps* data;

    /// <summary>
    /// The currently active rendering backend API.
    /// </summary>
    public RendererBackend Backend => data->Backend;

    /// <summary>
    /// A set of extended features supported by the device.
    /// </summary>
    public DeviceFeatures SupportedFeatures => data->Supported;

    /// <summary>
    /// The maximum number of draw calls in a single frame.
    /// </summary>
    public int MaxDrawCalls => (int)data->MaxDrawCalls;

    /// <summary>
    /// The maximum number of texture blits in a single frame.
    /// </summary>
    public int MaxBlits => (int)data->MaxBlits;

    /// <summary>
    /// The maximum size of a texture, in pixels.
    /// </summary>
    public int MaxTextureSize => (int)data->MaxTextureSize;

    /// <summary>
    /// The maximum layers in a texture.
    /// </summary>
    public int MaxTextureLayers => (int)data->MaxTextureLayers;

    /// <summary>
    /// The maximum number of render views supported.
    /// </summary>
    public int MaxViews => (int)data->MaxViews;

    /// <summary>
    /// The maximum number of frame buffers that can be allocated.
    /// </summary>
    public int MaxFramebuffers => (int)data->MaxFramebuffers;

    /// <summary>
    /// The maximum number of attachments to a single framebuffer.
    /// </summary>
    public int MaxFramebufferAttachments => (int)data->MaxFramebufferAttachements;

    /// <summary>
    /// The maximum number of programs that can be allocated.
    /// </summary>
    public int MaxPrograms => (int)data->MaxPrograms;

    /// <summary>
    /// The maximum number of shaders that can be allocated.
    /// </summary>
    public int MaxShaders => (int)data->MaxShaders;

    /// <summary>
    /// The maximum number of textures that can be allocated.
    /// </summary>
    public int MaxTextures => (int)data->MaxTextures;

    /// <summary>
    /// The maximum number of texture samplers that can be allocated.
    /// </summary>
    public int MaxTextureSamplers => (int)data->MaxTextureSamplers;

    /// <summary>
    /// The maximum number of compute bindings that can be allocated.
    /// </summary>
    public int MaxComputeBindings => (int)data->MaxComputeBindings;

    /// <summary>
    /// The maximum number of vertex declarations that can be allocated.
    /// </summary>
    public int MaxVertexDecls => (int)data->MaxVertexDecls;

    /// <summary>
    /// The maximum number of vertex streams that can be used.
    /// </summary>
    public int MaxVertexStreams => (int)data->MaxVertexStreams;

    /// <summary>
    /// The maximum number of index buffers that can be allocated.
    /// </summary>
    public int MaxIndexBuffers => (int)data->MaxIndexBuffers;

    /// <summary>
    /// The maximum number of vertex buffers that can be allocated.
    /// </summary>
    public int MaxVertexBuffers => (int)data->MaxVertexBuffers;

    /// <summary>
    /// The maximum number of dynamic index buffers that can be allocated.
    /// </summary>
    public int MaxDynamicIndexBuffers => (int)data->MaxDynamicIndexBuffers;

    /// <summary>
    /// The maximum number of dynamic vertex buffers that can be allocated.
    /// </summary>
    public int MaxDynamicVertexBuffers => (int)data->MaxDynamicVertexBuffers;

    /// <summary>
    /// The maximum number of uniforms that can be used.
    /// </summary>
    public int MaxUniforms => (int)data->MaxUniforms;

    /// <summary>
    /// The maximum number of occlusion queries that can be used.
    /// </summary>
    public int MaxOcclusionQueries => (int)data->MaxOcclusionQueries;

    /// <summary>
    /// The maximum number of encoder threads.
    /// </summary>
    public int MaxEncoders => (int)data->MaxEncoders;

    /// <summary>
    /// The amount of transient vertex buffer space reserved.
    /// </summary>
    public int TransientVertexBufferSize => (int)data->TransientVbSize;

    /// <summary>
    /// The amount of transient index buffer space reserved.
    /// </summary>
    public int TransientIndexBufferSize => (int)data->TransientIbSize;

    /// <summary>
    /// Indicates whether depth coordinates in NDC range from -1 to 1 (true) or 0 to 1 (false).
    /// </summary>
    public bool HomogeneousDepth => data->HomogeneousDepth != 0;

    /// <summary>
    /// Indicates whether the coordinate system origin is at the bottom left or top left.
    /// </summary>
    public bool OriginBottomLeft => data->OriginBottomLeft != 0;

    /// <summary>
    /// Details about the currently active graphics adapter.
    /// </summary>
    public Adapter CurrentAdapter => new((Vendor)data->VendorId, data->DeviceId);

    /// <summary>
    /// A list of all graphics adapters installed on the system.
    /// </summary>
    public AdapterCollection Adapters => new(data->GPUs, data->GPUCount);

    internal Capabilities(Caps* data)
    {
        this.data = data;
    }

    /// <summary>
    /// Checks device support for a specific texture format.
    /// </summary>
    /// <param name="format">The format to check.</param>
    /// <returns>The level of support for the given format.</returns>
    public TextureFormatSupport CheckTextureSupport(TextureFormat format)
    {
        return (TextureFormatSupport)data->Formats[(int)format];
    }

    /// <summary>
    /// Provides access to a collection of adapters.
    /// </summary>
    public struct AdapterCollection : IReadOnlyList<Adapter>
    {
        private readonly ushort* data;
        private readonly int count;

        /// <summary>
        /// Accesses the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the given index.</returns>
        public Adapter this[int index] => new((Vendor)data[index * 2], data[index * 2 + 1]);

        /// <summary>
        /// The number of elements in the collection.
        /// </summary>
        public int Count => count;

        internal AdapterCollection(ushort* data, int count)
        {
            this.data = data;
            this.count = count;
        }

        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>A collection enumerator.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<Adapter> IEnumerable<Adapter>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implements an enumerator for an AdapterCollection.
        /// </summary>
        public struct Enumerator : IEnumerator<Adapter>
        {
            private AdapterCollection collection;
            private int index;

            /// <summary>
            /// The current enumerated item.
            /// </summary>
            public Adapter Current => collection[index];

            object IEnumerator.Current => Current;

            internal Enumerator(AdapterCollection collection)
            {
                this.collection = collection;
                index = -1;
            }

            /// <summary>
            /// Advances to the next item in the sequence.
            /// </summary>
            /// <returns><c>true</c> if there are more items in the collection; otherwise, <c>false</c>.</returns>
            public bool MoveNext()
            {
                var newIndex = index + 1;
                if (newIndex >= collection.Count)
                {
                    return false;
                }

                index = newIndex;

                return true;
            }

            /// <summary>
            /// Empty; does nothing.
            /// </summary>
            public void Dispose() {}

            /// <summary>
            /// Not implemented.
            /// </summary>
            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}

/// <summary>
/// Contains details about an installed graphics adapter.
/// </summary>
public struct Adapter
{
    /// <summary>
    /// Represents the default adapter for the system.
    /// </summary>
    public static readonly Adapter Default = new(Vendor.None, 0);

    /// <summary>
    /// The IHV that published the adapter.
    /// </summary>
    public readonly Vendor Vendor;

    /// <summary>
    /// A vendor-specific identifier for the adapter type.
    /// </summary>
    public readonly int DeviceId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Adapter"/> struct.
    /// </summary>
    /// <param name="vendor">The vendor.</param>
    /// <param name="deviceId">The device ID.</param>
    public Adapter(Vendor vendor, int deviceId)
    {
        Vendor = vendor;
        DeviceId = deviceId;
    }

    public override string ToString()
    {
        return string.Format("Vendor: {0}, Device: {0}", Vendor, DeviceId);
    }
}

/// <summary>
/// Contains various performance metrics tracked by the library.
/// </summary>
public unsafe struct PerfStats
{
    private readonly Stats* data;

    /// <summary>
    /// CPU time between two <see cref="Bgfx.Frame"/> calls.
    /// </summary>
    public long CpuTimeFrame => data->CpuTimeFrame;

    /// <summary>
    /// CPU frame start time.
    /// </summary>
    public long CpuTimeStart => data->CpuTimeBegin;

    /// <summary>
    /// CPU frame end time.
    /// </summary>
    public long CpuTimeEnd => data->CpuTimeEnd;

    /// <summary>
    /// CPU timer frequency.
    /// </summary>
    public long CpuTimerFrequency => data->CpuTimerFrequency;

    /// <summary>
    /// Elapsed CPU time.
    /// </summary>
    public TimeSpan CpuElapsed => TimeSpan.FromSeconds((double)(CpuTimeEnd - CpuTimeStart) / CpuTimerFrequency);

    /// <summary>
    /// GPU frame start time.
    /// </summary>
    public long GpuTimeStart => data->GpuTimeBegin;

    /// <summary>
    /// GPU frame end time.
    /// </summary>
    public long GpuTimeEnd => data->GpuTimeEnd;

    /// <summary>
    /// GPU timer frequency.
    /// </summary>
    public long GpuTimerFrequency => data->GpuTimerFrequency;

    /// <summary>
    /// Elapsed GPU time.
    /// </summary>
    public TimeSpan GpuElapsed => TimeSpan.FromSeconds((double)(GpuTimeEnd - GpuTimeStart) / GpuTimerFrequency);

    /// <summary>
    /// Time spent waiting for the render thread.
    /// </summary>
    public long WaitingForRender => data->WaitRender;

    /// <summary>
    /// Time spent waiting for the submit thread.
    /// </summary>
    public long WaitingForSubmit => data->WaitSubmit;

    /// <summary>
    /// The number of draw calls submitted.
    /// </summary>
    public int DrawCallsSubmitted => data->NumDraw;

    /// <summary>
    /// The number of compute calls submitted.
    /// </summary>
    public int ComputeCallsSubmitted => data->NumCompute;

    /// <summary>
    /// The number of blit calls submitted.
    /// </summary>
    public int BlitCallsSubmitted => data->NumBlit;

    /// <summary>
    /// Maximum observed GPU driver latency.
    /// </summary>
    public int MaxGpuLatency => data->MaxGpuLatency;

    /// <summary>
    /// Number of allocated dynamic index buffers.
    /// </summary>
    public int DynamicIndexBufferCount => data->NumDynamicIndexBuffers;

    /// <summary>
    /// Number of allocated dynamic vertex buffers.
    /// </summary>
    public int DynamicVertexBufferCount => data->NumDynamicVertexBuffers;

    /// <summary>
    /// Number of allocated frame buffers.
    /// </summary>
    public int FrameBufferCount => data->NumFrameBuffers;

    /// <summary>
    /// Number of allocated index buffers.
    /// </summary>
    public int IndexBufferCount => data->NumIndexBuffers;

    /// <summary>
    /// Number of allocated occlusion queries.
    /// </summary>
    public int OcclusionQueryCount => data->NumOcclusionQueries;

    /// <summary>
    /// Number of allocated shader programs.
    /// </summary>
    public int ProgramCount => data->NumPrograms;

    /// <summary>
    /// Number of allocated shaders.
    /// </summary>
    public int ShaderCount => data->NumShaders;

    /// <summary>
    /// Number of allocated textures.
    /// </summary>
    public int TextureCount => data->NumTextures;

    /// <summary>
    /// Number of allocated uniforms.
    /// </summary>
    public int UniformCount => data->NumUniforms;

    /// <summary>
    /// Number of allocated vertex buffers.
    /// </summary>
    public int VertexBufferCount => data->NumVertexBuffers;

    /// <summary>
    /// Number of allocated vertex declarations.
    /// </summary>
    public int VertexDeclarationCount => data->NumVertexDecls;

    /// <summary>
    /// The amount of memory used by textures.
    /// </summary>
    public long TextureMemoryUsed => data->TextureMemoryUsed;

    /// <summary>
    /// The amount of memory used by render targets.
    /// </summary>
    public long RenderTargetMemoryUsed => data->RtMemoryUsed;

    /// <summary>
    /// The number of transient vertex buffers used.
    /// </summary>
    public int TransientVertexBuffersUsed => data->TransientVbUsed;

    /// <summary>
    /// The number of transient index buffers used.
    /// </summary>
    public int TransientIndexBuffersUsed => data->TransientIbUsed;

    /// <summary>
    /// Maximum available GPU memory.
    /// </summary>
    public long MaxGpuMemory => data->GpuMemoryMax;

    /// <summary>
    /// The amount of GPU memory currently in use.
    /// </summary>
    public long GpuMemoryUsed => data->GpuMemoryUsed;

    /// <summary>
    /// The width of the back buffer.
    /// </summary>
    public int BackbufferWidth => data->Width;

    /// <summary>
    /// The height of the back buffer.
    /// </summary>
    public int BackbufferHeight => data->Height;

    /// <summary>
    /// The width of the debug text buffer.
    /// </summary>
    public int TextBufferWidth => data->TextWidth;

    /// <summary>
    /// The height of the debug text buffer.
    /// </summary>
    public int TextBufferHeight => data->TextHeight;

    /// <summary>
    /// Gets a collection of statistics for each rendering view.
    /// </summary>
    public ViewStatsCollection Views => new(data->ViewStats, data->NumViews);

    static PerfStats()
    {
        Debug.Assert(Stats.NumTopologies == Enum.GetValues(typeof(Topology)).Length);
    }

    internal PerfStats(Stats* data)
    {
        this.data = data;
    }

    /// <summary>
    /// Gets the number of primitives rendered with the given topology.
    /// </summary>
    /// <param name="topology">The topology whose primitive count should be returned.</param>
    /// <returns>The number of primitives rendered.</returns>
    public int GetPrimitiveCount(Topology topology)
    {
        return (int)data->NumPrims[(int)topology];
    }

    /// <summary>
    /// Contains perf metrics for a single rendering view.
    /// </summary>
    public struct ViewStats
    {
        private readonly ViewStatsNative* data;

        /// <summary>
        /// The name of the view.
        /// </summary>
        public string Name => new(data->Name);

        /// <summary>
        /// The amount of CPU time elapsed during processing of this view.
        /// </summary>
        public long CpuTimeElapsed => (long)data->CpuTimeElapsed;

        /// <summary>
        /// The amount of GPU time elapsed during processing of this view.
        /// </summary>
        public long GpuTimeElapsed => (long)data->GpuTimeElapsed;

        internal ViewStats(ViewStatsNative* data)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// Provides access to a collection of view statistics.
    /// </summary>
    public struct ViewStatsCollection : IReadOnlyList<ViewStats>
    {
        private readonly ViewStatsNative* data;
        private readonly int count;

        /// <summary>
        /// Accesses the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the given index.</returns>
        public ViewStats this[int index] => new(data + index);

        /// <summary>
        /// The number of elements in the collection.
        /// </summary>
        public int Count => count;

        internal ViewStatsCollection(ViewStatsNative* data, int count)
        {
            this.data = data;
            this.count = count;
        }

        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>A collection enumerator.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<ViewStats> IEnumerable<ViewStats>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implements an enumerator for a ViewStatsCollection.
        /// </summary>
        public struct Enumerator : IEnumerator<ViewStats>
        {
            private ViewStatsCollection collection;
            private int index;

            /// <summary>
            /// The current enumerated item.
            /// </summary>
            public ViewStats Current => collection[index];

            object IEnumerator.Current => Current;

            internal Enumerator(ViewStatsCollection collection)
            {
                this.collection = collection;
                index = -1;
            }

            /// <summary>
            /// Advances to the next item in the sequence.
            /// </summary>
            /// <returns><c>true</c> if there are more items in the collection; otherwise, <c>false</c>.</returns>
            public bool MoveNext()
            {
                var newIndex = index + 1;
                if (newIndex >= collection.Count)
                {
                    return false;
                }

                index = newIndex;

                return true;
            }

            /// <summary>
            /// Empty; does nothing.
            /// </summary>
            public void Dispose() {}

            /// <summary>
            /// Not implemented.
            /// </summary>
            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Contains perf metrics for a single encoder instance.
    /// </summary>
    public struct EncoderStats
    {
        private readonly EncoderStatsNative* data;

        /// <summary>
        /// CPU frame start time.
        /// </summary>
        public long CpuTimeStart => data->CpuTimeBegin;

        /// <summary>
        /// CPU frame end time.
        /// </summary>
        public long CpuTimeEnd => data->CpuTimeEnd;

        internal EncoderStats(EncoderStatsNative* data)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// Provides access to a collection of encoder statistics.
    /// </summary>
    public struct EncoderStatsCollection : IReadOnlyList<EncoderStats>
    {
        private readonly EncoderStatsNative* data;
        private readonly int count;

        /// <summary>
        /// Accesses the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the given index.</returns>
        public EncoderStats this[int index] => new(data + index);

        /// <summary>
        /// The number of elements in the collection.
        /// </summary>
        public int Count => count;

        internal EncoderStatsCollection(EncoderStatsNative* data, int count)
        {
            this.data = data;
            this.count = count;
        }

        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>A collection enumerator.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<EncoderStats> IEnumerable<EncoderStats>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implements an enumerator for an EncoderStatsCollection.
        /// </summary>
        public struct Enumerator : IEnumerator<EncoderStats>
        {
            private EncoderStatsCollection collection;
            private int index;

            /// <summary>
            /// The current enumerated item.
            /// </summary>
            public EncoderStats Current => collection[index];

            object IEnumerator.Current => Current;

            internal Enumerator(EncoderStatsCollection collection)
            {
                this.collection = collection;
                index = -1;
            }

            /// <summary>
            /// Advances to the next item in the sequence.
            /// </summary>
            /// <returns><c>true</c> if there are more items in the collection; otherwise, <c>false</c>.</returns>
            public bool MoveNext()
            {
                var newIndex = index + 1;
                if (newIndex >= collection.Count)
                {
                    return false;
                }

                index = newIndex;

                return true;
            }

            /// <summary>
            /// Empty; does nothing.
            /// </summary>
            public void Dispose() {}

            /// <summary>
            /// Not implemented.
            /// </summary>
            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
