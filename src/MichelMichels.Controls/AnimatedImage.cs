using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MichelMichels.Controls;

[TemplatePart(Name = "PART_ImageBrush", Type = typeof(ImageBrush))]
[TemplatePart(Name = "PART_Placeholder", Type = typeof(Placeholder))]
public class AnimatedImage : Control
{
    private ImageBrush? imageBrush;
    private Placeholder? placeholder;

    static AnimatedImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedImage), new FrameworkPropertyMetadata(typeof(AnimatedImage)));
    }

    public override void OnApplyTemplate()
    {
        imageBrush = GetTemplateChild("PART_ImageBrush") as ImageBrush;
        placeholder = GetTemplateChild("PART_Placeholder") as Placeholder;

        base.OnApplyTemplate();
    }

    #region Properties
    public Stretch Stretch
    {
        get { return (Stretch)GetValue(StretchProperty); }
        set { SetValue(StretchProperty, value); }
    }
    public string Path
    {
        get { return (string)GetValue(PathProperty); }
        set { SetValue(PathProperty, value); }
    }
    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }
    #endregion

    #region Static property backing fields
    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register("Stretch", typeof(Stretch), typeof(AnimatedImage), new PropertyMetadata(Stretch.UniformToFill, new PropertyChangedCallback(OnStretchChanged), new CoerceValueCallback(CoerceStretch)));

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AnimatedImage), new PropertyMetadata(new CornerRadius(0)));

    public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register("Path", typeof(string), typeof(AnimatedImage), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPathChanged), new CoerceValueCallback(CoercePath)));
    #endregion

    private static object CoercePath(DependencyObject d, object value)
    {
        Debug.WriteLine($"{nameof(CoercePath)}: {value}");
        return value;
    }

    private static async void OnPathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        if (obj is not AnimatedImage animatedImage)
        {
            Debug.WriteLine($"{nameof(obj)} is not a type of {nameof(AnimatedImage)}");
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
            animatedImage.RaiseEvent(imageLoadingArgs);

            if (!Uri.TryCreate(newPath, UriKind.Absolute, out _))
            {
                newPath = System.IO.Path.Combine(Environment.CurrentDirectory, newPath);
            }
            ImageSource picture = BitmapFrame.Create(new Uri(newPath), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);

            await Task.Delay(500);

            imageLoadingArgs.Handled = true;
            animatedImage.RaiseEvent(new RoutedEventArgs(ImageLoadedEvent));

            if (animatedImage.imageBrush != null)
            {
                animatedImage.imageBrush.Stretch = animatedImage.Stretch;
                animatedImage.imageBrush.ImageSource = picture;
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
        finally
        {
        }
    }

    private static object CoerceStretch(DependencyObject d, object value)
    {
        Debug.WriteLine($"{nameof(CoerceStretch)}: {value}");
        return value;
    }

    private static void OnStretchChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        Debug.WriteLine($"{nameof(OnStretchChanged)}: {args.OldValue} -> {args.NewValue}");
    }

    public static readonly RoutedEvent ImageLoadedEvent = EventManager.RegisterRoutedEvent(
        name: "ImageLoaded",
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(AnimatedImage));

    public event RoutedEventHandler ImageLoaded
    {
        add { AddHandler(ImageLoadedEvent, value); }
        remove { RemoveHandler(ImageLoadedEvent, value); }
    }

    public static readonly RoutedEvent ImageLoadingEvent = EventManager.RegisterRoutedEvent(
        name: "ImageLoading",
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(RoutedEventHandler),
        ownerType: typeof(AnimatedImage));

    public event RoutedEventHandler ImageLoading
    {
        add { AddHandler(ImageLoadingEvent, value); }
        remove { RemoveHandler(ImageLoadingEvent, value); }
    }
}
