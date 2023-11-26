using MichelMichels.Wpf.Controls.Resources;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MichelMichels.Wpf.Controls;

[TemplatePart(Name = Part.MinimizeButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.MaximizeButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.RestoreButton, Type = typeof(TitlebarButton))]
[TemplatePart(Name = Part.CloseButton, Type = typeof(TitlebarButton))]
public class WindowControls : BaseControl
{
    private MinimizeButton? _minimizeButton;
    private MaximizeButton? _maximizeButton;
    private RestoreButton? _restoreButton;
    private CloseButton? _closeButton;

    private Window? _window;

    public static readonly DependencyProperty MinimizeButtonStyleProperty = DependencyProperty.Register(
        nameof(MinimizeButtonStyle),
        typeof(Style),
        typeof(WindowControls),
        new PropertyMetadata(null));

    public static readonly DependencyProperty MaximizeButtonStyleProperty = DependencyProperty.Register(
        nameof(MaximizeButtonStyle),
        typeof(Style),
        typeof(WindowControls),
        new PropertyMetadata(null));

    public static readonly DependencyProperty RestoreButtonStyleProperty = DependencyProperty.Register(
        nameof(RestoreButtonStyle),
        typeof(Style),
        typeof(WindowControls),
        new PropertyMetadata(null));

    public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register(
        nameof(CloseButtonStyle),
        typeof(Style),
        typeof(WindowControls),
        new PropertyMetadata(null));

    static WindowControls()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowControls),
            new FrameworkPropertyMetadata(typeof(WindowControls)));
    }

    public Style MinimizeButtonStyle
    {
        get => (Style)GetValue(MinimizeButtonStyleProperty);
        set => SetValue(MinimizeButtonStyleProperty, value);
    }
    public Style MaximizeButtonStyle
    {
        get => (Style)GetValue(MaximizeButtonStyleProperty);
        set => SetValue(MaximizeButtonStyleProperty, value);
    }
    public Style RestoreButtonStyle
    {
        get => (Style)GetValue(RestoreButtonStyleProperty);
        set => SetValue(RestoreButtonStyleProperty, value);
    }
    public Style CloseButtonStyle
    {
        get => (Style)GetValue(CloseButtonStyleProperty);
        set => SetValue(CloseButtonStyleProperty, value);
    }

    public override void OnApplyTemplate()
    {
        _window = Window.GetWindow(this) ?? throw new NotSupportedException();
        _window.StateChanged += OnWindowStateChanged;

        SubscribeToButtonsClickEvent();
        RefreshMaximizeRestoreButton();
        UpdateButtonsState();

        base.OnApplyTemplate();
    }

    private void SubscribeToButtonsClickEvent()
    {
        if (TryGetTemplateChild(Part.MinimizeButton, out _minimizeButton))
            SubscribeToButtonClickEvent(_minimizeButton, OnMinimizeButtonClicked);

        if (TryGetTemplateChild(Part.MaximizeButton, out _maximizeButton))
            SubscribeToButtonClickEvent(_maximizeButton, OnMaximizeButtonClicked);

        if (TryGetTemplateChild(Part.RestoreButton, out _restoreButton))
            SubscribeToButtonClickEvent(_restoreButton, OnRestoreButtonClicked);

        if (TryGetTemplateChild(Part.CloseButton, out _closeButton))
            SubscribeToButtonClickEvent(_closeButton, OnCloseButtonClicked);
    }

    private static void SubscribeToButtonClickEvent(ButtonBase? button, RoutedEventHandler eventHandler)
    {
        if (button == null)
            return;

        button.Click += eventHandler;
    }

    private void RefreshMaximizeRestoreButton()
    {
        var isMaximized = _window!.WindowState == WindowState.Maximized;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;

        if (_restoreButton != null)
            _restoreButton.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
    {
        _window!.Close();
    }

    private void OnRestoreButtonClicked(object sender, RoutedEventArgs e)
    {
        _window!.WindowState = WindowState.Normal;

        if (_restoreButton != null)
            _restoreButton.Visibility = Visibility.Hidden;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = Visibility.Visible;
    }

    private void OnMaximizeButtonClicked(object sender, RoutedEventArgs e)
    {
        _window!.WindowState = WindowState.Maximized;

        if (_restoreButton != null)
            _restoreButton.Visibility = Visibility.Visible;

        if (_maximizeButton != null)
            _maximizeButton.Visibility = Visibility.Hidden;
    }

    private void OnMinimizeButtonClicked(object sender, RoutedEventArgs e)
    {
        _window!.WindowState = WindowState.Minimized;
    }

    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        RefreshMaximizeRestoreButton();
    }
    private void UpdateButtonsState()
    {
        if (_window == null)
        {
            return;
        }

        switch (_window.ResizeMode)
        {
            case ResizeMode.NoResize:
                SetButtonVisibility(_minimizeButton, Visibility.Collapsed);
                SetButtonVisibility(_maximizeButton, Visibility.Collapsed);
                SetButtonVisibility(_restoreButton, Visibility.Collapsed);
                break;
            case ResizeMode.CanMinimize:
                SetButtonEnabled(_maximizeButton, false);
                SetButtonEnabled(_restoreButton, false);
                break;
        }
    }
    private void SetButtonVisibility(TitlebarButton? button, Visibility visibility)
    {
        if (button == null)
        {
            return;
        }

        button.Visibility = visibility;
    }
    private void SetButtonEnabled(TitlebarButton? button, bool isEnabled)
    {
        if (button == null)
        {
            return;
        }

        button.IsEnabled = isEnabled;
    }
}