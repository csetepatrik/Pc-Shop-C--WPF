namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// NincsEleg Exception
    /// </summary>
    public class NincsElegAlkatreszException : Exception
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NincsElegAlkatreszException() : base("Nincs elég alkatrész!")
        {
        }
    }
}
