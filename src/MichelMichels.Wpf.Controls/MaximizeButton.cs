using System.Windows;

namespace MichelMichels.Wpf.Controls;

public class MaximizeButton : TextTitlebarButton
{
    public MaximizeButton()
    {
        Click += MaximizeButton_Click;
    }

    static MaximizeButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MaximizeButton),
            new FrameworkPropertyMetadata(typeof(MaximizeButton)));
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).WindowState = WindowState.Maximized;
    }
}