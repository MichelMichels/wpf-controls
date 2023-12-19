using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace MichelMichels.Wpf.Controls.Internal;

internal partial class NativeMethods
{
    /// <summary>
    /// The MonitorFromWindow function retrieves a handle to the display monitor
    /// that has the largest area of intersection with the bounding rectangle of
    /// a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="dwFlags">Determines the function's return value if the window
    /// does not intersect any display monitor.</param>
    /// <returns> If the window intersects one or more display monitor rectangles,
    /// the return value is an HMONITOR handle to the display monitor that has
    /// the largest area of intersection with the window. If the window does
    /// not intersect a display monitor, the return value depends on the value of dwFlags.
    /// Remarks: If the window is currently minimized, MonitorFromWindow uses
    /// the rectangle of the window before it was minimized.</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    /// <summary>
    /// The GetMonitorInfo function retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A pointer to a NativeMonitorInfo structure that
    /// receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref NativeMonitorInfo lpmi);

    /// <summary>
    /// Retrieves the dimensions of the bounding rectangle of the specified window.
    /// The dimensions are given in screen coordinates that are relative to the
    /// upper-left corner of the screen.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="lpRect">A pointer to a NativeRectangle structure that receives the screen coordinates
    /// of the upper-left and lower-right corners of the window.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);
}
