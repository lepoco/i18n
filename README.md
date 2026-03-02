<div align="center">
    <img src="https://github.com/lepoco/i18n/blob/main/build/nuget.png?raw=true" width="128" alt="Lepo.i18n logo"/>
    <h1>Lepo.i18n</h1>
    <h3><em>Internationalization library for .NET applications.</em></h3>
</div>

<p align="center">
    <strong>Add multilingual support to WPF, WinForms, or CLI apps. JSON, YAML, and RESX resources with Dependency Injection.</strong>
</p>

<p align="center">
    <a href="https://www.nuget.org/packages/Lepo.i18n"><img src="https://img.shields.io/nuget/v/Lepo.i18n.svg" alt="NuGet"/></a>
    <a href="https://www.nuget.org/packages/Lepo.i18n"><img src="https://img.shields.io/nuget/dt/Lepo.i18n.svg" alt="NuGet Downloads"/></a>
    <a href="https://github.com/lepoco/i18n/stargazers"><img src="https://img.shields.io/github/stars/lepoco/i18n?style=social" alt="GitHub stars"/></a>
    <a href="https://github.com/lepoco/i18n/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lepoco/i18n" alt="License"/></a>
</p>

<p align="center">
    <a href="https://lepo.co/">Created in Poland by Leszek Pomianowski</a> and <a href="https://github.com/lepoco/i18n/graphs/contributors">open-source community</a>.
</p>

