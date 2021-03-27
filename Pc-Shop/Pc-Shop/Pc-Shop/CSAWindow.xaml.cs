namespace Pc_Shop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using System.Windows.Shapes;
    using Model;
    using Services;

    /// <summary>
    /// Interaction logic for CSAWindow.xaml
    /// </summary>
    public partial class CSAWindow : Window
    {
        /// <summary>
        /// csavm
        /// </summary>
        private CSAViewModel csaViewModel;

        /// <summary>
        /// CSAW konstruktora
        /// </summary>
        /// <param name="felh"> az aktuális CSA </param>
        public CSAWindow(User felh)
        {
            InitializeComponent();
            csaViewModel = new CSAViewModel(felh);
            this.DataContext = csaViewModel;
        }

        /// <summary>
        /// Egy megrendelés elvállalása
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva</param>
        private void Elvallal(object sender, RoutedEventArgs e)
        {
            if (lb0.SelectedItem == null)
            {
                MessageBox.Show("Nincs kiválasztva megrendelés!");
                return;
            }

            if (csaViewModel.Pck.Count == 0)
            {
                Megrendeles megrendeles = (Megrendeles)lb0.SelectedItem;
                List<Alkatresz> szukseges = (csaViewModel.DataService as DataService).KelloAlkMeghatarozas(megrendeles.Pck);
                try
                {
                    if ((csaViewModel.DataService as DataService).KelloAlkatreszOsszehasonlitas(szukseges, csaViewModel.Alkatreszek))
                    { 
                    foreach (var item in megrendeles.Pck)
                        {
                            csaViewModel.Pck.Add(item);
                        }

                        csaViewModel.Elvallalt = megrendeles;
                        megrendeles.Status = Status.Elvallalva;
                        megrendeles.Dolgozo = csaViewModel.CustomerSA.Nev;
                        (csaViewModel.DataService as DataService).Rakup(szukseges, csaViewModel.Alkatreszek);
                        (csaViewModel.DataService as DataService).UpdateStatus(megrendeles);
                    }
                }
                catch (NincsElegAlkatreszException exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                MessageBox.Show("Előbb el kell végezned a jelenlegi megbízásodat");
            }
        }

        /// <summary>
        /// Egy rendelés kiszolgálásának megvalósítása
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Keszen_Van(object sender, RoutedEventArgs e)
        {
            if (csaViewModel.Pck.Count == 0 || csaViewModel.Elvallalt == null)
            {
                MessageBox.Show("Nincs elvállalt PC!");
                return;
            }

            if (lb1.SelectedItem == null)
            {
                MessageBox.Show("Nincs elvállalt PC kiválasztva!");
                return;
            }

            if (!csaViewModel.Kivalasztott.Keszvan)
            {
                csaViewModel.Kivalasztott.Keszvan = true;
                (csaViewModel.DataService as DataService).PCUpdate(csaViewModel.Kivalasztott);
                if ((csaViewModel.DataService as DataService).OsszPCElkeszult(csaViewModel.Pck))
                {
                    csaViewModel.Pck = new ObservableCollection<PC>();
                    csaViewModel.Elvallalt.Status = Status.Kesz;
                    (csaViewModel.DataService as DataService).UpdateStatus(csaViewModel.Elvallalt);
                    MessageBox.Show("Szép munka, az adott rendeléshez az összes gép a rendelkezésre áll!");
                }
            }
            else
            {
                MessageBox.Show("A kiválasztott számítógép már elkészült!");
            }
        }

        /// <summary>
        /// Üzenet elküldése
        /// </summary>
        /// <param name="sender"> Nincs használva </param>
        /// <param name="e"> Nincs használva </param>
        private void Uzenet_Kuldese(object sender, RoutedEventArgs e)
        {
            if (cb0.SelectedItem == null)
            {
                MessageBox.Show("Nincs címzett kiválasztva!");
                return;
            }

            if (tb0.Text.ToString() != string.Empty)
            {
                csaViewModel.Level = new Level(csaViewModel.CustomerSA.Nev, csaViewModel.Cimzett.Nev, tb0.Text.ToString(), DateTime.Now);
                csaViewModel.Kimeno.Add(csaViewModel.Level);
                (csaViewModel.MessageService as MessageService).SendLevel(csaViewModel.Level);
                MessageBox.Show("Levél sikeresen elküldve");
            }
            else
            {
                MessageBox.Show("Hiányzó adat");
            }
        }
    }
}
