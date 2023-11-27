using MichelMichels.Wpf.Controls.Internal;
using MichelMichels.Wpf.Controls.Resources;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Shell;

namespace MichelMichels.Wpf.Controls;

public class ChromeWindow : BaseWindow
{
    // TODO: Minimizing animation gone: https://stackoverflow.com/a/21419172
    // TODO: No snap layout support in Windows 11

    public static readonly DependencyProperty TitlebarHeightProperty = DependencyProperty.Register(
        nameof(TitlebarHeight),
        typeof(double),
        typeof(ChromeWindow),
        new PropertyMetadata(48.0));

    public static readonly DependencyProperty ExtendsContentIntoTitleBarProperty = DependencyProperty.Register(
        nameof(ExtendsContentIntoTitleBar),
        typeof(bool),
        typeof(ChromeWindow),
        new PropertyMetadata(false, new PropertyChangedCallback(OnExtendsContentIntoTitleBarChanged)));

    public static readonly DependencyProperty WindowControlsStyleProperty = DependencyProperty.Register(
        nameof(WindowControlsStyle),
        typeof(Style),
        typeof(ChromeWindow),
        new PropertyMetadata(null));

    private AdornerDecorator? _windowContent;

    public ChromeWindow()
    {
        MouseLeftButtonDown += (_, _) => { DragMove(); };
    }

    static ChromeWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ChromeWindow),
            new FrameworkPropertyMetadata(typeof(ChromeWindow)));
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

    public Style WindowControlsStyle
    {
        get => (Style)GetValue(WindowControlsStyleProperty);
        set => SetValue(WindowControlsStyleProperty, value);
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
        if (_windowContent == null)
        {
            return;
        }

        _windowContent.Margin = ExtendsContentIntoTitleBar ? new Thickness(0) : new Thickness(0, TitlebarHeight, 0, 0);
    }

    private static void OnExtendsContentIntoTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ChromeWindow window)
        {
            return;
        }

        window.SetWindowContentMargin();
    }
}
