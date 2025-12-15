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
    /// Logique d'interaction pour UCMenu.xaml
    /// </summary>
    public partial class UCMenu : UserControl
    {
        public UCMenu()
        {
            InitializeComponent();
        }

        private void butParametres_Click(object sender, RoutedEventArgs e)
        {
            var mw = (MainWindow)Application.Current.MainWindow;
            UCParametres uc = new UCParametres();
            uc.sliderSon.Value = MainWindow.volumeFond;

            uc.butRetour.Click += (s, a) =>
            {
                mw.AfficheMenu();
            };

            uc.butValider.Click += (s, a) =>
            {
                MainWindow.volumeFond = (int)uc.sliderSon.Value;
                mw.AfficheMenu();
            };
            mw.ZoneJeu.Content = uc;
        }
    }
}
