namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Allapot Enum
    /// </summary>
    public enum Allapot
    {
        Var = 1, Folyamatban = 2, Befejezve = 3
    }

    /// <summary>
    /// CSAFeladat class
    /// </summary>
    public class CSAFeladat
    {
        /// <summary>
        /// Allapot field
        /// </summary>
        private Allapot allapot;

        /// <summary>
        /// pck lista
        /// </summary>
        private List<PC> pc;

        /// <summary>
        /// haladas int
        /// </summary>
        private int haladas;

        /// <summary>
        /// csa peldany
        /// </summary>
        private CSA csa;

        /// <summary>
        /// CSA Feladat ctor
        /// </summary>
        /// <param name="allapot"> allapot</param>
        /// <param name="pc">pc</param>
        public CSAFeladat(Allapot allapot, List<PC> pc)
        {
            this.Allapot = allapot;
            this.Pc = pc;
        }

        /// <summary>
        /// Gets or sets Allapot
        /// </summary>
        public Allapot Allapot
        {
            get
            {
                return this.allapot;
            }

            set
            {
                this.allapot = value;
            }
        }

        /// <summary>
        /// Gets or sets PC
        /// </summary>
        public List<PC> Pc
        {
            get
            {
                return this.pc;
            }

            set
            {
                this.pc = value;
            }
        }

        /// <summary>
        /// Gets or sets Haladas
        /// </summary>
        public int Haladas
        {
            get
            {
                return this.haladas;
            }

            set
            {
                this.haladas = value;
            }
        }

        /// <summary>
        /// Gets or sets CSA
        /// </summary>
        public CSA Csa
        {
            get
            {
                return this.csa;
            }

            set
            {
                this.csa = value;
            }
         }
        }
}
