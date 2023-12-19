using System.Windows;

namespace MichelMichels.Wpf.Controls;

public class CloseButton : TextTitlebarButton
{
    public CloseButton()
    {
        Click += CloseButton_Click;
    }

    static CloseButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton),
            new FrameworkPropertyMetadata(typeof(CloseButton)));
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
}