using System;
using SharpBgfx.Bindings;

namespace SharpBgfx.Original;

/// <summary>
/// Represents a compiled and linked shader program.
/// </summary>
public struct Program : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly Program Invalid = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Program"/> struct.
    /// </summary>
    /// <param name="vertexShader">The vertex shader.</param>
    /// <param name="fragmentShader">The fragment shader.</param>
    /// <param name="destroyShaders">if set to <c>true</c>, the shaders will be released after creating the program.</param>
    public Program(Shader vertexShader, Shader fragmentShader, bool destroyShaders = false)
    {
        handle = bgfx.create_program(vertexShader.handle, fragmentShader.handle, destroyShaders);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Program"/> struct.
    /// </summary>
    /// <param name="computeShader">The compute shader.</param>
    /// <param name="destroyShaders">if set to <c>true</c>, the compute shader will be released after creating the program.</param>
    public Program(Shader computeShader, bool destroyShaders = false)
    {
        handle = bgfx.create_compute_program(computeShader.handle, destroyShaders);
    }

    /// <summary>
    /// Releases the program.
    /// </summary>
    public void Dispose()
    {
        bgfx.destroy_program(handle);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
