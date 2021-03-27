namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// InvalidLogin class
    /// </summary>
    public class InvalidLoginException : Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InvalidLoginException() : base("Rossz felhasználónév vagy jelszó!")
        {
        }
    }
}
