using JM.Integration.Methanol.Services.Models;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Interface
{
    /// <summary>
    /// Contains methods to be implemented for Syngas Split KPI
    /// </summary>
    public interface ISyngasSplitService
    {
        /// <summary>
        /// Takes in section id to return Syngas split response for that particular Section
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns>Syngas split response for Section </returns>
        Task<SyngasSplit> GetSyngasSplitResponse(string sectionId);
    }
}