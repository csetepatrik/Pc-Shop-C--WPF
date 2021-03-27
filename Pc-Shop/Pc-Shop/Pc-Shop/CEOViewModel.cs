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
    /// CEO ViewModel
    /// </summary>
    public class CEOViewModel : Bindable
    {
        /// <summary>
        /// messageService
        /// </summary>
        private IMessageService messageService;

        /// <summary>
        /// dataservice
        /// </summary>
        private IDataService dataService;

        /// <summary>
        /// megrendelesek
        /// </summary>
        private ObservableCollection<Megrendeles> megrendelesek;

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
        /// ujrendeles
        /// </summary>
        private Megrendeles ujRendeles;

        /// <summary>
        /// rendeltpck
        /// </summary>
        private ObservableCollection<PC> rendeltPCk;

        /// <summary>
        /// rendelendotipus
        /// </summary>
        private PcTipus rendelendoTipus;

        /// <summary>
        /// keszlet
        /// </summary>
        private List<Alkatresz> keszlet;

        /// <summary>
        /// CEOVM konstruktora
        /// </summary>
        /// <param name="felh"> az aktuális munkatárs</param>
        public CEOViewModel(User felh)
        {
            felhasznalo = felh;
            DataService = new DataService();
            MessageService = new MessageService(felhasznalo.Nev);
            this.bejovo = (MessageService as MessageService).BejovoUzenetek;
            kimeno = (MessageService as MessageService).KimenoUzenetek;
            rendeltPCk = new ObservableCollection<PC>();
            keszlet = DataService.GetAllAlkatresz();
            megrendelesek = DataService.GetAllMegrendeles();
            RendelesID = (DataService as DataService).RendelesMAXID;
            Tipusok = (DataService as DataService).PcTipusok();
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
        /// Gets or sets rendelendo tipus
        /// </summary>
        public PcTipus RendelendoTipus
        {
            get
            {
                return rendelendoTipus;
            }

            set
            {
                rendelendoTipus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets rendelés azonosító
        /// </summary>
        public int RendelesID
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets tipusok
        /// </summary>
        public List<PcTipus> Tipusok
        {
            get; set;
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
        /// Gets or sets új megrendelés
        /// </summary>
        public Megrendeles UjRendeles
        {
            get
            {
                return ujRendeles;
            }

            set
            {
                ujRendeles = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets megrendelések
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
        /// Gets or sets megrendelt pck
        /// </summary>
        public ObservableCollection<PC> RendeltPCk
        {
            get
            {
                return rendeltPCk;
            }

            set
            {
                rendeltPCk = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets felhasználó
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
        /// Gets or sets cimzett
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
        /// Gets or sets bejovo levelek listája
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
            }
        }

        /// <summary>
        /// Gets elérhető munkatársak
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
        /// Adott típus megrendelt darabszámát hozzáadja rendelésekhez
        /// </summary>
        /// <param name="tipus"> pc típusa </param>
        /// <param name="darab"> mennyiseg </param>
        public void PcRendeleshezAd(PcTipus tipus, int darab)
        {
            List<PC> lista = (DataService as DataService).PCAlkatreszekkel();
            PC pc = lista.Where(x => x.Tipus == tipus).First();
            for (int i = 0; i < darab; i++)
            {
                PCHozzaadas(pc);
            }
        }

        /// <summary>
        /// Az adott megrendeléshez hány darab pc van kész
        /// </summary>
        /// <param name="megrendeles"> az adott megrendelés </param>
        /// <returns> darabszám </returns>
        public int PCKesz(Megrendeles megrendeles)
        {
            return (DataService as DataService).Megrendeleshez_Mennyi_PC_Done(megrendeles);
        }

        /// <summary>
        /// Számítógép hozzáadása a megrendelt pck-hez
        /// </summary>
        /// <param name="pc">kiválasztott pc</param>
        private void PCHozzaadas(PC pc)
        {
            rendeltPCk.Add(pc);
        }
    }
}
