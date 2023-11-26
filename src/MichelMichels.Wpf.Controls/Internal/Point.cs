using System;
using System.Runtime.InteropServices;

namespace MichelMichels.Wpf.Controls.Internal;

[Serializable, StructLayout(LayoutKind.Sequential)]
internal struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}