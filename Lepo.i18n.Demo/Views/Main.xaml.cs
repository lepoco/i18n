using System.Windows;
using System.Windows.Controls;

namespace Lepo.i18n.Demo.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            // We need to use a page if we want to reload it.

            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // The languages were loaded in the App class OnStartup method.

            switch (Translator.Current)
            {
                case "pl_PL":
                    await Translator.SetLanguageAsync("de_DE");
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
