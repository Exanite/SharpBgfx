using System;
using System.Collections.Generic;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a single compiled shader component.
/// </summary>
public unsafe struct Shader : IDisposable
{
    private Uniform[] uniforms;
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly Shader Invalid = new();

    /// <summary>
    /// The set of uniforms exposed by the shader.
    /// </summary>
    public IReadOnlyList<Uniform> Uniforms
    {
        get
        {
            if (uniforms == null)
            {
                var count = NativeMethods.bgfx_get_shader_uniforms(handle, null, 0);
                uniforms = new Uniform[count];
                NativeMethods.bgfx_get_shader_uniforms(handle, uniforms, count);
            }

            return uniforms;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Shader"/> struct.
    /// </summary>
    /// <param name="memory">The compiled shader memory.</param>
    public Shader(MemoryBlock memory)
    {
        handle = NativeMethods.bgfx_create_shader(memory.ptr);
        uniforms = null;
    }

    /// <summary>
    /// Releases the shader.
    /// </summary>
    public void Dispose()
    {
        NativeMethods.bgfx_destroy_shader(handle);
    }

    /// <summary>
    /// Sets the name of the shader, for debug display purposes.
    /// </summary>
    /// <param name="name">The name of the shader.</param>
    public void SetName(string name)
    {
        NativeMethods.bgfx_set_shader_name(handle, name, int.MaxValue);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
