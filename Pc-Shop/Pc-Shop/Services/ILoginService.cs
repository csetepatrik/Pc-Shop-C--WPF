namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// ilogin
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="password">Jelszó</param>
        /// <param name="userName">Felhasználónév</param>
        /// <returns>Login User</returns>
        User Login(string password, string userName);
    }
}
