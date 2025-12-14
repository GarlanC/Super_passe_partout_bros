using Microsoft.Win32;
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
using System.Windows.Threading;
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
        private DispatcherTimer timerSaut;
        private DispatcherTimer minuterie;

        public string orientationPerso = "Face";
        private int nb = 0;
        private int sol = 60;
        private bool enSaut = false;
        private int vitesseSaut = 0;

        public UCForet()
        {
            InitializeComponent();
            InitializeImagesMarche();
            InitializeTimer();

            timerSaut = new DispatcherTimer();
            timerSaut.Interval = TimeSpan.FromMilliseconds(16);
            timerSaut.Tick += AnimationSaut;

            if (MainWindow.Perso == "Homme")
                sol = 63;
            Canvas.SetBottom(imgPerso, sol);

            imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}.png"));
        }

        private void InitializeImagesMarche()
        {
            for (int i = 0; i < imgPersoDroite.Length; i++)
            {
                imgPersoDroite[i] = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}DroiteMarche{i + 1}.png"));
                imgPersoGauche[i] = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}GaucheMarche{i + 1}.png"));
            }
        }

        private void InitializeTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(16);
            minuterie.Start();
        }

        public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left && Canvas.GetLeft(imgPerso) <= -30 || e.Key == Key.Right && Canvas.GetLeft(imgPerso) > ActualWidth - imgPerso.Width + 30) && !enSaut)
                return;
            else
            {
                if (e.Key == Key.Right && !enSaut)
                {
                    DeplaceDroite(imgPerso, MainWindow.pasPerso);
                    orientationPerso = "Droite";
                }
                else if (e.Key == Key.Left && !enSaut)
                {
                    DeplaceGauche(imgPerso, MainWindow.pasPerso);
                    orientationPerso = "Gauche";
                }
                else if (e.Key == Key.Down && !enSaut)
                {
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Face.png"));
                }
                else if (e.Key == Key.Up && !enSaut && (orientationPerso == "Gauche" && Canvas.GetLeft(imgPerso) > -30 + 90 || orientationPerso == "Droite" && Canvas.GetLeft(imgPerso) < ActualWidth - imgPerso.Width + 30 - 90))
                {
                    enSaut = true;
                    vitesseSaut = 30;
                    timerSaut.Start();

                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}Saut.png"));
                }
            }
        }

        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && !enSaut)
                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Gauche.png"));
            else if (e.Key == Key.Right && !enSaut)
                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Droite.png"));
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

        private void AnimationSaut(object sender, EventArgs e)
        {
            Canvas.SetBottom(imgPerso, Canvas.GetBottom(imgPerso) + vitesseSaut);
            if (orientationPerso == "Gauche")
                Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - 3);
            else if (orientationPerso == "Droite")
                Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + 3);

            vitesseSaut -= 2;

            if (Canvas.GetBottom(imgPerso) <= sol)
            {
                Canvas.SetBottom(imgPerso, sol);
                timerSaut.Stop();
                enSaut = false;

                imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}.png"));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
            Application.Current.MainWindow.KeyUp += canvasJeu_KeyUp;
        }

        private void butPause_Click(object sender, RoutedEventArgs e)
        {
            minuterie.Stop();
            bool parametreOuvert = true;
            while (parametreOuvert)
            {
                UCParametres uc = new UCParametres();
                canvasJeu.Children.Add(uc);
                uc.butRetour.Click += (s, args) =>
                {
                    canvasJeu.Children.Remove(uc);
                    minuterie.Start();
                    parametreOuvert = false;
                };
                uc.butValider.Click += (s, args) =>
                {
                    canvasJeu.Children.Remove(uc);
                    minuterie.Start();
                    parametreOuvert = false;
                };
                break;
            }
        }
    }
}

