namespace Pc_Shop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using Services;

    /// <summary>
    /// Bejentkezes ViewModel
    /// </summary>
    public class BejelentkezesViewModel : Bindable
    {
        /// <summary>
        /// loginservice
        /// </summary>
        private ILoginService loginService;

        /// <summary>
        /// username
        /// </summary>
        private string felhasznaloNev;

        /// <summary>
        /// pw
        /// </summary>
        private string jelszo;

        /// <summary>
        /// user
        /// </summary>
        private User felhasznalo;

        /// <summary>
        /// Bejelentkezes ViewModel ctor
        /// </summary>
        public BejelentkezesViewModel()
        {
            loginService = new LoginService();
        }

        /// <summary>
        /// Gets or sets felhasználónév
        /// </summary>
        public string FelhasznaloNev
        {
            get
            {
                return felhasznaloNev;
            }

            set
            {
                felhasznaloNev = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets jelszó
        /// </summary>
        public string Jelszo
        {
            get
            {
                return jelszo;
            }

            set
            {
                jelszo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets felhasználó
        /// </summary>
        public User Felhasznalo
        {
            get
            {
                return felhasznalo;
            }

            set
            {
                felhasznalo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Bejelentkezés megvalósítása
        /// </summary>
        /// <returns> felhasználó </returns>
        public User Bejelentkezes()
        {
            return loginService.Login(jelszo, felhasznaloNev);
        }
    }
}
