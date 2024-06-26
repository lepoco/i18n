// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.DependencyInjection;

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
        DependencyInjectionLocalizationBuilder builder = new(services);

        configure(builder);

        LocalizationProviderFactory.SetInstance(builder.Build());

        _ = services.AddSingleton(_ => LocalizationProviderFactory.GetInstance()!);
        _ = services.AddTransient<IStringLocalizerFactory, ProviderBasedStringLocalizerFactory>();
        _ = services.AddTransient<ILocalizationCultureManager, LocalizationCultureManager>();
        _ = services.AddTransient<IStringLocalizer, ProviderBasedStringLocalizer>();

        return services;
    }
}
