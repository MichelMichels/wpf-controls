using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MichelMichels.Wpf.Controls;

public class TitlebarButton : Button
{
    public static readonly DependencyProperty MouseOverBackgroundBrushProperty = DependencyProperty.Register(
        nameof(MouseOverBackgroundBrush),
        typeof(Brush),
        typeof(TitlebarButton),
        new PropertyMetadata(new SolidColorBrush()));

    public static readonly DependencyProperty PressedBackgroundBrushProperty = DependencyProperty.Register(
        nameof(PressedBackgroundBrush),
        typeof(Brush),
        typeof(TitlebarButton),
        new PropertyMetadata(new SolidColorBrush()));

    static TitlebarButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TitlebarButton),
            new FrameworkPropertyMetadata(typeof(TitlebarButton)));
    }

    public Brush MouseOverBackgroundBrush
    {
        get => (Brush)GetValue(MouseOverBackgroundBrushProperty);
        set => SetValue(MouseOverBackgroundBrushProperty, value);
    }
    public Brush PressedBackgroundBrush
    {
        get => (Brush)GetValue(PressedBackgroundBrushProperty);
        set => SetValue(PressedBackgroundBrushProperty, value);
    }
}