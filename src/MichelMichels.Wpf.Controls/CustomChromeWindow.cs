using MichelMichels.Wpf.Controls.Internal;
using MichelMichels.Wpf.Controls.Resources;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Shell;

namespace MichelMichels.Wpf.Controls;

public class CustomChromeWindow : BaseWindow
{
    public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register(
        nameof(TitlebarHeight),
        typeof(double),
        typeof(CustomChromeWindow),
        new PropertyMetadata(48.0));

    public static readonly DependencyProperty ExtendsContentIntoTitleBarProperty = DependencyProperty.Register(
        nameof(ExtendsContentIntoTitleBar),
        typeof(bool),
        typeof(CustomChromeWindow),
        new PropertyMetadata(false, new PropertyChangedCallback(OnExtendsContentIntoTitleBarChanged)));

    private AdornerDecorator? _windowContent;

    public CustomChromeWindow()
    {
        MouseLeftButtonDown += (_, _) => { DragMove(); };
    }

    static CustomChromeWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomChromeWindow),
            new FrameworkPropertyMetadata(typeof(CustomChromeWindow)));
    }

    public override void OnApplyTemplate()
    {
        WindowChrome.SetWindowChrome(this, new WindowChrome()
        {
            CaptionHeight = TitlebarHeight
        });

        if (TryGetTemplateChild(Part.WindowContent, out _windowContent))
        {
            SetWindowContentMargin();
        }

        base.OnApplyTemplate();
    }

    public double TitlebarHeight
    {
        get => (double)GetValue(TitlebarHeightProperty);
        set => SetValue(TitlebarHeightProperty, value);
    }

    public bool ExtendsContentIntoTitleBar
    {
        get => (bool)GetValue(ExtendsContentIntoTitleBarProperty);
        set => SetValue(ExtendsContentIntoTitleBarProperty, value);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        if (PresentationSource.FromVisual(this) as HwndSource is { } hwndSource)
        {
            hwndSource.AddHook(NativeMethods.HookProc);
        }
    }

    private void SetWindowContentMargin()
    {
        _windowContent!.Margin = ExtendsContentIntoTitleBar ? new Thickness(0) : new Thickness(0, TitlebarHeight, 0, 0);
    }

    private static void OnExtendsContentIntoTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not CustomChromeWindow window)
        {
            return;
        }

        window.SetWindowContentMargin();
    }
}