---

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Why This Library?](#why-this-library)
- [Get Started](#get-started)
  - [Install the Packages](#install-the-packages)
- [How to Use](#how-to-use)
  - [1. Register Localizations](#1-register-localizations)
  - [2. Localize in XAML](#2-localize-in-xaml)
  - [3. Change Culture at Runtime](#3-change-culture-at-runtime)
- [Packages](#packages)
- [API Reference](#api-reference)
  - [LocalizationBuilder Methods](#localizationbuilder-methods)
  - [WPF XAML Extensions](#wpf-xaml-extensions)
  - [Culture Management](#culture-management)
- [Maintainers](#maintainers)
- [Support](#support)
- [License](#license)

---

## Why This Library?

Traditional .NET localization with resource files requires ceremony and scattered configuration:

```csharp
// Traditional approach - manual resource loading
ResourceManager rm = new("MyApp.Resources.Strings", typeof(Program).Assembly);
CultureInfo culture = new("pl-PL");
string translated = rm.GetString("HelloWorld", culture) ?? "Hello World";
```

With **Lepo.i18n**, localizations are registered once and consumed anywhere - including directly in XAML:

```csharp
// Lepo.i18n with Dependency Injection
services.AddStringLocalizer(b =>
{
    b.SetCulture("pl-PL");
    b.FromResource<Translations>(new CultureInfo("pl-PL"));
    b.FromResource<Translations>(new CultureInfo("en-US"));
});
```

```xml
<!-- Bind directly in XAML -->
<TextBlock Text="{i18n:StringLocalizer 'Hello World'}" />
```

---

## Get Started

### Install the Packages

```powershell
dotnet add package Lepo.i18n
```

Pick additional packages based on your needs:

```powershell
# Microsoft.Extensions.DependencyInjection integration
dotnet add package Lepo.i18n.DependencyInjection

# Load translations from JSON files
dotnet add package Lepo.i18n.Json

# WPF markup extensions
dotnet add package Lepo.i18n.Wpf
```

**NuGet:** <https://www.nuget.org/packages/Lepo.i18n>

---

## How to Use

### 1. Register Localizations

<details open>
<summary><strong>WPF Application (without DI)</strong></summary>

```csharp
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        this.UseStringLocalizer(b =>
        {
            b.SetCulture(new CultureInfo("pl-PL"));
            b.FromResource<Translations>(new CultureInfo("pl-PL"));
            b.FromResource<Translations>(new CultureInfo("en-US"));
        });
    }
}
```

</details>

<details>
<summary><strong>Generic Host with Dependency Injection</strong></summary>

```csharp
IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddStringLocalizer(b =>
        {
            b.SetCulture("pl-PL");
            b.FromResource<Translations>(new CultureInfo("pl-PL"));
            b.FromJson(assembly, "Resources.Translations-en.json", new CultureInfo("en-US"));
        });
    })
    .Build();
```

</details>

<details>
<summary><strong>Loading from JSON files</strong></summary>

```csharp
services.AddStringLocalizer(b =>
{
    b.SetCulture("en-US");
    b.FromJson(assembly, "Resources.en-US.json", new CultureInfo("en-US"));
    b.FromJson(assembly, "Resources.pl-PL.json", new CultureInfo("pl-PL"));
});
```

</details>

---

### 2. Localize in XAML

Add the i18n XML namespace and use the `StringLocalizer` markup extension:

```xml
<Window
    xmlns:i18n="http://schemas.lepo.co/i18n/2022/xaml">

    <StackPanel>
        <!-- Simple localization -->
        <TextBlock Text="{i18n:StringLocalizer 'Hello World'}" />

        <!-- With string format arguments -->
        <TextBlock Text="{i18n:StringLocalizer 'Test {0}, of {1}!', Arg1='What?', Arg2='No'}" />

        <!-- In any content property -->
        <CheckBox Content="{i18n:StringLocalizer 'Enable notifications'}" />
    </StackPanel>
</Window>
```

---

### 3. Change Culture at Runtime

<details open>
<summary><strong>WPF Application</strong></summary>

```csharp
Application.Current.SetLocalizationCulture(new CultureInfo("en-US"));
```

</details>

<details>
<summary><strong>With ILocalizationCultureManager (DI)</strong></summary>

```csharp
public class SettingsViewModel(ILocalizationCultureManager cultureManager)
{
    public void SwitchToEnglish()
    {
        cultureManager.SetCulture("en-US");
    }
}
```

</details>

---

## Packages

| Package | Description |
|---------|-------------|
| [`Lepo.i18n`](https://www.nuget.org/packages/Lepo.i18n) | Core library - localization builder, provider, and YAML support |
| [`Lepo.i18n.DependencyInjection`](https://www.nuget.org/packages/Lepo.i18n.DependencyInjection) | `IServiceCollection` integration with `IStringLocalizer` |
| [`Lepo.i18n.Json`](https://www.nuget.org/packages/Lepo.i18n.Json) | Load translations from embedded or external JSON files |
| [`Lepo.i18n.Wpf`](https://www.nuget.org/packages/Lepo.i18n.Wpf) | WPF markup extensions (`StringLocalizer`, `PluralStringLocalizer`) |

---

## API Reference

### LocalizationBuilder Methods

| Method | Description |
|--------|-------------|
| `SetCulture(CultureInfo)` | Set the default culture |
| `SetCulture(string)` | Set the default culture by name |
| `AddLocalization(LocalizationSet)` | Add a localization set manually |
| `FromResource<T>(CultureInfo)` | Load from embedded RESX resource |
| `FromJson(path, CultureInfo)` | Load from an embedded JSON file |
| `FromJson(assembly, path, CultureInfo)` | Load from a JSON file in a specific assembly |
| `FromYaml(assembly, path, CultureInfo)` | Load from an embedded YAML file |

### WPF XAML Extensions

| Extension | Description |
|-----------|-------------|
| `{i18n:StringLocalizer 'key'}` | Localize a string in XAML |
| `{i18n:PluralStringLocalizer ...}` | Localize with singular/plural forms |

### Culture Management

| Method | Description |
|--------|-------------|
| `app.UseStringLocalizer(Action)` | Configure localizations for a WPF app |
| `app.SetLocalizationCulture(CultureInfo)` | Change culture at runtime (WPF) |
| `services.AddStringLocalizer(Action)` | Register localizations via DI |
| `ILocalizationCultureManager.SetCulture()` | Change culture via DI service |

---

## Maintainers

- Leszek Pomianowski ([@pomianowski](https://github.com/pomianowski))

---

## Support

For support, please open a [GitHub issue](https://github.com/lepoco/i18n/issues/new). We welcome bug reports, feature requests, and questions.

---

## License

This project is licensed under the terms of the **MIT** open source license. Please refer to the [LICENSE](LICENSE) file for the full terms.

You can use it in private and commercial projects. Keep in mind that you must include a copy of the license in your project.
