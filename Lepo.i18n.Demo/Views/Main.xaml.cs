using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Lepo.i18n.Demo.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public string TranslatedTextPreparedInBackend { get; set; }

        public int CatsCount { get; set; }

        public Main()
        {
            // We need to use a page if we want to reload it.

            InitializeComponent();

            CatsCount = 2;

            TranslatedTextPreparedInBackend = Translator.Prepare(
                "home.multiplePrepare",
                Translator.String("home.multiplePrepare.inside"),
                88,
                512.55d
            );

            DataContext = this;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // The languages were loaded in the App class OnStartup method.

            switch (Translator.Current)
            {
                case "pl_PL":
                    // This language is dynamically loaded after other languages have been initialized on startup.
                    await Translator.SetLanguageAsync(Assembly.GetExecutingAssembly(), "de_DE", "Lepo.i18n.Demo.Strings.de_DE.yml");
                    break;

                case "de_DE":
                    await Translator.SetLanguageAsync("en_US");
                    break;

                default:
                    await Translator.SetLanguageAsync("pl_PL");
                    break;
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Current language: {Translator.Current}");
#endif

            (Application.Current.MainWindow as MainWindow)?.LoadFrame();
        }
    }
}