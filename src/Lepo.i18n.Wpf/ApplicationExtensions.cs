// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf;

/// <summary>
/// Provides extension methods for the <see cref="Application"/> class.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    /// Configures the application to use a string localizer.
    /// </summary>
    /// <param name="app">The application to configure.</param>
    /// <param name="configure">A delegate to configure the localization builder.</param>
    /// <returns>The configured application.</returns>
    public static Application UseStringLocalizer(
        this Application app,
        Action<LocalizationBuilder> configure
    )
    {
        LocalizationBuilder builder = new();

        configure(builder);

        LocalizationProvider localizer =
            new(CultureInfo.CurrentUICulture, builder.GetLocalizations());

        LocalizationProvider.SetInstance(localizer);

        return app;
    }

    /// <summary>
    /// Sets the culture for localization in the application.
    /// </summary>
    /// <param name="app">The application to set the culture for.</param>
    /// <param name="culture">The culture to set.</param>
    /// <returns>The application with the set culture.</returns>
    public static Application SetLocalizationCulture(this Application app, CultureInfo culture)
    {
        LocalizationProvider.GetInstance()?.SetCulture(culture);

        return app;
    }
}
