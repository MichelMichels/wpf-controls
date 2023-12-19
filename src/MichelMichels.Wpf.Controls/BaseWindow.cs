using System.Windows;

namespace MichelMichels.Wpf.Controls;

public abstract class BaseWindow : Window
{
    internal bool TryGetTemplateChild<T>(string templateChildName, out T? templateChild) where T : class
    {
        templateChild = GetTemplateChild(templateChildName) as T;

        return templateChild != null;
    }
}
