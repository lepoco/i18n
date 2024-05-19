// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n.Wpf;

public static class ApplicationExtensions
{
    public static Application UseStringLocalizer(
        this Application app,
        Action<LocalizationBuilder> configure
    )
    {
        LocalizationBuilder builder = new();

        configure(builder);

        LocalizationProvider localizer = new(CultureInfo.CurrentUICulture, builder.GetLocalizations());
        LocalizationProvider.SetInstance(localizer);

        return app;
    }
}
