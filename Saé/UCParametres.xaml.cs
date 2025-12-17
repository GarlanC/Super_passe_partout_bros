using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour UCParametres.xaml
    /// </summary>
    public partial class UCParametres : UserControl
    {
        public Button waitingButton;

        private Key oldHaut = Haut;
        private Key oldBas = Bas;
        private Key oldGauche = Gauche;
        private Key oldDroite = Droite;

        public static Key Haut = Key.Up;
        public static Key Bas = Key.Down;
        public static Key Gauche = Key.Left;
        public static Key Droite = Key.Right;
        public UCParametres()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sliderSon.Value = MainWindow.volumeFond;
            txtBas.Text = $"Bas : {Bas}";
            txtHaut.Text = $"Haut : {Haut}";
            txtGauche.Text = $"Gauche : {Gauche}";
            txtDroite.Text = $"Droite : {Droite}";
        }

        private void ChangeKey_Click(object sender, RoutedEventArgs e)
        {
            waitingButton = sender as Button;
            if (waitingButton == butHaut)
                txtHaut.Text = "Appuie sur une touche...";
            else if (waitingButton == butBas)
                txtBas.Text = "Appuie sur une touche...";
            else if (waitingButton == butGauche)
                txtGauche.Text = "Appuie sur une touche...";
            else if (waitingButton == butDroite)
                txtDroite.Text = "Appuie sur une touche...";

            this.Focus();
            Keyboard.Focus(this);
            this.PreviewKeyDown += UCParametres_KeyDown;
        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            txtHaut.Text = $"Haut : {oldHaut}";
            txtBas.Text = $"Bas : {oldBas}";
            txtGauche.Text = $"Gauche : {oldGauche}";
            txtDroite.Text = $"Droite : {oldDroite}";

            waitingButton = null;
            this.PreviewKeyDown -= UCParametres_KeyDown;
        }

        public void UCParametres_KeyDown(object sender, KeyEventArgs e)
        {
            if (waitingButton == null)
                return;

            if (waitingButton == butHaut)
            {
                Haut = e.Key;
                txtHaut.Text = $"Haut : {Haut}";
            }
            else if (waitingButton == butBas)
            {
                Bas = e.Key;
                txtBas.Text = $"Bas : {Bas}";
            }
            else if (waitingButton == butGauche)
            {
                Gauche = e.Key;
                txtGauche.Text = $"Gauche : {Gauche}";
            }
            else if (waitingButton == butDroite)
            {
                Droite = e.Key;
                txtDroite.Text = $"Droite : {Droite}";
            }

            waitingButton = null;
            this.PreviewKeyDown -= UCParametres_KeyDown;
        }
    }
}
