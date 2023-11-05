using CommunityToolkit.Mvvm.ComponentModel;

namespace MichelsMichels.Controls.Demo;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _imagePath = @"Resources\food.jpg";
}
