using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logique d'interaction pour UCForet.xaml
    /// </summary>
    public partial class UCForet : UserControl
    {
        public static string orientationPerso = "Face";
        public UCForet()
        {
            InitializeComponent();
            string nomFichierImage = $"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}.png";
            imgPerso.Source = new BitmapImage(new Uri(nomFichierImage));
        }

        public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && Canvas.GetLeft(imgPerso) <= -25 || e.Key == Key.Right && Canvas.GetLeft(imgPerso) > ActualWidth - imgPerso.Width + 25)
                Console.WriteLine("Le personnage ne peut pas sortir de la fenêtre");
            else
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + MainWindow.pasPerso);
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}DroiteMarche.png"));
                }
                else if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - MainWindow.pasPerso);
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}GaucheMarche.png"));
                }
                else
                {
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Face.png"));
                }
                Console.WriteLine("Position gauche personnage :" + Canvas.GetLeft(imgPerso));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
        }
    }
}

