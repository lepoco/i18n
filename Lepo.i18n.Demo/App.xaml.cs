// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

using System.Collections.Generic;
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

            var langPath = "Lepo.i18n.Demo.Strings.";

            Translator.LoadLanguages(
                Assembly.GetExecutingAssembly(),
                new Dictionary<string, string>
                {
                    {"en_US", langPath + "en_US.yml"},
                    {"pl_PL", langPath + "pl_PL.yml"},
                    {"de_DE", langPath + "de_DE.yml"},
                }
            );

            Translator.SetLanguage("en_US");
        }
    }
}
