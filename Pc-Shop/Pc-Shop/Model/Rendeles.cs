namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// rendeles class
    /// </summary>
    public class Rendeles
    {
        /// <summary>
        /// Alkatresz neve
        /// </summary>
        private Alkatresz alkatresz;

        /// <summary>
        /// Mennyiseg
        /// </summary>
        private int mennyiseg;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alkatresz">alkatresz adatadasa</param>
        /// <param name="mennyiseg">darab szam atadasa</param>
        public Rendeles(Alkatresz alkatresz, int mennyiseg)
        {
            this.alkatresz = alkatresz;
            this.mennyiseg = mennyiseg;
        }

        /// <summary>
        /// Gets ordered alkatresz
        /// </summary>
        public Alkatresz Alkatresz
        {
            get
            {
                return this.alkatresz;
            }            
        }

        /// <summary>
        /// Gets quantity
        /// </summary>
        public int Mennyiseg
        {
            get
            {
                return this.mennyiseg;
            }          
        }
    }
}
