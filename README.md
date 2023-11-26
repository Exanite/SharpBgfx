# SharpBgfx
This is a fork of https://github.com/MikePopoloski/SharpBgfx, but using the official bgfx bindings.

Note: Most of the original code has been commented out and I don't plan to reimplement most of it.

## Changes of note

In order to allow bgfx log messages to be properly formatted, I have forked bgfx ([Exanite/bgfx](https://github.com/Exanite/bgfx)) and added `bgfx_vsnprintf` to the list of exported functions.

The original SharpBgfx repo also did something similar. See: https://github.com/MikePopoloski/SharpBgfx/issues/11

# Original README
## SharpBgfx

Provides managed (C#,VB,F#,etc) bindings for the bgfx graphics library.

See <https://github.com/bkaradzic/bgfx>.

The main library is a minimal set of pinvoke declarations. The easiest way to use it is to drop the amalgamated SharpBgfx.cs file into your project and go. Alternatively, you can build the library into a managed assembly.

### Platforms

Currently only tested on Windows, though it will probably run fine on Mac and Linux if Mono is installed and the bgfx native library is rebuilt for those platforms.
