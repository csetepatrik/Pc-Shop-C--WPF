namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Állapot enum
    /// </summary>
    public enum Status
    {
        Nincs_elvallalva = 1, Elvallalva = 2, Kesz = 3
    }

    /// <summary>
    /// Megrendelés osztály
    /// </summary>
    public class Megrendeles : Bindable
    {
        /// <summary>
        /// int sorszam
        /// </summary>
        private int sorszam;

        /// <summary>
        /// string megrendelo
        /// </summary>
        private string megrendelo;

        /// <summary>
        /// string dolgozo
        /// </summary>
        private string dolgozo;

        /// <summary>
        /// int darab
        /// </summary>
        private int darab;

        /// <summary>
        /// status status
        /// </summary>
        private Status status;

        /// <summary>
        /// Pc lista
        /// </summary>
        private List<PC> pck;

        /// <summary>
        /// Megrendelés konstruktora
        /// </summary>
        /// <param name="pck"> PC lista </param>
        /// <param name="sorszam"> Megrendelés sorszáma </param>
        /// <param name="megrendelo"> Megrendelo </param>
        /// <param name="dolgozo"> Megrendelést végrehajtó alkalmazott </param>
        /// <param name="status">status</param>
        public Megrendeles(List<PC> pck, int sorszam, string megrendelo, string dolgozo, Status status)
        {
            this.sorszam = sorszam;
            this.megrendelo = megrendelo;
            this.dolgozo = dolgozo;
            this.pck = pck;
            this.Status = status;
            this.darab = pck.Count;
        }

        /// <summary>
        /// Gets or sets pck listája
        /// </summary>
        public List<PC> Pck
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
        /// Gets or sets pck állapota
        /// </summary>
        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets megrendelés sorszáma
        /// </summary>
        public int Sorszam
        {
            get
            {
                return sorszam;
            }
        }

        /// <summary>
        /// Gets megrendelo
        /// </summary>
        public string Megrendelo
        {
            get
            {
                return megrendelo;
            }
        }

        /// <summary>
        /// Gets mennyiség
        /// </summary>
        public int Darab
        {
            get
            {
                return darab;
            }
        }

        /// <summary>
        /// Gets or sets a pc-n dolgozó alkalmazott
        /// </summary>
        public string Dolgozo
        {
            get
            {
                return dolgozo;
            }

            set
            {
                dolgozo = value;
                OnPropertyChanged();
            }
        }
    }
}
