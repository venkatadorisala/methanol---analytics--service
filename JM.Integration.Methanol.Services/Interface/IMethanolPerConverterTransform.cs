using JM.Integration.Methanol.Common.DBModel;
using JM.Integration.Methanol.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Services.Interface
{
    /// <summary>
    /// Implements Methanol Production Per Converter mapper
    /// </summary>
    public interface IMethanolPerConverterTransform
    {
        string TransactionType
        {
            get;
            set;
        }
        /// <summary>
        /// Returns Methanol Production Per Converter dataset
        /// </summary>
        /// <param name="MethanolProductionPerConverterKPIDataSet"></param>
        /// <returns></returns>
        public BaseKpiResponse MethanolPerConverterTransform(IList<MethanolProductionPerConverterKPIDataSet> methanolProductionPerConverterKPIDataSet);
    }
}
