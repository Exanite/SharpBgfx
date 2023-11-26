using System;
using SharpBgfx.Bindings;

namespace SharpBgfx;

/// <summary>
/// Provides an interface for programs to respond to callbacks from the bgfx library.
/// </summary>
public interface ICallbackHandler
{
    /// <summary>
    /// Called when an error occurs in the library.
    /// </summary>
    /// <param name="fileName">The name of the source file in which the message originated.</param>
    /// <param name="line">The line number in which the message originated.</param>
    /// <param name="errorType">The type of error that occurred.</param>
    /// <param name="message">Message string detailing what went wrong.</param>
    /// <remarks>
    /// If the error type is not <see cref="ErrorType.DebugCheck"/>, bgfx is in an
    /// unrecoverable state and the application should terminate.
    /// This method can be called from any thread.
    /// </remarks>
    void ReportError(string fileName, int line, ErrorType errorType, string message);

    /// <summary>
    /// Called to print debug messages.
    /// </summary>
    /// <param name="fileName">The name of the source file in which the message originated.</param>
    /// <param name="line">The line number in which the message originated.</param>
    /// <param name="format">The message format string.</param>
    /// <param name="args">A pointer to format arguments.</param>
    /// <remarks>This method can be called from any thread.</remarks>
    void ReportDebug(string fileName, int line, string format, IntPtr args);

    /// <summary>
    /// Called when a profiling region is entered.
    /// </summary>
    /// <param name="name">The name of the region.</param>
    /// <param name="color">The color of the region.</param>
    /// <param name="filePath">The path of the source file containing the region.</param>
    /// <param name="line">The line number on which the region was started.</param>
    void ProfilerBegin(string name, int color, string filePath, int line);

    /// <summary>
    /// Called when a profiling region is ended.
    /// </summary>
    void ProfilerEnd();

    /// <summary>
    /// Queries the size of a cache item.
    /// </summary>
    /// <param name="id">The cache entry ID.</param>
    /// <returns>The size of the cache item, or 0 if the item is not found.</returns>
    int GetCachedSize(long id);

    /// <summary>
    /// Retrieves an entry from the cache.
    /// </summary>
    /// <param name="id">The cache entry ID.</param>
    /// <param name="data">A pointer that should be filled with data from the cache.</param>
    /// <param name="size">The size of the memory block pointed to be <paramref name="data"/>.</param>
    /// <returns><c>true</c> if the item is found in the cache; otherwise, <c>false</c>.</returns>
    bool GetCacheEntry(long id, IntPtr data, int size);

    /// <summary>
    /// Saves an entry in the cache.
    /// </summary>
    /// <param name="id">The cache entry ID.</param>
    /// <param name="data">A pointer to the data to save in the cache.</param>
    /// <param name="size">The size of the memory block pointed to be <paramref name="data"/>.</param>
    void SetCacheEntry(long id, IntPtr data, int size);

    /// <summary>
    /// Save a captured screenshot.
    /// </summary>
    /// <param name="path">The path at which to save the image.</param>
    /// <param name="width">The width of the image.</param>
    /// <param name="height">The height of the image.</param>
    /// <param name="pitch">The number of bytes between lines in the image.</param>
    /// <param name="data">A pointer to the image data to save.</param>
    /// <param name="size">The size of the image memory.</param>
    /// <param name="flipVertical"><c>true</c> if the image origin is bottom left instead of top left; otherwise, <c>false</c>.</param>
    void SaveScreenShot(string path, int width, int height, int pitch, IntPtr data, int size, bool flipVertical);

    /// <summary>
    /// Notifies that a frame capture has begun.
    /// </summary>
    /// <param name="width">The width of the capture surface.</param>
    /// <param name="height">The height of the capture surface.</param>
    /// <param name="pitch">The number of bytes between lines in the captured frames.</param>
    /// <param name="format">The format of captured frames.</param>
    /// <param name="flipVertical"><c>true</c> if the image origin is bottom left instead of top left; otherwise, <c>false</c>.</param>
    void CaptureStarted(int width, int height, int pitch, bgfx.TextureFormat format, bool flipVertical);

    /// <summary>
    /// Notifies that a frame capture has finished.
    /// </summary>
    void CaptureFinished();

    /// <summary>
    /// Notifies that a frame has been captured.
    /// </summary>
    /// <param name="data">A pointer to the frame data.</param>
    /// <param name="size">The size of the frame data.</param>
    void CaptureFrame(IntPtr data, int size);
}
