using Avalonia.Controls.Templates;
using Avalonia.Controls;

namespace AvaloniaUIDashboard
{
    public class ViewLocator : IDataTemplate
    {
        public IControl Build(object data)
        {
            string name = data.GetType().FullName!.Replace("ViewModel", "View");
            Type? type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ObservableObject;
        }
    }
}
