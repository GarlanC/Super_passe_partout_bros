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
            uc.butNiveaux.Click += AfficherChoixMenu;
            uc.butParametres.Click += AfficherParametres;
        }

        private void AfficherParametres(object sender, RoutedEventArgs e)
        {
            UCParametres uc = new UCParametres();
            ZoneJeu.Content = uc;
            uc.butRetourParametre.Click += RetourParametre_Click;
        }

        private void RetourParametre_Click(object sender, RoutedEventArgs e)
        {
            AfficheMenu();
        }

        private void AfficherChoixMenu(object sender, RoutedEventArgs e)
        {
            UCChoixNiveau uc = new UCChoixNiveau();
            ZoneJeu.Content = uc;
            uc.butRetourNiveaux.Click += RetourNiveaux_Click;
        }

        private void RetourNiveaux_Click(object sender, RoutedEventArgs e)
        {
            AfficheMenu();
        }

        private void AfficherChoixPerso(object sender, RoutedEventArgs e)
        {
            UCChoixPerso uc = new UCChoixPerso();
            ZoneJeu.Content = uc;

        }

        /*public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && Canvas.GetLeft(imgPerso) <= 0 || e.Key == Key.Right && Canvas.GetLeft(imgPerso) > ActualWidth - imgPerso.Width)
                Console.WriteLine("Le père Noel ne peut pas sortir de la fenêtre");
            else
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + MainWindow.PasPerso);
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/img/pereNoel{MainWindow.Perso}Droite.png"));
                }
                if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - MainWindow.PasPerso);
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/img/pereNoel{MainWindow.Perso}Gauche.png"));
                }
                Console.WriteLine("Position Left père Noel :" + Canvas.GetLeft(imgPerso));
            }
        }*/
    }
}