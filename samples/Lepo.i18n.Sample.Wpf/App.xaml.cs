// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Globalization;
using System.Windows;
using Lepo.i18n.Sample.Wpf.Resources;
using Lepo.i18n.Wpf;

namespace Lepo.i18n.Sample.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _ = this.UseStringLocalizer(b =>
        {
            b.SetCulture(new CultureInfo("pl-PL"));

            b.FromResource<Translations>(new CultureInfo("pl-PL"));
            b.FromResource<Translations>(new CultureInfo("en-US"));
        });
    }
}
