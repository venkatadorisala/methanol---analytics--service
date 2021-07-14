using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Models;
using System.Collections.Generic;

namespace JM.Integration.Methanol.Services.Interface
{
    public interface IKPIResponseTransform
    {
        string TransactionType
        {
            get;
            set;
        }

        BaseKpiResponse Transform(IList<KPIDbDataSet> kPIDbDataSet);
    }
}