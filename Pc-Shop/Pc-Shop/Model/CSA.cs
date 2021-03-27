namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// CSA class
    /// </summary>
    public class CSA
    {
        /// <summary>
        /// csa id
        /// </summary>
        private int id;

        /// <summary>
        /// username field
        /// </summary>
        private string userName;

        /// <summary>
        /// CSA ctor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userName">username</param>
        public CSA(int id, string userName)
        {
            this.id = id;
            this.userName = userName;
        }

        /// <summary>
        /// Gets ID
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets UserName
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
        }
    }
}
