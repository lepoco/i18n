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
    /// Adds a string localizer to the specified services collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the string localizer to.</param>
    /// <param name="configure">An action to configure the <see cref="StringLocalizerBuilder"/>.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    /// <remarks>
    /// This method configures a <see cref="StringLocalizerBuilder"/>, creates a <see cref="StringLocalizer"/> from it,
    /// sets the <see cref="StringLocalizer"/> as the instance in the <see cref="StringLocalizerFactory"/>,
    /// and then adds the <see cref="ILocalizationManager"/> and <see cref="IStringLocalizer"/> services to the services collection.
    /// </remarks>
    public static IServiceCollection AddStringLocalizer(
        this IServiceCollection services,
        Action<StringLocalizerBuilder> configure
    )
    {
        StringLocalizerBuilder builder = new();

        configure(builder);

        StringLocalizer localizer = new(builder.GetLocalizations());

        StringLocalizerFactory.SetInstance(localizer);

        _ = services.AddSingleton<ILocalizationManager, LocalizationManager>();
        _ = services.AddSingleton<IStringLocalizer>((_) => StringLocalizerFactory.GetInstance()!);

        return services;
    }
}
