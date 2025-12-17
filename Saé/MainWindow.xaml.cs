using System.Diagnostics;
using System.Media;
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
        public static MediaPlayer musiqueFond { get; private set; }
        public static MediaPlayer bruitageSonPas { get; private set; }
        public static MediaPlayer bruitageSonSaut { get; private set; }
        public static MediaPlayer bruitageSonPiece { get; private set; }
        public static MediaPlayer bruitageSonCle { get; private set; }
        public static string Perso { get; set; }
        public static int pasPerso { get; set; } = 5;
        public static int volumeFond { get; set; } = 100;
        public static string[] bruitages = { "Pas", "Saut", "Piece", "Cle" };

        public MainWindow()
        {
            InitializeComponent();
            bruitageSonPas = InitializeSon("sonMarcheTerre.wav");
            bruitageSonSaut = InitializeSon("sonSaut.wav");
            bruitageSonPiece = InitializeSon("sonPiece.wav");
            bruitageSonCle = InitializeSon("sonCle.wav");
            AfficheMenu();
        }

        public static void InitializeSonFond()
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "son", "sonFond.mp3");

            musiqueFond = new MediaPlayer();
            musiqueFond.Volume = volumeFond / 100;
            musiqueFond.Open(new Uri(path, UriKind.Absolute));
            musiqueFond.MediaEnded += (_, __) => { musiqueFond.Position = TimeSpan.Zero; musiqueFond.Play(); };
        }

        public static MediaPlayer InitializeSon(string sonInit)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "son", sonInit);
            MediaPlayer bruitage = new MediaPlayer();
            bruitage.Volume = volumeFond / 100;
            bruitage.Open(new Uri(path, UriKind.Absolute));
            return bruitage;
        }

        public void AfficheMenu()
        {
            UCMenu uc = new UCMenu();
            ZoneJeu.Content = uc;
            uc.butJouer.Click += AfficherChoixPerso;
            uc.butParametres.Click += AfficherParametres;
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
            uc.butRetourNiveaux.Click += RetourMenu_Click;
            uc.butDesert.Click += LancerDesert;
            uc.butForet.Click += LancerForet;
            uc.butPlage.Click += LancerPlage;

        }

        private void AfficherParametres(object sender, RoutedEventArgs e)
        {
            UCParametres uc = new UCParametres();
            ZoneJeu.Content = uc;
            uc.butRetour.Click += RetourMenu_Click;
            uc.butValider.Click += EnregistreSon;
        }

        public void RetourMenu_Click(object sender, RoutedEventArgs e)
        {
            UCParametres uc = ZoneJeu.Content as UCParametres;
            if (uc != null)
            {
                uc.waitingButton = null;
                uc.PreviewKeyDown -= uc.UCParametres_KeyDown;
            }

            AfficheMenu();
        }

        public void EnregistreSon(object sender, RoutedEventArgs e)
        {
            UCParametres uc = new UCParametres();
            ZoneJeu.Content = uc;
            volumeFond = (int)uc.sliderSon.Value;
            AfficheMenu();
        }

        private void LancerPlage(object sender, RoutedEventArgs e)
        {
            UCPlage uc = new UCPlage();
            ZoneJeu.Content = uc;
            uc.butPause.Click += AfficherParametres;
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
            uc.CleCollectee += AgagneForet;
        }

        private void AgagneForet()
        {
            UCGagneForet uc = new UCGagneForet();
            ZoneJeu.Content = uc;
            uc.butSuivant.Click += ChoixNiveau;
        }
    }
}