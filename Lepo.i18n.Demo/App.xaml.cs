// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Reflection;
using System.Windows;

namespace Lepo.i18n.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            WPFUI.Theme.Watcher.Start(true, true);

            Translator.SetLanguage(Assembly.GetExecutingAssembly(), "en_US", "Lepo.i18n.Demo.Strings.en_US.yaml");
        }
    }
}
