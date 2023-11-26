using System.Windows;
using System.Windows.Controls;

namespace MichelMichels.Wpf.Controls;

public class WindowControls : Control
{
    static WindowControls()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowControls),
            new FrameworkPropertyMetadata(typeof(WindowControls)));
    }
}