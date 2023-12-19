using System.Runtime.InteropServices;

namespace MichelMichels.Wpf.Controls.Internal;

[StructLayout(LayoutKind.Sequential)]
internal struct MinMaxInfo
{
    public Point PointReserved;
    public Point PointMaxSize;
    public Point PointMaxPosition;
    public Point PointMinTrackSize;
    public Point PointMaxTrackSize;
}