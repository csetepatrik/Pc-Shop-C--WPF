namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Jogosultság enum
    /// </summary>
    public enum Jogosultsag
    {
        CEO = 1, CSA = 2, SM = 3
    }

    /// <summary>
    /// Felhasználó osztály
    /// </summary>
    public class User
    {
        /// <summary>
        /// nev string
        /// </summary>
        private string nev;

        /// <summary>
        /// jogosultsag peldany
        /// </summary>
        private Jogosultsag jogosultsag;

        /// <summary>
        /// id string
        /// </summary>
        private string id;

        /// <summary>
        /// username field
        /// </summary>
        private string userName;

        /// <summary>
        /// password string
        /// </summary>
        private string password;

        /// <summary>
        /// Felhasználó konstruktora
        /// </summary>
        /// <param name="userName"> Felhasználó neve a rendszerben </param>
        /// <param name="nev"> Felhasználó valódi neve </param>
        /// <param name="jog"> Felhasználó jogosultsága </param>
        /// <param name="id"> Felhasználó azonosítója </param>
        /// <param name="pw"> Felhasználó jelszava </param>
        public User(string userName, string nev, Jogosultsag jog, string id, string pw)
        {
            this.nev = nev;
            this.jogosultsag = jog;
            this.id = id;
            this.password = pw;
            this.userName = userName;
        }

        /// <summary>
        /// Gets nev
        /// </summary>
        public string Nev
        {
            get { return this.nev; }
        }

        /// <summary>
        /// Gets or sets jogosultság
        /// </summary>
        public Jogosultsag Jogosultsag
        {
            get
            {
                return this.jogosultsag;
            }

            set
            {
                this.jogosultsag = value;
            }
        }

        /// <summary>
        /// Gets azonosito
        /// </summary>
        public string Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets felhasználónév
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
        }

        /// <summary>
        /// Gets jelszó
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
        }

        /// <summary>
        /// Eredeti string felülírása
        /// </summary>
        /// <returns>Visszaadja a nevet jogosultsággal ellátva</returns>
        public override string ToString()
        {
            return this.nev + " - " + this.jogosultsag.ToString();
        }
    }
}
