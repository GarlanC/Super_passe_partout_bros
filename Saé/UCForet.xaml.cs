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
        private BitmapImage[] imgPersoDroite = new BitmapImage[3];
        private BitmapImage[] imgPersoGauche = new BitmapImage[3];
        public static string derniereTouche = "";
        public static string orientationPerso = "Face";
        private int nb = 0;
        public UCForet()
        {
            InitializeComponent();
            InitializeImagesMarche();
            if (MainWindow.Perso == "Homme")
                Canvas.SetBottom(imgPerso, 63);
            else
                Canvas.SetBottom(imgPerso, 60);
            string nomFichierImage = $"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}.png";
            imgPerso.Source = new BitmapImage(new Uri(nomFichierImage));
        }

        private void InitializeImagesMarche()
        {
            for (int i = 0; i < imgPersoDroite.Length; i++)
            {
                imgPersoDroite[i] = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}DroiteMarche{i + 1}.png"));
                imgPersoGauche[i] = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}GaucheMarche{i + 1}.png"));
            }
        }

        public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && Canvas.GetLeft(imgPerso) <= -30 || e.Key == Key.Right && Canvas.GetLeft(imgPerso) > ActualWidth - imgPerso.Width + 10)
                return;
            else if (e.Key == Key.Up && derniereTouche == "Right" && Canvas.GetLeft(imgPerso) < ActualWidth - imgPerso.Width + 10 - 91 || e.Key == Key.Up && derniereTouche == "Left" && Canvas.GetLeft(imgPerso) > -30 + 91)
            {
                if (derniereTouche == "Right")
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}DroiteSaut.png"));
                else if (derniereTouche == "Left")
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}GaucheSaut.png"));
                Canvas.SetBottom(imgPerso, Canvas.GetBottom(imgPerso) + 100);
            }
            else
            {
                if (e.Key == Key.Right)
                {
                    DeplaceDroite(imgPerso, MainWindow.pasPerso);
                    derniereTouche = "Right";
                }
                else if (e.Key == Key.Left)
                {
                    DeplaceGauche(imgPerso, MainWindow.pasPerso);
                    derniereTouche = "Left";
                }
            }
        }

        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Gauche.png"));
            else if (e.Key == Key.Right)
                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Droite.png"));
            else if (e.Key == Key.Down)
                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Face.png"));
            else if (e.Key == Key.Up && derniereTouche == "Right" && Canvas.GetLeft(imgPerso) < ActualWidth - imgPerso.Width + 10 - 91 || e.Key == Key.Up && derniereTouche == "Left" && Canvas.GetLeft(imgPerso) > -30 + 91)
            {
                for (int i = 1; i < 14; i++)
                {
                    if (derniereTouche == "Right")
                    {
                        Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + i);
                        imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Droite.png"));
                    }
                    else if (derniereTouche == "Left")
                    {
                        Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - i);
                        imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Gauche.png"));
                    }
                    Canvas.SetBottom(imgPerso, Canvas.GetBottom(imgPerso) - i);
                }
                Canvas.SetBottom(imgPerso, Canvas.GetBottom(imgPerso) - 9);
            }
        }

        public void DeplaceGauche(Image image, double pas)
        {
            Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - MainWindow.pasPerso);
            AnimationDeplacements(imgPersoGauche);
        }

        public void DeplaceDroite(Image image, double pas)
        {
            Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + MainWindow.pasPerso);
            AnimationDeplacements(imgPersoDroite);
        }

        public void AnimationDeplacements(BitmapImage[] tabImg)
        {
            nb++;
            if (nb / 3 == tabImg.Length)
                nb = 0;
            if (nb % 3 == 0)
                imgPerso.Source = tabImg[nb / 3];
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
            Application.Current.MainWindow.KeyUp += canvasJeu_KeyUp;
        }
    }
}

