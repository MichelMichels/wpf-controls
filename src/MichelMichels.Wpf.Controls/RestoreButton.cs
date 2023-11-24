using System.Windows;

namespace MichelMichels.Wpf.Controls;

public class RestoreButton : TitlebarButton
{
    public RestoreButton()
    {
        Click += RestoreButton_Click;
    }

    static RestoreButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RestoreButton),
            new FrameworkPropertyMetadata(typeof(RestoreButton)));
    }

    private void RestoreButton_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).WindowState = WindowState.Normal;
    }
}