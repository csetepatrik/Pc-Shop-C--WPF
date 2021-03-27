namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using PcShopDataBase;

    /// <summary>
    /// DataAccessLayer class
    /// </summary>
    public class DataAccessLayer
    {
        /// <summary>
        /// Function adatok
        /// </summary>
        private Functions adatok;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DataAccessLayer()
        {
            this.adatok = new Functions();
            RendelesMaxID = this.adatok.MaxId;
        }

        /// <summary>
        /// Gets or sets Rendelés MAX ID
        /// </summary>        
        public int RendelesMaxID
        {
            get; set;
        }

        /// <summary>
        /// Gets Alkatrészek listája
        /// </summary>        
        public List<Alkatresz> GetAlkatreszek
        {
            get { return this.AlkatreszekList(); }
        }

        /// <summary>
        /// Gets Feladatok listája
        /// </summary>
        public List<CSAFeladat> GetAllFeladat
        {
            get { return this.Feladatok(); }
        }

        /// <summary>
        /// Gets Megrendelések listája
        /// </summary>
        public ObservableCollection<Megrendeles> GetAllMegrendeles
        {
            get { return this.Megrendelesek(); }
        }

        /// <summary>
        /// Gets Levelek listája
        /// </summary>
        public List<Level> GetAllUzenetek
        {
            get { return Uzenetek(); }
        }

        /// <summary>
        /// Userek Listája
        /// </summary>
        /// <returns>List<User> felhasznalok</returns>
        public List<User> Felhasznalok()
        {
            List<User> felhasznalok = new List<User>();

            foreach (var item in this.adatok.Users())
            {
                Jogosultsag jog;
                if (item.BEOSZTAS == "SM")
                {
                    jog = Jogosultsag.SM;
                }
                else if (item.BEOSZTAS == "CSA")
                {
                 jog = Jogosultsag.CSA;
                }
                else if (item.BEOSZTAS == "CEO")
                {
                    jog = Jogosultsag.CEO;
                }
                else
                {
                    jog = Jogosultsag.CEO;
                }

                felhasznalok.Add(new User(item.FelhasznaloNev, item.NEV, jog, item.ID.ToString(), item.Jelszo));
            }

            return felhasznalok;
        }

        /// <summary>
        /// A berendelt elemek listáját hozzáadja a megfelelő raktárkészlethez
        /// </summary>
        /// <param name="rendelesek">Berendelt dolgok listája</param>
        /// <returns>Elkuldott</returns>
        public async Task SendAlkatreszRendeles(ObservableCollection<Rendeles> rendelesek)
        {
           await this.adatok.AlkatreszRendeles(rendelesek);
        }

        /// <summary>
        /// Berakja az üzenetet az adatbázisba
        /// </summary>
        /// <param name="level">Üzenet</param>
        /// <returns>SendMessage</returns>
        public async Task SendMessage(Level level)
        {
            await this.adatok.SendUzenet(level);
        }

        /// <summary>
        /// Rendelést updateli
        /// </summary>
        /// <param name="megrendeles">Amit szeretnénk updatelni</param>
        public void UpdateRendeles(Megrendeles megrendeles)
        {
            this.adatok.UpdateMegrendeles(megrendeles);
        }

        /// <summary>
        /// Raktárt updateli
        /// </summary>
        /// <param name="alkatresz">Ezekkel az alkatrészekkel</param>
        public void UpdateRaktar(List<Alkatresz> alkatresz)
        {
            this.adatok.RaktarUpdate(alkatresz);
        }

        /// <summary>
        /// Felvesz egy rendelést
        /// </summary>
        /// <param name="megrendeles">Ezt a rendelést veszi fel</param>
        public void RendelesFelvetel(Megrendeles megrendeles)
        {
            this.adatok.RendelesFelvetel(megrendeles);
        }

        /// <summary>
        /// PC státusz változást updatel
        /// </summary>
        /// <param name="pc">Ennek a pc-nek</param>
        public void PCMegrendUpdate(Model.PC pc)
        {
            this.adatok.UpdatePcKesz(pc);
        }

        /// <summary>
        /// PC alkatrészeket visszadja
        /// </summary>
        /// <returns>PcAlkat</returns>
        public List<Model.PC> PCAlkat()
        {
            return this.adatok.PcAlkatreszek();
        }

        /// <summary>
        /// Hány kész pc van
        /// </summary>
        /// <param name="megrendeles">Az adott megrendelésben</param>
        /// <returns>Pc mennyiseg</returns>
        public int Rendhez_pc_mennyiKesz(Megrendeles megrendeles)
        {
            return this.adatok.PcToOrderd(megrendeles);
        }

        /// <summary>
        /// Üzenetek listája
        /// </summary>
        /// <returns>összes üzenet</returns>
        private List<Level> Uzenetek()
        {
            List<Level> levelek = new List<Level>();
            foreach (var item in this.adatok.OsszesUzenet())
            {
                levelek.Add(new Level(item.FELADO, item.CIMZETT, item.KOZLEMENY, Convert.ToDateTime(item.KULDESDATUM)));
            }

            return levelek;
        }

        /// <summary>
        /// Megrendelések listája
        /// </summary>
        /// <returns>összes megrendelés</returns>
        private ObservableCollection<Megrendeles> Megrendelesek()
        {
            ObservableCollection<Megrendeles> megrendelesek = new ObservableCollection<Megrendeles>();
            foreach (var item in this.adatok.OsszRend())
            {
                List<Model.PC> pcs = this.adatok.OrderToPc((int)item.ID);
                megrendelesek.Add(new Megrendeles(pcs, (int)item.ID, item.NEV, item.CSA, (Status)item.STATUSZ));
            }

            return megrendelesek;
        }

        /// <summary>
        /// Feladatok listája
        /// </summary>
        /// <returns>Az összes feladat</returns>
        private List<CSAFeladat> Feladatok()
        {
            List<CSAFeladat> feladatok = new List<CSAFeladat>();
            foreach (var item in this.Megrendelesek())
            {
                feladatok.Add(new CSAFeladat((Allapot)item.Status, item.Pck));
            }

            return feladatok;
        }

        /// <summary>
        /// Alkatrészek listája
        /// </summary>
        /// <returns>összes alkatrész</returns>
        private List<Alkatresz> AlkatreszekList()
        {
            List<Alkatresz> alk = new List<Alkatresz>();
            alk.Add(new Alkatresz("ALAPLAP") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().ALAPLAP });
            alk.Add(new Alkatresz("HDD") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().HDD });
            alk.Add(new Alkatresz("MEMORIA") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().MEMORIA });
            alk.Add(new Alkatresz("PROCESSZOR") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().PROCESSZOR });
            alk.Add(new Alkatresz("SSD") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().SSD });
            alk.Add(new Alkatresz("VIDEOKARTYA") { Mennyiseg = (int)this.adatok.RaktarbanLevoAktareszek().VIDEOKARTYA });
            return alk;
        }
    }
}
