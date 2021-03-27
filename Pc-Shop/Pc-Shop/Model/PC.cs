namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Számítógép típus enum
    /// </summary>
    public enum PcTipus
    {
        Gamer = 1, Standard = 2, ToWork = 3
    }

    /// <summary>
    /// PC osztály
    /// </summary>
    public class PC : Bindable
    {
        /// <summary>
        /// Alkatresz lista
        /// </summary>
        private List<Alkatresz> alkatreszek;

        /// <summary>
        /// int sorszam
        /// </summary>
        private int sorszam;

        /// <summary>
        /// bool keszvan
        /// </summary>
        private bool keszvan;

        /// <summary>
        /// Pc peldany
        /// </summary>
        private PcTipus tipus;

        /// <summary>
        /// PC osztály konstruktora
        /// </summary>
        /// <param name="p"> PC típusa </param>
        /// <param name="alkatreszek"> PC-hez tartozó alkatrész lista </param>
        /// <param name="rdy"> Kész van-e bool </param>
        /// <param name="sorszam"> Sorszáma </param>
        public PC(PcTipus p, List<Alkatresz> alkatreszek, bool rdy, int sorszam)
        {
            this.alkatreszek = alkatreszek;
            this.sorszam = sorszam;
            this.Keszvan = rdy;
            this.Tipus = p;
        }

        /// <summary>
        /// Gets alkatreszek lista
        /// </summary>
        public List<Alkatresz> Alkatreszek
        {
            get
            {
                return this.alkatreszek;
            }
        }

        /// <summary>
        /// Gets sorszám
        /// </summary>
        public int Sorszam
        {
            get
            {
                return sorszam;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether keszvan bool
        /// </summary>
        public bool Keszvan
        {
            get
            {
                return keszvan;
            }

            set
            {
                keszvan = value;
                OnPropertyChanged();
                OnPropertyChanged("KeszSzoveg");
            }
        }

        /// <summary>
        /// Gets státusz
        /// </summary>
        public string KeszSzoveg
        {
            get
            {
                return Keszvan ? "Kész van" : "Nincs kész";
            }
        }

        /// <summary>
        /// Gets or sets PC típusa
        /// </summary>
        public PcTipus Tipus
        {
            get
            {
                return tipus;
            }

            set
            {
                tipus = value;
            }
        }
    }
}
