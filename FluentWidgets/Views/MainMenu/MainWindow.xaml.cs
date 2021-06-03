using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Navigation;
using FluentWidgets.ViewModels.MainMenu;
using FluentWidgets.Views.Widgets;
using ModernWpf.Controls;

namespace FluentWidgets.Views.MainMenu
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainMenuViewModel _mmVieWModel = new MainMenuViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mmVieWModel;
        }
    }
}