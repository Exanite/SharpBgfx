namespace SharpBgfx;

/// <summary>
/// Specifies various error types that can be reported by bgfx.
/// </summary>
/// <remarks>
/// https://github.com/bkaradzic/bgfx/blob/6dea6a22b64eea21829bdffdade1c482d4fc3eb5/include/bgfx/bgfx.h#L31
/// </remarks>
public enum ErrorType {
    /// <summary>
    /// A debug check failed; the program can safely continue, but the issue should be investigated.
    /// </summary>
    DebugCheck,

    /// <summary>
    /// The program tried to compile an invalid shader.
    /// </summary>
    InvalidShader,

    /// <summary>
    /// An error occurred during bgfx library initialization.
    /// </summary>
    UnableToInitialize,

    /// <summary>
    /// Failed while trying to create a texture.
    /// </summary>
    UnableToCreateTexture,

    /// <summary>
    /// The graphics device was lost and the library was unable to recover.
    /// </summary>
    DeviceLost,
}
