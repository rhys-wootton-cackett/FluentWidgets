﻿using System.Windows;
using FluentWidgets.Widgets;

namespace FluentWidgets
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var widget = new WeatherLarge();
            widget.Show();
        }
    }
}