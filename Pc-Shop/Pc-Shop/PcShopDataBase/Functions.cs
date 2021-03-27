namespace PcShopDataBase
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// funkciok osztalya
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// Entity
        /// </summary>
        private Database1Entities entity;

        /// <summary>
        /// iuz
        /// </summary>
        private int iuz;

        /// <summary>
        /// MegrendelesId
        /// </summary>
        private int imegrend;

        /// <summary>
        /// A Rendeles legnagyobb ID
        /// </summary>
        private int maxId;

        /// <summary>
        /// PCRendelesID
        /// </summary>
        private int pcrendid;

        /// <summary>
        /// function cons
        /// </summary>
        public Functions()
        {
            this.entity = new Database1Entities();
            if (this.OsszesUzenet().Count == 0)
            {
                this.iuz = 0;
            }
            else
            {
                this.iuz = (int)this.entity.Uzeneteks.Max(x => x.ID);
            }

            this.pcrendid = (int)this.entity.PcRendelesres.Max(x => x.ID);
            this.maxId = (int)this.entity.Rendeleseks.Max(x => x.ID);
            this.imegrend = (int)this.entity.Rendeleseks.Max(x => x.ID);
        }

        /// <summary>
        /// Gets or sets rendeles aktualis legnagyobb ID-ja
        /// </summary>
        public int MaxId
        {
            get
            {
                return this.maxId;
            }

            set
            {
                this.maxId = value;
            }
        }

        /// <summary>
        /// megadja hogy milyen regisztralt felhasznalok vannak
        /// </summary>
        /// <returns>felhasznalok listajat</returns>
        public List<Felhasznalo> Users()
        {
            var us = this.entity.Felhasznaloes.Select(x => x).ToList();
            return us;
        }

        /// <summary>
        /// megadja az adatbazisbol az osszes uzenetet
        /// </summary>
        /// <returns>uzenetek</returns>
        public List<Uzenetek> OsszesUzenet()
        {
            var uzenetek = this.entity.Uzeneteks.Select(x => x).ToList();
            return uzenetek;
        }

        /// <summary>
        /// a megfelelo pchez rendeljuk az alkatreszt
        /// </summary>
        /// <returns>pc lista</returns>
        public List<Model.PC> PcAlkatreszek()
        {
            var pcalk = this.entity.PcAlkatreszeks.Select(x => x).ToList();
            List<Model.PC> pc = new List<Model.PC>();
            foreach (var pcitem in pcalk)
            {
                PcTipus p;
                if (pcitem.TIPUS == "Gamer")
                {
                    p = PcTipus.Gamer;
                }
                else if (pcitem.TIPUS == "Standard")
                {
                    p = PcTipus.Standard;
                }
                else
                {
                    p = PcTipus.ToWork;
                }

                List<Alkatresz> alkatreszek = new List<Alkatresz>();
                alkatreszek.Add(new Alkatresz("ALAPLAP") { Mennyiseg = int.Parse(pcitem.ALAPLAP) });
                alkatreszek.Add(new Alkatresz("HDD") { Mennyiseg = (int)pcitem.HDD });
                alkatreszek.Add(new Alkatresz("MEMORIA") { Mennyiseg = (int)pcitem.MEMORIA });
                alkatreszek.Add(new Alkatresz("PROCESSZOR") { Mennyiseg = (int)pcitem.PROCESSZOR });
                alkatreszek.Add(new Alkatresz("SSD") { Mennyiseg = (int)pcitem.SSD });
                alkatreszek.Add(new Alkatresz("VIDEOKARTYA") { Mennyiseg = (int)pcitem.VIDEOKARTYA });
                pc.Add(new Model.PC(p, alkatreszek, false, (int)pcitem.ID));
            }

            return pc;
        }

        /// <summary>
        /// Megadja tipustol fuggoen mennyi alkatresz kell egy pchez
        /// </summary>
        /// <returns>Az adott PCbol mennyi kell</returns>
        public List<PcAlkatreszek> PcHezMennyi()
        {
            var pcalk = this.entity.PcAlkatreszeks.Select(x => x).ToList();
            return pcalk;
        }

        /// <summary>
        /// Megadja az osszes rendelest
        /// </summary>
        /// <returns>rend</returns>
        public List<Rendelesek> OsszRend()
        {
            var rend = this.entity.Rendeleseks.Select(x => x).ToList();
            return rend;
        }

        /// <summary>
        /// adott megrendelesnel hany PC van kesz
        /// </summary>
        /// <param name="megrendeles">megrendeles</param>
        /// <returns>kesz db szam</returns>
        public int PcToOrderd(Megrendeles megrendeles)
        {
            var rend = this.entity.PcRendelesres.Where(x => x.RENDID == megrendeles.Sorszam).ToList();
            int mennyi = 0;
            foreach (var item in rend)
            {
                if (item.STATUSZ == 2)
                {
                    mennyi++;
                }
            }

            return mennyi;
        }

        /// <summary>
        /// megadott ID-u rendeleshez PC
        /// </summary>
        /// <param name="rendelesID">rendeleshez tartozo id</param>
        /// <returns> Adott rendeleshez milyen PC tartozik</returns>
        public List<Model.PC> OrderToPc(int rendelesID)
        {
            var pcs = this.entity.PcRendelesres.Where(x => x.RENDID == rendelesID).Select(z => z).ToList();
            List<Model.PC> pcss = new List<Model.PC>();
            foreach (var item in pcs)
            {
                PcTipus pt;
                if (item.TIPUS == "Standard")
                {
                    pt = PcTipus.Standard;
                }
                else if (item.TIPUS == "Gamer")
                {
                    pt = PcTipus.Gamer;
                }
                else
                {
                    pt = PcTipus.ToWork;
                }

                List<Alkatresz> alk = new List<Alkatresz>();
                foreach (var it in this.PcHezMennyi())
                {
                    if (it.TIPUS == pt.ToString())
                    {
                        alk.Add(new Alkatresz("ALAPLAP") { Mennyiseg = int.Parse(it.ALAPLAP) });
                        alk.Add(new Alkatresz("HDD") { Mennyiseg = (int)it.HDD });
                        alk.Add(new Alkatresz("MEMORIA") { Mennyiseg = (int)it.MEMORIA });
                        alk.Add(new Alkatresz("PROCESSZOR") { Mennyiseg = (int)it.PROCESSZOR });
                        alk.Add(new Alkatresz("SSD") { Mennyiseg = (int)it.SSD });
                        alk.Add(new Alkatresz("VIDEOKARTYA") { Mennyiseg = (int)it.VIDEOKARTYA });
                    }
                }

                bool kesz;
                if (item.STATUSZ == 3)
                {
                    kesz = true;
                }
                else
                {
                    kesz = false;
                }

                pcss.Add(new Model.PC(pt, alk, kesz, (int)item.ID));
            }

            return pcss;
        }

        /// <summary>
        /// adott megrendeles statuszat valtoztatja
        /// </summary>
        /// <param name="meg">a megrendeles</param>
        public void UpdateMegrendeles(Megrendeles meg)
        {
            var me = this.entity.Rendeleseks.Where(x => x.ID == meg.Sorszam).First();
            me.STATUSZ = (int)meg.Status;
            me.CSA = meg.Dolgozo;

            var pcMe = this.entity.PcRendelesres.Where(x => x.RENDID == meg.Sorszam).First();
            pcMe.STATUSZ = (int)meg.Status;
            this.entity.SaveChanges();
        }

        /// <summary>
        /// uj megrendelés felvétele
        /// </summary>
        /// <param name="rendeles">adott rendelés</param>
        public void RendelesFelvetel(Megrendeles rendeles)
        {
            var rend = new Rendelesek()
            {
                ID = this.MaxId++,
                NEV = rendeles.Megrendelo,
                STATUSZ = (int)rendeles.Status
            };

            this.entity.Rendeleseks.Add(rend);

            foreach (var it in rendeles.Pck)
            {
                var pcs = new PcRendelesre()
                {
                    ID = this.pcrendid++,
                    RENDID = this.MaxId,
                    STATUSZ = 1,
                    TIPUS = it.Tipus.ToString()
                };

                this.entity.PcRendelesres.Add(pcs);
            }

            this.entity.SaveChanges();
        }

        /// <summary>
        /// Adott pc statusz frissites
        /// </summary>
        /// <param name="p">adott PC</param>
        public void UpdatePcKesz(Model.PC p)
        {
            var pck = this.entity.PcRendelesres.Where(x => x.ID == p.Sorszam).First();
            if (p.Keszvan)
            {
                pck.STATUSZ = 3;
            }
            else
            {
                pck.STATUSZ = 2;
            }

            this.entity.SaveChanges();
        }

        /// <summary>
        /// Megadja hogy melyik alkatreszbol mennyi van a raktárban
        /// </summary>
        /// <returns>A raktárban lévő alkatrészek száma</returns>
        public Raktar RaktarbanLevoAktareszek()
        {
            var rak = this.entity.Raktars.Select(x => x).First();
            return rak;
        }

        /// <summary>
        /// Updateli a raktárat.
        /// </summary>
        /// <param name="alkatreszek">Ezekkel az alkatrészekkel</param>
        public void RaktarUpdate(List<Alkatresz> alkatreszek)
        {
            var keszlet = this.entity.Raktars.Select(x => x).First();
            foreach (var it in alkatreszek)
            {
                switch (it.Nev.ToUpper())
                {
                    case "ALAPLAP":
                        keszlet.ALAPLAP = it.Mennyiseg;
                        break;
                    case "HDD":
                        keszlet.HDD = it.Mennyiseg;
                        break;
                    case "MEMORIA":
                        keszlet.MEMORIA = it.Mennyiseg;
                        break;
                    case "PROCESSZOR":
                        keszlet.PROCESSZOR = it.Mennyiseg;
                        break;
                    case "SSD":
                        keszlet.SSD = it.Mennyiseg;
                        break;
                    case "VIDEOKARTYA":
                        keszlet.VIDEOKARTYA = it.Mennyiseg;
                        break;
                }
            }

            this.entity.SaveChanges();
        }

        /// <summary>
        /// Elkuld egy uzenetet valakinek
        /// </summary>
        /// <param name="uzenet">kuldott uzenet</param>
        /// <returns>senduzenetek</returns>
        public async Task SendUzenet(Level uzenet)
        {
            var uzi = new Uzenetek()
            {
                ID = this.iuz++,
                FELADO = uzenet.Felado,
                CIMZETT = uzenet.Cimzett,
                KULDESDATUM = uzenet.Datum,
                KOZLEMENY = uzenet.Uzenet
            };

            this.entity.Uzeneteks.Add(uzi);
            this.entity.SaveChanges();
        }

        /// <summary>
        /// Alkatreszt rendel
        /// </summary>
        /// <param name="alk">Megrendelendo alkatresz</param>
        /// <returns>Alkatresz</returns>
        public async Task AlkatreszRendeles(ObservableCollection<Rendeles> alk)
        {
            var keszlet = this.entity.Raktars.FirstOrDefault();
            foreach (var it in alk)
            {
                switch (it.Alkatresz.Nev.ToUpper())
                {
                    case "ALAPLAP":
                        keszlet.ALAPLAP += it.Mennyiseg;
                        break;
                    case "HDD":
                        keszlet.HDD += it.Mennyiseg;
                        break;
                    case "MEMORIA":
                        keszlet.MEMORIA += it.Mennyiseg;
                        break;
                    case "PROCESSZOR":
                        keszlet.PROCESSZOR += it.Mennyiseg;
                        break;
                    case "SSD":
                        keszlet.SSD += it.Mennyiseg;
                        break;
                    case "VIDEOKARTYA":
                        keszlet.VIDEOKARTYA += it.Mennyiseg;
                        break;
                }

                this.entity.SaveChanges();
            }
        }
    }
}
