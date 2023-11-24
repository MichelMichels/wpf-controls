using MichelMichels.Wpf.Controls.Resources;
using System.Windows;
using System.Windows.Media;

namespace MichelMichels.Wpf.Controls;

[TemplatePart(Name = Part.LoadingGradientBrush, Type = typeof(LinearGradientBrush))]
public class Placeholder : BaseControl
{
    public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
        nameof(BackgroundColor),
        typeof(Color),
        typeof(Placeholder),
        new PropertyMetadata(Color.FromRgb(0xee, 0xee, 0xee)));

    public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register(
        nameof(ForegroundColor),
        typeof(Color),
        typeof(Placeholder),
        new PropertyMetadata(Color.FromRgb(0xdd, 0xdd, 0xdd)));

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(Placeholder),
        new PropertyMetadata(new CornerRadius(0)));

    private LinearGradientBrush? _brush;

    static Placeholder()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Placeholder),
            new FrameworkPropertyMetadata(typeof(Placeholder)));
    }

    public override void OnApplyTemplate()
    {
        Window.GetWindow(this).SizeChanged += Window_SizeChanged;

        TryGetTemplateChild(nameof(Part.LoadingGradientBrush), out _brush);

        base.OnApplyTemplate();
    }

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    public Color ForegroundColor
    {
        get => (Color)GetValue(ForegroundColorProperty);
        set => SetValue(ForegroundColorProperty, value);
    }
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_brush == null)
        {
            return;
        }

        _brush.StartPoint = new Point(0, e.NewSize.Height / 2);
        _brush.EndPoint = new Point(e.NewSize.Width, e.NewSize.Height / 2);
    }
}