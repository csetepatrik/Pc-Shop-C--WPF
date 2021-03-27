namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// imessage
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// getallLevels
        /// </summary>
        /// <returns>GetALlLevels</returns>
        List<Level> GetAllLevels();

        /// <summary>
        /// sendlevel
        /// </summary>
        /// <param name="level">level</param>
        void SendLevel(Level level);
    }
}
