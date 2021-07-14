using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.Common.DBModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Services.Interface
{
    public interface IReactorPeakTempTransform
    {
        string TransactionType
        {
            get;
            set;
        }

        public BaseKpiResponse ReactorConverterPeakTempTransform(IList<ReactorPeakTempKPIDataSet> reactorPeakTempKPIDataSet);
    }
}
