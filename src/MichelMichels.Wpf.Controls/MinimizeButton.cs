using System.Windows;

namespace MichelMichels.Wpf.Controls;

public class MinimizeButton : TextTitlebarButton
{
    public MinimizeButton()
    {
        Click += MinimizeButton_Click;
    }

    static MinimizeButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MinimizeButton),
            new FrameworkPropertyMetadata(typeof(MinimizeButton)));
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).WindowState = WindowState.Minimized;
    }
}