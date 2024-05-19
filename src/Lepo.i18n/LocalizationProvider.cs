// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and Lepo.i18n Contributors.
// All Rights Reserved.

namespace Lepo.i18n;

public class LocalizationProvider(CultureInfo culture, IEnumerable<LocalizationSet> sets) : ILocalizationProvider
{
    private static LocalizationProvider? instance;

    public LocalizationSet? Get(CultureInfo culture)
    {
        return sets.FirstOrDefault(s => s.Culture.Equals(culture));
    }

    public LocalizationSet? Get(string name, CultureInfo culture)
    {
        return sets.FirstOrDefault(s => s.Culture.Equals(culture) && s.Name == name);
    }

    public CultureInfo GetCulture()
    {
        return culture;
    }

    public void SetCulture(CultureInfo cultureInfo)
    {
        culture = cultureInfo;
    }

    public static LocalizationProvider? GetInstance()
    {
        return instance;
    }

    public static void SetInstance(LocalizationProvider provider)
    {
        instance = provider;
    }
}
