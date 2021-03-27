namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using Model;

    /// <summary>
    /// Loginservice class
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// Ascii string
        /// </summary>
        public const string ASCII = @" abcdefghijklmnopqrstuvwxyzáéíóöőúüűABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÖŐÚÜŰ,.:|!+0123456789#$%&'()*-/;<=>?@[\]^_`{}~ÇâäçëîÄæÆô¢£¥ƒªº¿⌐¬½¼¡«»░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙Ô╒╓╫╪┘┌█▄▌▐▀ßµ≡±≥≤⌠⌡÷≈∙•?²■";

        /// <summary>
        /// int kulcs
        /// </summary>
        private const int KULCS = 5;

        /// <summary>
        /// repo peldany
        /// </summary>
        private DataAccessLayer repository;

        /// <summary>
        /// user lista
        /// </summary>
        private List<User> users;

        /// <summary>
        /// ctor
        /// </summary>
        public LoginService()
        {
            repository = new DataAccessLayer();
            users = repository.Felhasznalok();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="password">username</param>
        /// <param name="userName">pw</param>
        /// <returns>User</returns>
        public User Login(string password, string userName)
        {           
            string vissza = Titkosit(password);
            foreach (var user in users)
            {
                if (user.Password == vissza && user.UserName == userName)
                {                   
                    return user;                   
                }
            }
            
               throw new InvalidLoginException();                                  
        }

        /// <summary>
        /// Titkositas
        /// </summary>
        /// <param name="nyilt_szoveg">szoveg</param>
        /// <returns>Titkositott szoveg</returns>
        public string Titkosit(string nyilt_szoveg)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(nyilt_szoveg);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }
    }
}
