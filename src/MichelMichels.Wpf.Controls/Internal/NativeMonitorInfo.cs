using System.Runtime.InteropServices;

namespace MichelMichels.Wpf.Controls.Internal;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal struct NativeMonitorInfo
{
    public int Size;
    public NativeRectangle Monitor;
    public NativeRectangle Work;
    public uint Flags;
}