# 🏃 Lepo i18n

[Created with ❤ in Poland by lepo.co](https://dev.lepo.co/)  
Lepo.i18n is an essential tool for developers aiming to create multilingual applications in WPF, WinForms, or CLI environments. By harnessing the capabilities of Dependency Injection (DI) and localization resources, Lepo.i18n facilitates better management of language resources, reducing complexity and enhancing the adaptability of your applications to different languages and cultures. With its support for JSON, WPF, and YAML formats, Lepo.i18n ensures seamless integration of localization into your project, making it a go-to solution for your internationalization needs.

[![GitHub license](https://img.shields.io/github/license/lepoco/i18n)](https://github.com/lepoco/i18n/blob/master/LICENSE) [![Nuget](https://img.shields.io/nuget/v/Lepo.i18n)](https://www.nuget.org/packages/Lepo.i18n/) [![Nuget](https://img.shields.io/nuget/dt/Lepo.i18n?label=nuget)](https://www.nuget.org/packages/Lepo.i18n/) [![Sponsors](https://img.shields.io/github/sponsors/lepoco)](https://github.com/sponsors/lepoco)

## 👀 What does this repo contain?

This repository contains the source code for the Lepo.i18n NuGet packages.

## Gettings started

Lepo.i18n is available as NuGet package on NuGet.org:  
https://www.nuget.org/packages/Lepo.i18n  
https://www.nuget.org/packages/Lepo.i18n.DependencyInjection  
https://www.nuget.org/packages/Lepo.i18n.Wpf  
https://www.nuget.org/packages/Lepo.i18n.Json

You can add it to your project using .NET CLI:

```powershell
dotnet add package Lepo.i18n.Wpf
dotnet add package Lepo.i18n.DependencyInjection
```

, or package manager console:

```powershell
NuGet\Install-Package Lepo.i18n.Wpf
NuGet\Install-Package Lepo.i18n.DependencyInjection
```

### 🛠️ How to Use Lepo i18n

#### 1. Read localizations

In this step, we register our localizations in the DI container.

```csharp
IHost host = Host.CreateDefaultBuilder()
  .ConfigureServices((context, services) =>
    {
      services.AddStringLocalizer(b =>
      {
        b.FromResource<Translations>(new("pl-PL"));
        b.FromYaml(assembly, "Lepo.i18n.Resources.Translations-en.yaml", new("en-US"));
      });
    }
  )
  .Build();
```

#### 2. Localize!

```xml
<Grid>
  <CheckBox
    Grid.Row="1"
    Content="{i18n:StringLocalizer 'Leave the keys that were pressed marked.'}"
    IsChecked="{Binding ViewModel.PreviewKeys, Mode=TwoWay}" />
</Grid>
```

## Compilation

To build the project, use Visual Studio 2022 and open the .sln file.

Visual Studio  
**Lepo.i18n** is an Open Source project. You are entitled to download and use the freely available Visual Studio Community Edition to build, run or develop for Lepo.i18n. As per the Visual Studio Community Edition license, this applies regardless of whether you are an individual or a corporate user.

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.

## License

**Lepo.i18n** is free and open source software licensed under **MIT License**. You can use it in private and commercial projects.  
Keep in mind that you must include a copy of the license in your project.
