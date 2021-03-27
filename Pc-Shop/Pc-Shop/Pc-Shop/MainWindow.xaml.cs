namespace Pc_Shop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using Model;
    using Services;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// bvm
        /// </summary>
        private BejelentkezesViewModel bVM;

        /// <summary>
        /// Bejelenkezés konstruktora, adatbázis útvonalának megadása
        /// </summary>
        public MainWindow()
        {
            var databaseLocation = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).GetDirectories("PcShopDataBase").First();
            AppDomain.CurrentDomain.SetData("DataDirectory", databaseLocation.FullName);
            InitializeComponent();
            this.bVM = new BejelentkezesViewModel();
            this.DataContext = bVM;
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Bejelentkezes(object sender, RoutedEventArgs e)
        {
            try
            {
                this.bVM.Jelszo = jelszo.Password;
                this.bVM.Felhasznalo = bVM.Bejelentkezes();
                if (this.bVM.Felhasznalo.Jogosultsag == Jogosultsag.CEO)
                {
                    CEOWindow ceow = new CEOWindow(this.bVM.Felhasznalo);
                    ceow.Show();
                    Elrejtes();
                }
                else if (this.bVM.Felhasznalo.Jogosultsag == Jogosultsag.SM)
                {
                    SMWindow smw = new SMWindow(this.bVM.Felhasznalo);
                    smw.Show();
                    Elrejtes();
                }
                else
                {
                    CSAWindow csaw = new CSAWindow(bVM.Felhasznalo);
                    csaw.Show();
                    Elrejtes();
                }
            }
            catch (InvalidLoginException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Bejelentkezési felület eltüntetése 
        /// </summary>
        private void Elrejtes()
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
