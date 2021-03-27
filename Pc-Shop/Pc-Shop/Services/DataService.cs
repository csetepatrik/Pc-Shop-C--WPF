using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public class DataService : IDataService
    {
        /// <summary>
        /// Készleten lévő alkatrészek listája.
        /// </summary>
        private List<Alkatresz> alkatreszek;
        /// <summary>
        /// Alkatrészrendelések
        /// </summary>
        private ObservableCollection<Rendeles> alkatreszRendelesek;
        /// <summary>
        /// Megrendelések
        /// </summary>
        private ObservableCollection<Megrendeles> rendelesek;
        /// <summary>
        /// repository
        /// </summary>
        private DataAccessLayer repo;
        /// <summary>
        /// Felhasználok listája
        /// </summary>
        private List<User> felhasznalok;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DataService()
        {
            repo = new DataAccessLayer();
            this.alkatreszRendelesek = new ObservableCollection<Rendeles>();
            rendelesek = repo.GetAllMegrendeles;
            this.alkatreszek = repo.GetAlkatreszek;
            felhasznalok = repo.Felhasznalok();
            RendelesMAXID = repo.RendelesMaxID;
        }

        /// <summary>
        /// Property for Felhasználok.
        /// </summary>
        public List<User> Felhasznalok
        {
            get
            {
                return felhasznalok;
            }
        }

        public int RendelesMAXID
        {
            get; set;
        }

        public List<Alkatresz> GetAllAlkatresz()
        {
            return this.alkatreszek;
        }

        public ObservableCollection<Rendeles> GetAllAlkatreszRendeles()
        {
            return this.alkatreszRendelesek;
        }

        public ObservableCollection<Megrendeles> GetAllMegrendeles()
        {
            return rendelesek;
        }

        public async Task SendAlkatreszRendeles(ObservableCollection<Rendeles> alkatreszRendeles)
        {
            await repo.SendAlkatreszRendeles(this.alkatreszRendelesek);
        }

        public void UpdateStatus(Megrendeles megrendeles)
        {
            repo.UpdateRendeles(megrendeles);
        }

        public void PCUpdate(Model.PC pc)
        {
            repo.PCMegrendUpdate(pc);
        }

        public List<Alkatresz> RendelhetoAlkatreszek()
        {
            List<Alkatresz> alkatresz = new List<Alkatresz>();
            alkatresz.Add(new Alkatresz("ALAPLAP"));
            alkatresz.Add(new Alkatresz("SSD"));
            alkatresz.Add(new Alkatresz("HDD"));
            alkatresz.Add(new Alkatresz("PROCESSZOR"));
            alkatresz.Add(new Alkatresz("VIDEOKARTYA"));
            alkatresz.Add(new Alkatresz("MEMORIA"));
            return alkatresz;

        }

        /// <summary>
        /// Frissíti az alkatrész adatbázist.
        /// </summary>
        /// <param name="alkatresz">Ezekkel az alkatrészekkel.</param>
        public void RaktarUpdate(List<Alkatresz> alkatresz)
        {
            repo.UpdateRaktar(alkatresz);
        }

        /// <summary>
        /// Meghatározza a kellő alkatrészeket egy PC-hez.
        /// </summary>
        /// <param name="pc">Ezekhez határozza meg.</param>
        /// <returns>A kellő alkatrészek listáját.</returns>
        public List<Alkatresz> KellAlkatreszMeghataroz(List<Model.PC> pc)
        {
            List<Alkatresz> kell = RendelhetoAlkatreszek();
            foreach (var item in pc)
            {
                foreach (var alkatresz in item.Alkatreszek)
                {
                    foreach (var kellalkatresz in kell)
                    {
                        if (alkatresz.Nev == kellalkatresz.Nev)
                        {
                            kellalkatresz.Mennyiseg += alkatresz.Mennyiseg;
                        }
                    }
                }
            }
            return kell;
        }

        /// <summary>
        /// Összehasonlítja, hogy van-e elég alkatrész az adott feladat végrehajtásához.
        /// </summary>
        /// <param name="kell">Ezek az alkatrészek kellenek.</param>
        /// <param name="van">Ezek az alkatrészek vannak.</param>
        /// <returns>Van-e megfelelő szám, vagy nincs. </returns>
        public bool KelloAlkatreszOsszehasonlitas(List<Alkatresz> kell, List<Alkatresz> van)
        {
            foreach (var va in van)
            {
                foreach (var ke in kell)
                {
                    if (va.Nev == ke.Nev)
                    {
                        if (va.Mennyiseg - ke.Mennyiseg < 0)
                        {
                            throw new NincsElegAlkatreszException();
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Felveszi a rendelést.
        /// </summary>
        /// <param name="megrendeles">Ezt a rendelést veszi fel.</param>
        public void RendelesFelvetele(Megrendeles megrendeles)
        {
            repo.RendelesFelvetel(megrendeles);
        }

        /// <summary>
        /// Visszaadja a PCTipusok listáját.
        /// </summary>
        /// <returns></returns>
        public List<PcTipus> PcTipusok()
        {
            List<PcTipus> tipusok = new List<PcTipus>();
            tipusok.Add(PcTipus.Gamer);
            tipusok.Add(PcTipus.Standard);
            tipusok.Add(PcTipus.ToWork);
            return tipusok;
        }

        /// <summary>
        /// Visszaadja a PC-ket alkatrészekkel feltöltve.
        /// </summary>
        /// <returns></returns>
        public List<PC> PCAlkatreszekkel()
        {
            return repo.PCAlkat();
        }

        /// <summary>
        /// Megmondja, hogy mennyi PC van kész adott rendeléshez.
        /// </summary>
        /// <param name="megrendeles">Ehhez a rendeléshez.</param>
        /// <returns>Kész PC-k száma.</returns>
        public int Megrendeleshez_Mennyi_PC_Done(Megrendeles megrendeles)
        {
            return repo.Rendhez_pc_mennyiKesz(megrendeles);
        }

        /// <summary>
        /// Updateli a raktárat.
        /// </summary>
        /// <param name="kell">Ezekkel kell updatelni.</param>
        /// <param name="van">Az eddigi raktár</param>
        public void Rakup(List<Alkatresz> kell, List<Alkatresz> van)
        {
            foreach (var item in kell)
            {
                foreach (var va in van)
                {
                    if (item.Nev == va.Nev)
                    {
                        va.Mennyiseg -= item.Mennyiseg;
                    }
                }
            }
            RaktarUpdate(this.alkatreszek);
        }

        /// <summary>
        /// Meghatározza a kellő alkatrészeket.
        /// </summary>
        /// <param name="pck">Ezekhez a PC-k hez.</param>
        /// <returns>Az alkatrészet listáját.</returns>
        public List<Alkatresz> KelloAlkMeghatarozas(List<Model.PC> pck)
        {
            List<Alkatresz> kell = KellAlkatreszMeghataroz(pck);
            return kell;
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott PC-k elkészültek-e már.
        /// </summary>
        /// <param name="pck">Ezek a PC-k</param>
        /// <returns>Igen vagy nem.</returns>
        public bool OsszPCElkeszult(ObservableCollection<Model.PC> pck)
        {
            int db = 0;
            foreach (var item in pck)
            {
                if (item.Keszvan)
                {
                    db++;
                }
            }

            if (db == pck.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
