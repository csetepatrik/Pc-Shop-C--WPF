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
    /// Interaction logic for CEOWindow.xaml
    /// </summary>
    public partial class CEOWindow : Window
    {
        /// <summary>
        /// ceovm
        /// </summary>
        private CEOViewModel ceoViewModel;

        /// <summary>
        /// CEOWindow konstruktora
        /// </summary>
        /// <param name="felh"> az aktualis felhasznalo(ceo) </param>
        public CEOWindow(User felh)
        {
            InitializeComponent();
            ceoViewModel = new CEOViewModel(felh);
            this.DataContext = ceoViewModel;
        }

        /// <summary>
        /// Pc hozzáadása rendelésekhez
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> e </param>
        private void Pc_Hozzaad(object sender, RoutedEventArgs e)
        {
            if (kivalasztott.SelectedItem == null)
            {
                MessageBox.Show("Nincs PC típus kiválasztva!");
                return;
            }

            if (darab.Text.ToString() != string.Empty && int.Parse(darab.Text.ToString()) > 0)
            {
                ceoViewModel.PcRendeleshezAd(ceoViewModel.RendelendoTipus, int.Parse(darab.Text.ToString()));
                MessageBox.Show("Hozzáadva!");
            }
            else
            {
                MessageBox.Show("Nem megfelelő input");
            }
        }

        /// <summary>
        /// Rendelés felvétele
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> e </param>
        private void Rendeles_Felvetele(object sender, RoutedEventArgs e)
        {
            ceoViewModel.UjRendeles = new Megrendeles(ceoViewModel.RendeltPCk.ToList(), ++ceoViewModel.RendelesID, megrendelo.Text.ToString(), string.Empty, Status.Nincs_elvallalva);
            ceoViewModel.Megrendelesek.Add(ceoViewModel.UjRendeles);
            (ceoViewModel.DataService as DataService).RendelesFelvetele(ceoViewModel.UjRendeles);
            ceoViewModel.RendeltPCk = new ObservableCollection<PC>();
            MessageBox.Show("Sikeres felvétel");
        }

        /// <summary>
        /// Levél megküldése
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Uzenet_Kuldese(object sender, RoutedEventArgs e)
        {
            if (comboB.SelectedItem == null)
            {
                MessageBox.Show("Nincs címzett kiválasztva!");

                return;
            }

            if (tb0.Text.ToString() != string.Empty)
            {
                ceoViewModel.Level = new Level(ceoViewModel.Felhasznalo.Nev, ceoViewModel.Cimzett.Nev, tb0.Text.ToString(), DateTime.Now);
                ceoViewModel.Kimeno.Add(ceoViewModel.Level);
                (ceoViewModel.MessageService as MessageService).SendLevel(ceoViewModel.Level);
                MessageBox.Show("Levél elküldve");
            }
            else
            {
                MessageBox.Show("Hiányos adat");
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy számot írtunk-e be
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
