namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Alkatresz class
    /// </summary>
    public class Alkatresz : Bindable
    {
        /// <summary>
        /// Alkatresz neve
        /// </summary>
        private string nev;

        /// <summary>
        /// Alkatresz darab szama
        /// </summary>
        private int mennyiseg;

        /// <summary>
        /// Alkatresz
        /// </summary>
        /// <param name="nev"> nev adatasa</param>
        public Alkatresz(string nev)
        {
            this.nev = nev;
        }

        /// <summary>
        /// Gets pc alkatresz
        /// </summary>
        public string Nev
        {
            get
            {
                return nev;
            }
        }

        /// <summary>
        /// Gets or sets Mennyiseg
        /// </summary>
        public int Mennyiseg
        {
            get
            {
                return mennyiseg;
            }

            set
            {
                mennyiseg = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// nev kiiratasa
        /// </summary>
        /// <returns>Nev</returns>
        public override string ToString()
        {
            return nev;
        }
    }
}
