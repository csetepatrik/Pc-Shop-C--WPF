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
    /// idataservice
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Összes alkatrész listája.
        /// </summary>
        /// <returns>Az összes alkatrész.</returns>
        List<Alkatresz> GetAllAlkatresz();
        
        /// <summary>
        /// Az összes rendelés.
        /// </summary>
        /// <returns>Az összes rendelések listája.</returns>
        ObservableCollection<Megrendeles> GetAllMegrendeles();

        /// <summary>
        /// Az összes alkatrész, amit berendelünk.
        /// </summary>
        /// <returns>A berendelt alkatrészek listája</returns>
        ObservableCollection<Rendeles> GetAllAlkatreszRendeles();

        /// <summary>
        /// A rendelés leadása.
        /// </summary>
        /// <param name="alkatreszRendeles">A megrendelendő alkatrészek listája.</param>
        /// <returns>Send</returns>
        Task SendAlkatreszRendeles(ObservableCollection<Rendeles> alkatreszRendeles);
    }
}
