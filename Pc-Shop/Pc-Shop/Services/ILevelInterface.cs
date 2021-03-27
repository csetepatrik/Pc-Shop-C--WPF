namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// ilevelinterface
    /// </summary>
    public interface ILevelInterface
    {
        /// <summary>
        /// Get Levél.
        /// </summary>
        /// <returns>A levelek listája.</returns>
        List<Level> GetAllLevel();

        /// <summary>
        /// Levél küldése
        /// </summary>
        /// <param name="level">Levél, amit küldeni szeretnénk.</param>
        void SendLevel(Level level);
    }
}
