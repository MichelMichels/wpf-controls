using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MichelMichels.Wpf.Controls;

public class TitlebarButton : Button
{
    public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register(
        nameof(MouseOverBackground),
        typeof(Brush),
        typeof(TitlebarButton),
        new PropertyMetadata(new SolidColorBrush()));

    public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(
        nameof(PressedBackground),
        typeof(Brush),
        typeof(TitlebarButton),
        new PropertyMetadata(new SolidColorBrush()));

    static TitlebarButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TitlebarButton),
            new FrameworkPropertyMetadata(typeof(TitlebarButton)));
    }

    public Brush MouseOverBackground
    {
        get => (Brush)GetValue(MouseOverBackgroundProperty);
        set => SetValue(MouseOverBackgroundProperty, value);
    }
    public Brush PressedBackground
    {
        get => (Brush)GetValue(PressedBackgroundProperty);
        set => SetValue(PressedBackgroundProperty, value);
    }
}