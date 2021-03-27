namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// MessageService
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// repo peldany
        /// </summary>
        private DataAccessLayer repository;

        /// <summary>
        /// uzenetek
        /// </summary>
        private List<Level> uzenetek;

        /// <summary>
        /// bejovouzi
        /// </summary>
        private List<Level> bejovoUzenetek;

        /// <summary>
        /// kimenouzenetek
        /// </summary>
        private ObservableCollection<Level> kimenoUzenetek;

        /// <summary>
        /// string nev
        /// </summary>
        private string nev;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="nev">nev</param>
        public MessageService(string nev)
        {
            this.nev = nev;
            this.repository = new DataAccessLayer();
            this.uzenetek = repository.GetAllUzenetek;
            this.BejovoUzenetek = new List<Level>();
            this.KimenoUzenetek = new ObservableCollection<Level>();
            this.BeerkezoUzentek();
            this.KimenoUzentek();
        }

        /// <summary>
        /// Gets or sets BejovoUzenetek
        /// </summary>
        public List<Level> BejovoUzenetek
        {
            get
            {
                return this.bejovoUzenetek;
            }

            set
            {
                this.bejovoUzenetek = value;
            }
        }

        /// <summary>
        /// Gets or sets KimenoUzenet
        /// </summary>
        public ObservableCollection<Level> KimenoUzenetek
        {
            get
            {
                return this.kimenoUzenetek;
            }

            set
            {
                this.kimenoUzenetek = value;
            }
        }

        /// <summary>
        /// GetAllLevels
        /// </summary>
        /// <returns>GetAllLevels</returns>
        public List<Level> GetAllLevels()
        {
            return repository.GetAllUzenetek;
        }

        /// <summary>
        /// SendAllLevel
        /// </summary>
        /// <param name="level">level</param>
        public void SendLevel(Level level)
        {
            repository.SendMessage(level);
        }

        /// <summary>
        /// KimenoUzenet
        /// </summary>
        private void KimenoUzentek()
        {
            foreach (var item in repository.GetAllUzenetek)
            {
                if (item.Felado == this.nev)
                {
                    this.KimenoUzenetek.Add(item);
                }
            }
        }

        /// <summary>
        /// BeerkezoUzenetek
        /// </summary>
        private void BeerkezoUzentek()
        {
            foreach (var item in repository.GetAllUzenetek)
            {
                if (item.Cimzett == this.nev)
                {
                    this.BejovoUzenetek.Add(item);
                }
            }            
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="level">level</param>
        private void SendMessage(Level level)
        {
            repository.SendMessage(level);
        }
    }
}
