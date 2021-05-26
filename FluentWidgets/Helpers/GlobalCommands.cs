using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FluentWidgets.Helpers
{
    public class GlobalCommands
    {
        public static readonly ICommand CloseCommand = new DelegateCommand(o =>
        {
            var widget = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.Title.Equals(o));
            widget?.Close();
        });
    }
}