// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Lepo.i18n.Demo.Views;
using System.Windows;
using System.Windows.Navigation;

namespace Lepo.i18n.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WPFUI.Background.Manager.Apply(this);

            InitializeComponent();

            LoadFrame();
        }

        public void LoadFrame()
        {
            RootFrame.Navigate(new Main());
            RootFrame.NavigationService.RemoveBackEntry();
            RootFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
    }
}
