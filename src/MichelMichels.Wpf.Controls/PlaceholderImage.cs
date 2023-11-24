using MichelMichels.Wpf.Controls.Resources;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MichelMichels.Wpf.Controls;

[TemplatePart(Name = Part.ImageBrush, Type = typeof(ImageBrush))]
[TemplatePart(Name = Part.Placeholder, Type = typeof(Placeholder))]
public class PlaceholderImage : BaseControl
{
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
        nameof(Stretch),
        typeof(Stretch),
        typeof(PlaceholderImage),
        new PropertyMetadata(Stretch.UniformToFill));

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(PlaceholderImage),
        new PropertyMetadata(new CornerRadius(0)));

    public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
        nameof(Path),
        typeof(string),
        typeof(PlaceholderImage),
        new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPathChanged)));

    public static readonly RoutedEvent ImageLoadedEvent = EventManager.RegisterRoutedEvent(
        nameof(ImageLoaded),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(PlaceholderImage));

    public static readonly RoutedEvent ImageLoadingEvent = EventManager.RegisterRoutedEvent(
       nameof(ImageLoading),
       RoutingStrategy.Bubble,
       typeof(RoutedEventHandler),
       typeof(PlaceholderImage));

    private ImageBrush? _imageBrush;
    private Placeholder? _placeholder;

    static PlaceholderImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderImage),
            new FrameworkPropertyMetadata(typeof(PlaceholderImage)));
    }

    public override void OnApplyTemplate()
    {
        TryGetTemplateChild(Part.ImageBrush, out _imageBrush);
        TryGetTemplateChild(Part.Placeholder, out _placeholder);

        base.OnApplyTemplate();
    }

    public Stretch Stretch
    {
        get => (Stretch)GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }
    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public event RoutedEventHandler ImageLoaded
    {
        add => AddHandler(ImageLoadedEvent, value);
        remove => RemoveHandler(ImageLoadedEvent, value);
    }
    public event RoutedEventHandler ImageLoading
    {
        add => AddHandler(ImageLoadingEvent, value);
        remove => RemoveHandler(ImageLoadingEvent, value);
    }

    private static async void OnPathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        if (obj is not PlaceholderImage placeholderImage)
        {
            Debug.WriteLine($"{nameof(obj)} is not a type of {nameof(PlaceholderImage)}");
            return;
        }

        if (args.NewValue == null)
        {
            Debug.WriteLine($"{args.NewValue} is null");
            return;
        }

        if (args.NewValue is not string newPath)
        {
            Debug.WriteLine($"{args.NewValue} is not a string");
            return;
        }

        if (string.IsNullOrWhiteSpace(newPath))
        {
            Debug.WriteLine($"{newPath} is null or whitespace");
            return;
        }

        try
        {
            RoutedEventArgs imageLoadingArgs = new(ImageLoadingEvent);
            placeholderImage.RaiseEvent(imageLoadingArgs);

            if (!Uri.TryCreate(newPath, UriKind.Absolute, out _))
            {
                newPath = System.IO.Path.Combine(Environment.CurrentDirectory, newPath);
            }
            ImageSource picture = BitmapFrame.Create(new Uri(newPath), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);

            await Task.Delay(500);

            imageLoadingArgs.Handled = true;
            placeholderImage.RaiseEvent(new RoutedEventArgs(ImageLoadedEvent));

            if (placeholderImage._imageBrush != null)
            {
                placeholderImage._imageBrush.Stretch = placeholderImage.Stretch;
                placeholderImage._imageBrush.ImageSource = picture;
            }
        }
        catch (UriFormatException)
        {
            Debug.WriteLine("Maybe not an image");
        }
        catch (FileNotFoundException)
        {
            Debug.WriteLine("File not found");
        }
        catch (NotSupportedException)
        {
            Debug.WriteLine("File not supported.");
        }
    }
}
