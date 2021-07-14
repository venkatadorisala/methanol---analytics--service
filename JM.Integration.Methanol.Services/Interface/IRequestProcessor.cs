using JM.Integration.Methanol.Services.Models;
using System.Threading.Tasks;

namespace JM.Integration.Methanol.Services.Interface
{
    /// <summary>
    /// Implements method for KPI response
    /// </summary>
    public interface IRequestProcessor 
    {

        string TransactionName
        {
            get;
            set;
        }

        Task<BaseKpiResponse> GetKPIResponse(string requestId);
    }
}