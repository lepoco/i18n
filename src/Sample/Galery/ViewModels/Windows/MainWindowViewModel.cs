// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Galery.ViewModels.Windows;
public partial class MainWindowViewModel : ObservableObject
{

    private readonly IStringLocalizer _stringLocalizer;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> _menuItems;

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems;

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new()
    {
        new MenuItem { Header = "Home", Tag = "tray_home" }
    };

    public MainWindowViewModel(IStringLocalizer stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
        ApplicationTitle = _stringLocalizer["Titele"].Value;
        MenuItems =
            [
                new NavigationViewItem()
                {
                    Content = _stringLocalizer["Menu.Dashboard"].Value,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationViewItem()
                {
                    Content =_stringLocalizer["Menu.Data"].Value,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                    TargetPageType = typeof(Views.Pages.DataPage)
                }
            ];

        FooterMenuItems =
            [
                new NavigationViewItem()
                {
                    Content = _stringLocalizer["Menu.Settings"].Value,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(Views.Pages.SettingsPage)
                }
            ];
    }

}
