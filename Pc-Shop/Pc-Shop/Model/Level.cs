namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Level class
    /// </summary>
    public class Level : Bindable
    {
        /// <summary>
        /// felado
        /// </summary>
        private string felado;

        /// <summary>
        /// string cimzett
        /// </summary>
        private string cimzett;

        /// <summary>
        /// uzenet string
        /// </summary>
        private string uzenet;

        /// <summary>
        /// datum field
        /// </summary>
        private DateTime datum;

        /// <summary>
        /// Levél konstruktora
        /// </summary>
        /// <param name="felado"> Levél feladója </param>
        /// <param name="cimzett"> Levél címzettje </param>
        /// <param name="uzenet"> Levél szövege </param>
        /// <param name="datum"> Elküldés dátuma </param>
        public Level(string felado, string cimzett, string uzenet, DateTime datum)
        {
            this.felado = felado;
            this.cimzett = cimzett;
            this.uzenet = uzenet;
            this.datum = datum;
        }

        /// <summary>
        /// Gets felado
        /// </summary>
        public string Felado
        {
            get
            {
                return felado;
            }
        }

        /// <summary>
        /// Gets cimzett
        /// </summary>
        public string Cimzett
        {
            get
            {
                return cimzett;
            }
        }

        /// <summary>
        /// Gets uzenet szovege
        /// </summary>
        public string Uzenet
        {
            get
            {
                return uzenet;
            }
        }

        /// <summary>
        /// Gets or sets uzenet datuma
        /// </summary>
        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                datum = value;
            }
        }
    }
}
