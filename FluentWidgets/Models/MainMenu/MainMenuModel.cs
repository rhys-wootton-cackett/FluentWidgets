using System.Collections.Generic;
using System.Windows.Media;
using FluentWidgets.Helpers.Objects;
using ModernWpf.Controls;

namespace FluentWidgets.Models.MainMenu
{
    public class MainMenuModel
    {
        public List<MainMenuItem> GenerateTopMenuItems()
        {
            return new List<MainMenuItem>
            {
                new MainMenuItem
                {
                    Content = "Widgets",
                    Icon = new FontIcon {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\uE71D"},
                    MenuItems = new List<MainMenuItem>
                    {
                        new MainMenuItem
                        {
                            Content = "Date, Time and Calendar",
                            Icon = new FontIcon {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\uE787"}
                        },
                        new MainMenuItem
                        {
                            Content = "Weather",
                            Icon = new FontIcon {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\uE706"}
                        },
                        new MainMenuItem
                        {
                            Content = "Processor",
                            Icon = new FontIcon {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\uE950"}
                        }
                    }
                }
            };
        }
        public List<MainMenuItem> GenerateBottomMenuItems()
        {
            return new List<MainMenuItem>
            {
                new MainMenuItem
                {
                    Content = "Settings",
                    Icon = new FontIcon {FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = "\uE713"}
                }
            };
        }
    }
}
