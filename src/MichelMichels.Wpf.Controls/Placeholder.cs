using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MichelMichels.Wpf.Controls;

[TemplatePart(Name = "PART_LoadingGradientBrush", Type = typeof(LinearGradientBrush))]
public class Placeholder : Control
{
    private LinearGradientBrush? brush;

    public Placeholder()
    {
        if (Application.Current.MainWindow != null)
        {
            Application.Current.MainWindow.SizeChanged += MainWindow_SizeChanged;
        }
    }

    static Placeholder()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Placeholder), new FrameworkPropertyMetadata(typeof(Placeholder)));
    }

    public override void OnApplyTemplate()
    {
        brush = GetTemplateChild("PART_LoadingElement_GradientBrush") as LinearGradientBrush;

        base.OnApplyTemplate();
    }



    public Color BackgroundColor
    {
        get { return (Color)GetValue(BackgroundColorProperty); }
        set { SetValue(BackgroundColorProperty, value); }
    }

    public static readonly DependencyProperty BackgroundColorProperty =
        DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(Placeholder), new PropertyMetadata(Color.FromRgb(0xee, 0xee, 0xee)));



    public Color ForegroundColor
    {
        get { return (Color)GetValue(ForegroundColorProperty); }
        set { SetValue(ForegroundColorProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ForegroundColor.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ForegroundColorProperty =
        DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(Placeholder), new PropertyMetadata(Color.FromRgb(0xdd, 0xdd, 0xdd)));

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Placeholder), new PropertyMetadata(new CornerRadius(0)));

    private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (brush != null)
        {
            double windowWidth = Application.Current.MainWindow.ActualWidth;
            double windowHeight = Application.Current.MainWindow.ActualHeight;

            brush.StartPoint = new Point(0, windowHeight / 2);
            brush.EndPoint = new Point(windowWidth, windowHeight / 2);
        }
    }
}
