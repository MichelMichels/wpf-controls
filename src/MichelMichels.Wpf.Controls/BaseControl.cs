using System.Windows.Controls;

namespace MichelMichels.Wpf.Controls;

public abstract class BaseControl : Control
{
    internal bool TryGetTemplateChild<T>(string templateChildName, out T? templateChild) where T : class
    {
        templateChild = GetTemplateChild(templateChildName) as T;

        return templateChild != null;
    }
}
