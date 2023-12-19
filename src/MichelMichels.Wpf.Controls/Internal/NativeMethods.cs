using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace MichelMichels.Wpf.Controls.Internal;

internal static partial class NativeMethods
{
    private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

    public static IntPtr HookProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        WM windowMessage = (WM)msg;

        Debug.WriteLine($"[{DateTime.Now:HH:mm:ss.FFF}] {windowMessage} {wParam} {lParam}");


        switch (windowMessage)
        {
            case WM.NCHITTEST:
                handled = true;
                return new IntPtr((int)HT.MAXBUTTON);
            case WM.GETMINMAXINFO:

                WmGetMinMaxInfo(hwnd, lParam);

                // Setting handled to false lets the handling of the window message fall through to default WPF mechanisms.
                // This way, the Min* attributes of the Window manage the minimum size
                // and the custom WmGetMinMaxInfo code manages the maximum size.
                handled = false;

                break;

            default:
                break;
        }

        return IntPtr.Zero;
    }

    /// <summary>
    /// The TryGetCurrentMonitorInfo function retrieves information about a display monitor.
    /// </summary>
    /// <param name="hwnd">A handle to the window of interest.</param>
    /// <param name="lpmi">Outputted NativeMonitorInfo structure that
    /// holds information about the current display monitor.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    public static bool TryGetCurrentMonitorInfo(IntPtr hwnd, out NativeMonitorInfo lpmi)
    {
        var currentMonitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
        lpmi = new NativeMonitorInfo() { Size = Marshal.SizeOf(typeof(NativeMonitorInfo)) };
        return currentMonitor != IntPtr.Zero && GetMonitorInfo(currentMonitor, ref lpmi);
    }

    private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
    {
        var hasWindowRect = GetWindowRect(hwnd, out Rect lpRect);

        if (!hasWindowRect ||
            lpRect is { Left: < 0, Top: < 0, Right: < 0, Bottom: < 0 })
            return;

        // We need to tell the system what our size should be when maximized. Otherwise it will cover the whole screen,
        // including the task bar.
        if (Marshal.PtrToStructure(lParam, typeof(MinMaxInfo)) is not MinMaxInfo minMaxInfo)
            return;

        // Adjust the maximized size and position to fit the work area of the correct monitor
        if (TryGetCurrentMonitorInfo(hwnd, out NativeMonitorInfo currentMonitorInfo))
        {
            minMaxInfo.PointMaxPosition.X = Math.Abs(currentMonitorInfo.Work.Left - currentMonitorInfo.Monitor.Left);
            minMaxInfo.PointMaxPosition.Y = Math.Abs(currentMonitorInfo.Work.Top - currentMonitorInfo.Monitor.Top);
            minMaxInfo.PointMaxSize.X = Math.Abs(currentMonitorInfo.Work.Right - currentMonitorInfo.Work.Left);
            minMaxInfo.PointMaxSize.Y = Math.Abs(currentMonitorInfo.Work.Bottom - currentMonitorInfo.Work.Top);
            minMaxInfo.PointMaxTrackSize.X = minMaxInfo.PointMaxSize.X;
            minMaxInfo.PointMaxTrackSize.Y = minMaxInfo.PointMaxSize.Y;
        }

        Marshal.StructureToPtr(minMaxInfo, lParam, true);
    }
}