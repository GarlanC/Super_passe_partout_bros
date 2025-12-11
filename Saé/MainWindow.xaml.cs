using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Saé
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Perso { get; set; }
        public static int pasPerso { get; set; } = 5;

        public MainWindow()
        {
            InitializeComponent();
            AfficheMenu();
        }

        private void AfficheMenu()
        {
            UCMenu uc = new UCMenu();
            ZoneJeu.Content = uc;
            uc.butJouer.Click += AfficherChoixPerso;
            uc.butParametres.Click += AfficherParametres;
        }

        private void AfficherParametres(object sender, RoutedEventArgs e)
        {
            UCParametres uc = new UCParametres();
            ZoneJeu.Content = uc;
            uc.butRetourParametre.Click += RetourParametre_Click;
        }

        private void AfficherChoixPerso(object sender, RoutedEventArgs e)
        {
            UCChoixPerso uc = new UCChoixPerso();
            ZoneJeu.Content = uc;
            uc.butJouer.Click += ChoixNiveau;
        }

        private void ChoixNiveau(object sender, RoutedEventArgs e)
        {
            UCChoixNiveau uc = new UCChoixNiveau();
            ZoneJeu.Content = uc;
            uc.butRetourNiveaux.Click += RetourNiveaux_Click;
            uc.butDesert.Click += LancerDesert;
            uc.butForet.Click += LancerForet;
            uc.butPlage.Click += LancerPlage;

        }

        private void LancerPlage(object sender, RoutedEventArgs e)
        {
            UCPlage uc = new UCPlage();
            ZoneJeu.Content = uc;
        }

        private void LancerDesert(object sender, RoutedEventArgs e)
        {
            UCDesert uc = new UCDesert();
            ZoneJeu.Content = uc;
        }

        private void LancerForet(object sender, RoutedEventArgs e)
        {
            UCForet uc = new UCForet();
            ZoneJeu.Content = uc;
        }

        private void RetourNiveaux_Click(object sender, RoutedEventArgs e)
        {
            AfficheMenu();
        }

        private void RetourParametre_Click(object sender, RoutedEventArgs e)
        {
            AfficheMenu();
        }
    }
}