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
    /// Customer Support Admin VM
    /// </summary>
    public class CSAViewModel : Bindable
    {
        /// <summary>
        /// messageservice
        /// </summary>
        private IMessageService messageService;

        /// <summary>
        /// dataservice
        /// </summary>
        private IDataService dataService;

        /// <summary>
        /// alkatresz
        /// </summary>
        private List<Alkatresz> alkatreszek;

        /// <summary>
        /// user
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
        /// kimeno
        /// </summary>
        private ObservableCollection<Level> kimeno;

        /// <summary>
        /// megrendelesek
        /// </summary>
        private List<Megrendeles> megrendelesek;

        /// <summary>
        /// keszlet
        /// </summary>
        private List<Alkatresz> keszlet;

        /// <summary>
        /// pck
        /// </summary>
        private ObservableCollection<PC> pck;

        /// <summary>
        /// kivalasztott
        /// </summary>
        private PC kivalasztott;

        /// <summary>
        /// CSAVM konstruktora
        /// </summary>
        /// <param name="csa"> aktuális CSA </param>
        public CSAViewModel(User csa)
        {
            this.CustomerSA = csa;
            this.DataService = new DataService();
            this.MessageService = new MessageService(csa.Nev);
            this.pck = new ObservableCollection<PC>();
            this.bejovo = (MessageService as MessageService).BejovoUzenetek;
            this.kimeno = (MessageService as MessageService).KimenoUzenetek;
            this.megrendelesek = new List<Megrendeles>();
            OsszesRendeles();
            ElvallaltMunka();
            this.alkatreszek = DataService.GetAllAlkatresz();
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
        /// Gets or sets alkatrészek listája
        /// </summary>
        public List<Alkatresz> Alkatreszek
        {
            get
            {
                return this.alkatreszek;
            }

            set
            {
                this.alkatreszek = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets megrendelések listája
        /// </summary>
        public List<Megrendeles> Megrendelesek
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
        /// Gets or sets levél
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
                OnPropertyChanged();
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
                    if (item.Nev != CustomerSA.Nev)
                    {
                        lista.Add(item);
                    }
                }

                return lista;
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
        /// Gets or sets elvállalt megrendelés
        /// </summary>
        public Megrendeles Elvallalt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets elvállalt gépek
        /// </summary>
        public ObservableCollection<PC> Pck
        {
            get
            {
                return pck;
            }

            set
            {
                pck = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets kiválasztott pc elkészültre állítása
        /// </summary>
        public PC Kivalasztott
        {
            get
            {
                return kivalasztott;
            }

            set
            {
                kivalasztott = value;
            }
        }

        /// <summary>
        /// Gets aktuális dolgozó
        /// </summary>
        public User CustomerSA
        {
            get;
        }

        /// <summary>
        /// Gets or sets elvállalt munka
        /// </summary>
        public Megrendeles ElvallaltMegrendeles
        {
            get; set;
        }

        /// <summary>
        /// Elvállalt rendelésekhez újabb pck hozzáadása
        /// </summary>
        private void ElvallaltMunka()
        {
            foreach (var item in megrendelesek)
            {
                if (item.Status == Status.Elvallalva)
                {
                    foreach (var pc in item.Pck)
                    {
                        pck.Add(pc);
                    }
                }
            }
        }

        /// <summary>
        /// Összes szabad rendelés összeszedése, plusz azok, amiket a dolgozó már elvállalt
        /// </summary>
        private void OsszesRendeles()
        {
            foreach (var item in DataService.GetAllMegrendeles())
            {
                if (item.Status == Status.Nincs_elvallalva || (item.Dolgozo == CustomerSA.Nev && item.Status != Status.Kesz))
                {
                    megrendelesek.Add(item);
                    if (item.Dolgozo == CustomerSA.Nev)
                    {
                        ElvallaltMegrendeles = item;
                    }
                }
            }
        }
    }
}
