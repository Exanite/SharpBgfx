using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a framebuffer attachment.
/// </summary>
public struct Attachment
{
    /// <summary>
    /// Access control for using the attachment.
    /// </summary>
    public ComputeBufferAccess Access;

    /// <summary>
    /// The attachment texture handle.
    /// </summary>
    public Texture Texture;

    /// <summary>
    /// The texture mip level.
    /// </summary>
    public int Mip;

    /// <summary>
    /// Cube map face or depth layer/slice.
    /// </summary>
    public int Layer;

    /// <summary>
    /// Additional flags for framebuffer resolve.
    /// </summary>
    public ResolveFlags Resolve;
}

/// <summary>
/// An aggregated frame buffer, with one or more attached texture surfaces.
/// </summary>
public unsafe struct FrameBuffer : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly FrameBuffer Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> struct.
    /// </summary>
    /// <param name="width">The width of the render target.</param>
    /// <param name="height">The height of the render target.</param>
    /// <param name="format">The format of the new surface.</param>
    /// <param name="flags">Texture sampling flags.</param>
    public FrameBuffer(int width, int height, TextureFormat format, TextureFlags flags = TextureFlags.ClampU | TextureFlags.ClampV)
    {
        handle = bgfx.create_frame_buffer((ushort)width, (ushort)height, format, flags);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> struct.
    /// </summary>
    /// <param name="ratio">The amount to scale when the backbuffer resizes.</param>
    /// <param name="format">The format of the new surface.</param>
    /// <param name="flags">Texture sampling flags.</param>
    public FrameBuffer(BackbufferRatio ratio, TextureFormat format, TextureFlags flags = TextureFlags.ClampU | TextureFlags.ClampV)
    {
        handle = bgfx.create_frame_buffer_scaled(ratio, format, flags);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> struct.
    /// </summary>
    /// <param name="attachments">A set of attachments from which to build the frame buffer.</param>
    /// <param name="destroyTextures">if set to <c>true</c>, attached textures will be destroyed when the frame buffer is
    /// destroyed.</param>
    public FrameBuffer(Attachment[] attachments, bool destroyTextures = false)
    {
        var count = (byte)attachments.Length;
        var native = stackalloc NativeAttachment[count];
        for (var i = 0; i < count; i++)
        {
            var attachment = attachments[i];
            native[i] = new NativeAttachment
            {
                access = attachment.Access,
                handle = attachment.Texture.handle,
                mip = (ushort)attachment.Mip,
                layer = (ushort)attachment.Layer,
                resolve = attachment.Resolve,
            };
        }

        handle = bgfx.create_frame_buffer_from_attachment(count, native, destroyTextures);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> struct.
    /// </summary>
    /// <param name="textures">A set of textures from which to build the frame buffer.</param>
    /// <param name="destroyTextures">if set to <c>true</c>, attached textures will be destroyed when the frame buffer is
    /// destroyed.</param>
    public FrameBuffer(Texture[] textures, bool destroyTextures = false)
    {
        var count = (byte)textures.Length;
        var native = stackalloc ushort[count];
        for (var i = 0; i < count; i++)
        {
            native[i] = textures[i].handle;
        }

        handle = bgfx.create_frame_buffer_from_handles(count, native, destroyTextures);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrameBuffer"/> struct.
    /// </summary>
    /// <param name="windowHandle">The OS window handle to which the frame buffer is attached.</param>
    /// <param name="width">The width of the render target.</param>
    /// <param name="height">The height of the render target.</param>
    /// <param name="format">Window back buffer color format.</param>
    /// <param name="depthFormat">A desired format for a depth buffer, if applicable.</param>
    public FrameBuffer(IntPtr windowHandle, int width, int height, TextureFormat format = TextureFormat.Count, TextureFormat depthFormat = TextureFormat.Count)
    {
        handle = bgfx.create_frame_buffer_from_nwh(windowHandle, (ushort)width, (ushort)height, format, depthFormat);
    }

    /// <summary>
    /// Sets the name of the frame buffer, for debug display purposes.
    /// </summary>
    /// <param name="name">The name of the texture.</param>
    public void SetName(string name)
    {
        bgfx.set_frame_buffer_name(handle, name, int.MaxValue);
    }

    /// <summary>
    /// Releases the frame buffer.
    /// </summary>
    public void Dispose()
    {
        bgfx.destroy_frame_buffer(handle);
    }

    /// <summary>
    /// Gets the texture associated with a particular framebuffer attachment.
    /// </summary>
    /// <param name="attachment">The attachment index.</param>
    /// <returns>The texture associated with the attachment.</returns>
    public Texture GetTexture(int attachment = 0)
    {
        var info = new Texture.TextureInfo();

        return new Texture(bgfx.get_texture(handle, (byte)attachment), ref info);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }

    internal struct NativeAttachment
    {
        public ComputeBufferAccess access;
        public ushort handle;
        public ushort mip;
        public ushort layer;
        public ResolveFlags resolve;
    }
}
