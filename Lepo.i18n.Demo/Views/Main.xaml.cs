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
        public Main()
        {
            // We need to use a page if we want to reload it.

            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            switch (Translator.Current)
            {
                case "pl_PL":
                    Translator.SetLanguage(Assembly.GetExecutingAssembly(), "de_DE", "Lepo.i18n.Demo.Strings.de_DE.yaml");
                    break;

                case "de_DE":
                    Translator.SetLanguage(Assembly.GetExecutingAssembly(), "en_US", "Lepo.i18n.Demo.Strings.en_US.yaml");
                    break;

                default:
                    Translator.SetLanguage(Assembly.GetExecutingAssembly(), "pl_PL", "Lepo.i18n.Demo.Strings.pl_PL.yaml");
                    break;
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Current language: {Translator.Current}");
#endif

            (Application.Current.MainWindow as MainWindow)?.LoadFrame();
        }
    }
}
