using SharpBgfx.Bindings;

namespace SharpBgfx;

public class BgfxConstants
{
    public const ushort InvalidHandleId = ushort.MaxValue;

    public static readonly bgfx.DynamicIndexBufferHandle InvalidDynamicIndexBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.DynamicVertexBufferHandle InvalidDynamicVertexBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.FrameBufferHandle InvalidFrameBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.IndexBufferHandle InvalidIndexBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.IndirectBufferHandle InvalidIndirectBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.OcclusionQueryHandle InvalidOcclusionQueryHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.ProgramHandle InvalidProgramHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.ShaderHandle InvalidShaderHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.TextureHandle InvalidTextureHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.UniformHandle InvalidUniformHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.VertexBufferHandle InvalidVertexBufferHandle = new() { idx = InvalidHandleId };
    public static readonly bgfx.VertexLayoutHandle InvalidVertexLayoutHandle = new() { idx = InvalidHandleId };
}
