using MichelMichels.Wpf.Controls;

namespace MichelsMichels.Controls.Demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : CustomChromeWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
