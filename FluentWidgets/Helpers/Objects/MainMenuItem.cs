using System.Collections.Generic;
using FluentWidgets.Properties;
using ModernWpf.Controls;

namespace FluentWidgets.Helpers.Objects
{
    public class MainMenuItem
    {
        public string Content { get; set; }
        public string ToolTip => this.Content;
        public FontIcon Icon { get; set; }
        public Page Page { get; set; }
        public List<MainMenuItem> MenuItems { get; set; }
    }
}
