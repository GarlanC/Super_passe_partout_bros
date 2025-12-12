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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Saé
{
    /// <summary>
    /// Logique d'interaction pour UCForet.xaml
    /// </summary>
    public partial class UCForet : UserControl
    {
        private BitmapImage[] persos = new BitmapImage[5];
        public static string orientationPerso = "Face";
        private int nb = 0;
        public UCForet()
        {
            InitializeComponent();
            InitializeImagesDroiteMarche();
            if (MainWindow.Perso == "Homme")
                Canvas.SetBottom(imgPerso, 63);
            else
                Canvas.SetBottom(imgPerso, 60);
            string nomFichierImage = $"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}.png";
            imgPerso.Source = new BitmapImage(new Uri(nomFichierImage));
        }

        private void InitializeImagesDroiteMarche()
        {
            for (int i = 0; i < persos.Length; i++)
                persos[i] = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}DroiteMarche{i + 1}.png"));
        }

        public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && Canvas.GetLeft(imgPerso) <= -30 || e.Key == Key.Right && Canvas.GetLeft(imgPerso) > ActualWidth - imgPerso.Width + 10)
                Console.WriteLine("Le personnage ne peut pas sortir de la fenêtre");
            else
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + MainWindow.pasPerso);
                    nb++;
                    if (nb / 3 == persos.Length)
                        nb = 0;
                    if (nb % 3 == 0)
                        imgPerso.Source = persos[nb / 3];
                }
                else if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - MainWindow.pasPerso);
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Gauche.png"));
                }
                Console.WriteLine("Position gauche personnage :" + Canvas.GetLeft(imgPerso));
            }
        }

        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Face.png"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
            Application.Current.MainWindow.KeyUp += canvasJeu_KeyUp;
        }
    }
}

