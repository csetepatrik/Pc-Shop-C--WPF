namespace Pc_Shop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using Services;

    /// <summary>
    /// System Manager VM
    /// </summary>
    public class SMViewModel : Bindable
    {
        /// <summary>
        /// dataser
        /// </summary>
        private IDataService dataService;

        /// <summary>
        /// messageserv
        /// </summary>
        private IMessageService messageService;

        /// <summary>
        /// keszlet
        /// </summary>
        private List<Alkatresz> keszlet;

        /// <summary>
        /// rendeles
        /// </summary>
        private Rendeles rendeles;

        /// <summary>
        /// felhasznalo
        /// </summary>
        private User felhasznalo;

        /// <summary>
        /// level
        /// </summary>
        private Level level;

        /// <summary>
        /// cimzett
        /// </summary>
        private User cimzett;

        /// <summary>
        /// bejovo
        /// </summary>
        private List<Level> bejovo;

        /// <summary>
        /// kimenok
        /// </summary>
        private ObservableCollection<Level> kimeno;

        /// <summary>
        /// alkatreszrendelesek
        /// </summary>
        private ObservableCollection<Rendeles> alkatreszRendelesek;

        /// <summary>
        /// megrendelesek
        /// </summary>
        private ObservableCollection<Megrendeles> megrendelesek;

        /// <summary>
        /// rendelendo
        /// </summary>
        private ObservableCollection<Alkatresz> rendelendo;

        /// <summary>
        /// darab
        /// </summary>
        private int darab;

        /// <summary>
        /// kivalasztott
        /// </summary>
        private Alkatresz kivalasztott;

        /// <summary>
        /// SMVM konstruktora
        /// </summary>
        /// <param name="felh"> aktuális SM </param>
        public SMViewModel(User felh)
        {
            this.felhasznalo = felh;
            MessageService = new MessageService(felhasznalo.Nev);
            DataService = new DataService();
            rendelendo = new ObservableCollection<Alkatresz>();
            keszlet = DataService.GetAllAlkatresz();
            this.bejovo = (MessageService as MessageService).BejovoUzenetek;
            kimeno = (MessageService as MessageService).KimenoUzenetek;
            megrendelesek = DataService.GetAllMegrendeles();
            this.alkatreszRendelesek = DataService.GetAllAlkatreszRendeles();
            Rendelheto = (DataService as DataService).RendelhetoAlkatreszek();
        }

        /// <summary>
        /// Gets or sets dataservice
        /// </summary>
        public IDataService DataService
        {
            get
            {
                return dataService;
            }

            set
            {
                dataService = value;
            }
        }

        /// <summary>
        /// Gets or sets messageservice
        /// </summary>
        public IMessageService MessageService
        {
            get
            {
                return messageService;
            }

            set
            {
                messageService = value;
            }
        }

        /// <summary>
        /// Gets or sets aktuális dolgozó
        /// </summary>
        public User Felhasznalo
        {
            get
            {
                return felhasznalo;
            }

            set
            {
                felhasznalo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets megrendelhető alkatrészek
        /// </summary>
        public List<Alkatresz> Rendelheto
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets küldendő levél
        /// </summary>
        public Level Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        /// <summary>
        /// Gets or sets levél címzettje
        /// </summary>
        public User Cimzett
        {
            get
            {
                return cimzett;
            }

            set
            {
                cimzett = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets bejövő levelek listája
        /// </summary>
        public List<Level> Bejovo
        {
            get
            {
                return this.bejovo;
            }

            set
            {
                this.bejovo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets kimenő levelek listája
        /// </summary>
        public ObservableCollection<Level> Kimeno
        {
            get
            {
                return kimeno;
            }

            set
            {
                kimeno = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets címezhető munkatársak listája
        /// </summary>
        public List<User> Munkatarsak
        {
            get
            {
                List<User> lista = new List<User>();
                foreach (var item in (DataService as DataService).Felhasznalok)
                {
                    if (item.Nev != felhasznalo.Nev)
                    {
                        lista.Add(item);
                    }
                }

                return lista;
            }
        }

        /// <summary>
        /// Gets or sets adott rendelés
        /// </summary>
        public Rendeles Rendeles
        {
            get
            {
                return rendeles;
            }

            set
            {
                rendeles = value;
            }
        }

        /// <summary>
        /// Gets or sets elérhető alkatrészek listája
        /// </summary>
        public List<Alkatresz> Keszlet
        {
            get
            {
                return keszlet;
            }

            set
            {
                keszlet = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets alkatrészrendelések beszállítónak
        /// </summary>
        public ObservableCollection<Rendeles> AlkatreszRendelesek
        {
            get
            {
                return this.alkatreszRendelesek;
            }

            set
            {
                this.alkatreszRendelesek = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets aktuális megrendelések
        /// </summary>
        public ObservableCollection<Megrendeles> Megrendelesek
        {
            get
            {
                return megrendelesek;
            }

            set
            {
                megrendelesek = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets kiválasztott alkatrész
        /// </summary>
        public Alkatresz Kivalasztott
        {
            get
            {
                return kivalasztott;
            }

            set
            {
                kivalasztott = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets megrendelendő alkatrészek
        /// </summary>
        public ObservableCollection<Alkatresz> Rendelendo
        {
            get
            {
                return rendelendo;
            }

            set
            {
                rendelendo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets alkatrészek mennyisége
        /// </summary>
        public int Darab
        {
            get
            {
                return darab;
            }

            set
            {
                darab = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Az adott alkatrészt hozzáadjuk a raktárhoz
        /// </summary>
        public void AlkatreszrendelesKuldese()
        {
            foreach (var item in this.AlkatreszRendelesek)
            {
                foreach (var keszletItem in Keszlet)
                {
                    if (keszletItem.Nev == item.Alkatresz.Nev)
                    {
                        keszletItem.Mennyiseg += item.Mennyiseg;
                    }
                }
            }

            (DataService as DataService).SendAlkatreszRendeles(this.alkatreszRendelesek);
        }
    }
}
