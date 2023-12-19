using System.Windows;

namespace MichelMichels.Wpf.Controls;

public class TextTitlebarButton : TitlebarButton
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(TextTitlebarButton),
        new PropertyMetadata(string.Empty));

    static TextTitlebarButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextTitlebarButton),
            new FrameworkPropertyMetadata(typeof(TextTitlebarButton)));
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}