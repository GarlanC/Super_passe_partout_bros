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
    /// Logique d'interaction pour UCPorte.xaml
    /// </summary>
    public partial class UCPorte : UserControl
    
    {
        public event Action OuvrirPorte;
        public UCPorte()
        {
            InitializeComponent();
        }

        private void UCPorte_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.cadenasForetDebloque)
                butCadenasForet.Visibility = Visibility.Collapsed;
            if (MainWindow.cadenasDesertDebloque)
                butCadenasDesert.Visibility = Visibility.Collapsed;
            if (MainWindow.cadenasPlageDebloque)
                butCadenasPlage.Visibility = Visibility.Collapsed;
        }
        private void butCadenasForet_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nbCleForet > 0)
            {
                MainWindow.cadenasForet += 1;
                MainWindow.nbCleForet --;
                butCadenasForet.Visibility = Visibility.Collapsed;
                MainWindow.cadenasForetDebloque = true;
                txtCptCleForet.Text = $"Clé foret : {MainWindow.nbCleForet}";
            }
        }

        private void butCadenasDesert_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nbCleDesert > 0)
            {
                MainWindow.cadenasDesert += 1;
                MainWindow.nbCleDesert -= 1;
                butCadenasDesert.Visibility = Visibility.Collapsed;
                MainWindow.cadenasDesertDebloque = true;
                txtCptCleDesert.Text = $"Clé desert : {MainWindow.nbCleDesert}";
            }
        }

        private void butCadenasPlage_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.nbClePlage > 0)
            {
                MainWindow.cadenasPlage += 1;
                MainWindow.nbClePlage -= 1;
                butCadenasPlage.Visibility = Visibility.Collapsed;
                MainWindow.cadenasPlageDebloque = true;
                txtCptClePlage.Text = $"Clé plage : {MainWindow.nbClePlage}";
            }

        }

        private void butPorte_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.cadenasDesert > 0 && MainWindow.cadenasForet > 0 && MainWindow.cadenasPlage > 0)
                OuvrirPorte.Invoke();
        }
    }
}
