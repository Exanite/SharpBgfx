using System;

namespace SharpBgfx.Original;

/// <summary>
/// Represents an occlusion query.
/// </summary>
public unsafe struct OcclusionQuery : IDisposable
{
    internal readonly ushort handle;

    /// <summary>
    /// Represents an invalid handle.
    /// </summary>
    public static readonly OcclusionQuery Invalid = new();

    /// <summary>
    /// Gets the result of the query.
    /// </summary>
    public OcclusionQueryResult Result => NativeMethods.bgfx_get_result(handle, null);

    /// <summary>
    /// Gets the number of pixels that passed the test. Only valid
    /// if <see cref="Result"/> is also valid.
    /// </summary>
    public int PassingPixels
    {
        get
        {
            var pixels = 0;
            NativeMethods.bgfx_get_result(handle, &pixels);

            return pixels;
        }
    }

    private OcclusionQuery(ushort handle)
    {
        this.handle = handle;
    }

    /// <summary>
    /// Creates a new query.
    /// </summary>
    /// <returns>The new occlusion query.</returns>
    public static OcclusionQuery Create()
    {
        return new OcclusionQuery(NativeMethods.bgfx_create_occlusion_query());
    }

    /// <summary>
    /// Releases the query.
    /// </summary>
    public void Dispose()
    {
        NativeMethods.bgfx_destroy_occlusion_query(handle);
    }

    /// <summary>
    /// Sets the condition for which the query should check.
    /// </summary>
    /// <param name="visible"><c>true</c> for visible; <c>false</c> for invisible.</param>
    public void SetCondition(bool visible)
    {
        NativeMethods.bgfx_set_condition(handle, visible);
    }

    public override string ToString()
    {
        return string.Format("Handle: {0}", handle);
    }
}
