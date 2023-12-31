using System;
using System.Runtime.InteropServices;

namespace MichelMichels.Wpf.Controls.Internal;

[Serializable, StructLayout(LayoutKind.Sequential)]
internal struct NativeRectangle
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public NativeRectangle(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}