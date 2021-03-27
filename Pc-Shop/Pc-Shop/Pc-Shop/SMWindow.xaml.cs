namespace Pc_Shop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using Model;
    using Services;

    /// <summary>
    /// Interaction logic for SMWindow.xaml
    /// </summary>
    public partial class SMWindow : Window
    {
        /// <summary>
        /// smvm
        /// </summary>
        private SMViewModel smViewModel;

        /// <summary>
        /// SMWindow konstruktora
        /// </summary>
        /// <param name="felh"> aktuális SM </param>
        public SMWindow(User felh)
        {
            InitializeComponent();
            smViewModel = new SMViewModel(felh);
            this.DataContext = smViewModel;
        }

        /// <summary>
        /// Készlethez hozzáadja a beérkezett alkatrészt
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Raktarhoz_Ad(object sender, RoutedEventArgs e)
        {
            smViewModel.AlkatreszrendelesKuldese();
            smViewModel.AlkatreszRendelesek = new ObservableCollection<Rendeles>();
        }

        /// <summary>
        /// Adott alkatrész megrendelése
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e">Nincs használva </param>
        private void Megrendel(object sender, RoutedEventArgs e)
        {
            if (smViewModel.AlkatreszRendelesek.Count == 0)
            {
                foreach (var item in smViewModel.Rendelendo)
                {
                    smViewModel.Rendeles = new Rendeles(item, item.Mennyiseg);
                    smViewModel.AlkatreszRendelesek.Add(smViewModel.Rendeles);
                }

                smViewModel.Rendelendo = new ObservableCollection<Alkatresz>();
            }
            else
            {
                MessageBox.Show("Légy türelemmel míg az előző rendelésed megérkezik!");
            }
        }

        /// <summary>
        /// Rendelendő alkatrészeket tartalmazó listához ad hozzá újabb eleme(ke)t
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> e </param>
        private void Hozzaad(object sender, RoutedEventArgs e)
        {
            if (cb0.SelectedItem == null)
            {
                MessageBox.Show("Nincs alkatrész kiválasztva!");
                return;
            }

            if (smViewModel.Darab > 0)
            {
                smViewModel.Kivalasztott.Mennyiseg = smViewModel.Darab;
                smViewModel.Rendelendo.Add(smViewModel.Kivalasztott);
            }
            else
            {
                MessageBox.Show("Legalább egyet kell rendelned!");
            }
        }

        /// <summary>
        /// ÜZenet küldése
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Uzenet_Kuldese(object sender, RoutedEventArgs e)
        {
            if (cb1.SelectedItem == null)
            {
                MessageBox.Show("Nincs címzett kiválasztva!");
                return;
            }

            if (tb0.Text.ToString() != string.Empty)
            {
                smViewModel.Level = new Level(smViewModel.Felhasznalo.Nev, smViewModel.Cimzett.Nev, tb0.Text.ToString(), DateTime.Now);
                smViewModel.Kimeno.Add(smViewModel.Level);
                (smViewModel.MessageService as MessageService).SendLevel(smViewModel.Level);
                MessageBox.Show("Sikereses elküldve");
            }
            else
            {
                MessageBox.Show("Hiányos adat");
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy helyes-e az input
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Helyes_Input(object sender, TextCompositionEventArgs e)
        {
            Regex regular = new Regex("[^0-9]+");
            e.Handled = regular.IsMatch(e.Text);
        }
    }
}
