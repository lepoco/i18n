// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using Microsoft.Extensions.Localization;
using Wpf.Ui.Appearance;
using Lepo.i18n;
using Wpf.Ui.Controls;
using Galery.Models;
using Newtonsoft.Json;
using System.IO;

namespace Galery.ViewModels.Pages;
public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    private readonly IStringLocalizer _stringLocalizer;
    private readonly ILocalizationCultureManager _cultureManager;

    public SettingsViewModel(IStringLocalizer stringLocalizer, ILocalizationCultureManager cultureManager)
    {
        _stringLocalizer = stringLocalizer;
        _cultureManager = cultureManager;
    }

    [ObservableProperty]
    private string _appVersion = String.Empty;

    [ObservableProperty]
    private bool _isEnglish = true;

    [ObservableProperty]
    private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom() { }

    private void InitializeViewModel()
    {
        AppVersion = _stringLocalizer["Lan"].Value;
        IsEnglish = _cultureManager.GetCulture().EnglishName == "English (United States)";
        CurrentTheme = ApplicationThemeManager.GetAppTheme();
        //AppVersion = $"UiDesktopApp1 - {GetAssemblyVersion()}";
        _isInitialized = true;
    }

    private string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? String.Empty;
    }

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch (parameter)
        {
            case "theme_light":
                if (CurrentTheme == ApplicationTheme.Light)
                    break;

                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                CurrentTheme = ApplicationTheme.Light;

                break;

            default:
                if (CurrentTheme == ApplicationTheme.Dark)
                    break;

                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                CurrentTheme = ApplicationTheme.Dark;

                break;
        }
    }

    [RelayCommand]
    private void OnChangeLan(bool isChecked)
    {
        if (isChecked)
        {
            _cultureManager.SetCulture("en-US");
            File.WriteAllText(@"CultureInfos.json", JsonConvert.SerializeObject(new LanInfo() { CultureInfo = "en-US" }));
        }
        else
        {
            _cultureManager.SetCulture("fa-IR");
            File.WriteAllText(@"CultureInfos.json", JsonConvert.SerializeObject(new LanInfo() { CultureInfo = "fa-IR" }));
        }

        AppVersion = _stringLocalizer["Lan"].Value;

        System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
        App.Current.Shutdown();
    }
}
