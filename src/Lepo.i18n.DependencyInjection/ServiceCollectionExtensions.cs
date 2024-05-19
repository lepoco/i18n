// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the string localizer and related services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configure">A delegate to configure the localization builder.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddStringLocalizer(
        this IServiceCollection services,
        Action<LocalizationBuilder> configure
    )
    {
        LocalizationBuilder builder = new();

        configure(builder);

        LocalizationProvider localizer =
            new(CultureInfo.CurrentCulture, builder.GetLocalizations());
        LocalizationProvider.SetInstance(localizer);

        _ = services.AddSingleton<ILocalizationProvider>(
            (_) => LocalizationProvider.GetInstance()!
        );
        _ = services.AddTransient<ILocalizationCultureManager, LocalizationCultureManager>();
        _ = services.AddTransient<IStringLocalizer, StringLocalizer>();

        return services;
    }
}
