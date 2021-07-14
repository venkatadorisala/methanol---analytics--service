using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Models;
using System.Collections.Generic;

namespace JM.Integration.Methanol.Services.Interface
{
    /// <summary>
    /// Implements converter peak temperature mapper
    /// </summary>
    public interface IPeakTempTransform
    {
        /// <summary>
        /// Type of converter peak temerature KPI
        /// </summary>
        string TransactionType
        {
            get;
            set;
        }

        /// <summary>
        /// Returns converter peak dataset
        /// </summary>
        /// <param name="converterPeakTempKPIDataSet"></param>
        /// <returns></returns>
        public BaseKpiResponse ConverterPeakTempTransform(IList<ConverterPeakTempKPIDataSet> converterPeakTempKPIDataSet);
    }
}