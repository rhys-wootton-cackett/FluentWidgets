using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using FluentWidgets.Helpers.Objects;
using FluentWidgets.Models.MainMenu;
using ModernWpf.Controls;

namespace FluentWidgets.ViewModels.MainMenu
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        private MainMenuModel _mmModel = new MainMenuModel();

        public event PropertyChangedEventHandler PropertyChanged;
        public List<MainMenuItem> MainMenuTopItems { get; }
        public List<MainMenuItem> MainMenuBottomItems { get; }

        public MainMenuViewModel()
        {
            this.MainMenuTopItems = _mmModel.GenerateTopMenuItems();
            this.MainMenuBottomItems = _mmModel.GenerateBottomMenuItems();
        }
    }
}