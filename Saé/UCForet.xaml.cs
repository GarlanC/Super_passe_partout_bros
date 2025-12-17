using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        private TranslateTransform camera;
        public event Action CleCollectee;
        public bool HasKey { get; private set; } = false;
        public bool enPause = false;
        public string orientationPerso = "Face";
        private int nb = 0;
        private int sol = 60;
        private bool enSaut = false;
        private int vitesseSaut = 0;
        private double largeurNiveau = 1600;

        public UCForet()
        {
            InitializeComponent();
            InitializeImagesMarche();
            InitializeTimer();
            MainWindow.InitializeSonFond();
            MainWindow.musiqueFond.Play();

            camera = cameraTransform;

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

        private void VerifierCle()
        {
            double xPerso = Canvas.GetLeft(imgPerso);
            double xCle = Canvas.GetLeft(cleForet);

            if (xPerso + 30 >= xCle)
            {
                cleForet.Visibility = Visibility.Hidden;
                HasKey = true;
                MainWindow.bruitageSonCle.Stop();
                MainWindow.bruitageSonCle.Play();
                CleCollectee.Invoke();
            }
        }

        public void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == UCParametres.Gauche && Canvas.GetLeft(imgPerso) <= -30 || e.Key == UCParametres.Droite && Canvas.GetLeft(imgPerso) > 2*ActualWidth - imgPerso.Width + 20) && !enSaut)
                return;
            
            else if (HasKey == false && !enSaut && !enPause)
            {
                MainWindow.bruitageSonSaut.Stop();
                MainWindow.bruitageSonTerre.Stop();
                if (e.Key == UCParametres.Droite)
                {
                    DeplaceDroite(imgPerso, MainWindow.pasPerso);
                    MainWindow.bruitageSonTerre.Play();
                    orientationPerso = "Droite";
                }
                else if (e.Key == UCParametres.Gauche)
                {
                    DeplaceGauche(imgPerso, MainWindow.pasPerso);
                    MainWindow.bruitageSonTerre.Play();
                    orientationPerso = "Gauche";
                }
                else if (e.Key == UCParametres.Bas)
                {
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Face.png"));
                    MainWindow.bruitageSonTerre.Play();
                }
                else if (e.Key == UCParametres.Haut && (orientationPerso == "Gauche" && Canvas.GetLeft(imgPerso) > -30 + 90 || orientationPerso == "Droite" && Canvas.GetLeft(imgPerso) < 2 * ActualWidth - imgPerso.Width + 20 - 90))
                {
                    enSaut = true;
                    vitesseSaut = 25;
                    MainWindow.bruitageSonSaut.Play();
                    timerSaut.Start();

                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}{orientationPerso}Saut.png"));
                }
                VerifierCle();
            }
        }

        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            if (HasKey == false && !enSaut && !enPause)
            {
                if (e.Key == UCParametres.Gauche)
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Gauche.png"));
                else if (e.Key == UCParametres.Droite)
                    imgPerso.Source = new BitmapImage(new Uri($"pack://application:,,,/images/img{MainWindow.Perso}Droite.png"));
                VerifierCle();
            }
        }

        public void DeplaceGauche(Image image, double pas)
        {
            Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) - MainWindow.pasPerso);
            AnimationDeplacements(imgPersoGauche);
            UpdateCamera();
        }

        public void DeplaceDroite(Image image, double pas)
        {
            Canvas.SetLeft(imgPerso, Canvas.GetLeft(imgPerso) + MainWindow.pasPerso);
            AnimationDeplacements(imgPersoDroite);
            UpdateCamera();
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
            UpdateCamera();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
            Application.Current.MainWindow.KeyUp += canvasJeu_KeyUp;
        }

        private void butPause_Click(object sender, RoutedEventArgs e)
        {
            minuterie.Stop();
            MainWindow.musiqueFond.Pause();
            enPause = true;
            UCPauseJeu ucPause = new UCPauseJeu();
            butPause.Visibility = Visibility.Collapsed;
            canvasJeu.Children.Add(ucPause);
            ucPause.RenderTransform = new TranslateTransform(-camera.X, 0);
            ucPause.butContinuer.Click += (s, args) =>
            {
                canvasJeu.Children.Remove(ucPause);
                butPause.Visibility = Visibility.Visible;
                minuterie.Start();
                MainWindow.musiqueFond.Play();
                enPause = false;
            };
            ucPause.butQuitter.Click += (s, args) =>
            {
                butPause.Visibility = Visibility.Visible;
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                enPause = false;
                mainWindow.AfficheMenu();
            };
            ucPause.butParametres.Click += (s, args) =>
            {
                UCParametres uc = new UCParametres();
                uc.RenderTransform = new TranslateTransform(-camera.X, 0);
                canvasJeu.Children.Add(uc);
                uc.butRetour.Click += (sender, args) =>
                {
                    canvasJeu.Children.Remove(uc);
                    minuterie.Start();
                };
                uc.butValider.Click += (sender, args) =>
                {
                    MainWindow.volumeFond = (int)uc.sliderSon.Value;
                    MainWindow.musiqueFond.Volume = Math.Clamp(MainWindow.volumeFond / 100.0, 0.0, 1.0);
                    canvasJeu.Children.Remove(uc);
                    minuterie.Start();
                };
            };
        }

        private void UpdateCamera()
        {
            double xPerso = Canvas.GetLeft(imgPerso);
            double ecranCentre = ActualWidth / 2;

            double xCamera = ecranCentre - xPerso - imgPerso.Width / 2;

            xCamera = Math.Min(0, xCamera);
            xCamera = Math.Max(-(largeurNiveau - ActualWidth), xCamera);

            camera.X = xCamera;
        }
    }
}

