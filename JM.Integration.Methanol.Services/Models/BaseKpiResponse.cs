using System.Collections.Generic;

namespace JM.Integration.Methanol.Services.Models
{
    /// <summary>
    /// Use this as base class for API consisting charts
    /// </summary>
    public class BaseKpiResponse
    {
        public BaseKpiResponse()
        {
            DataSet = new List<BaseKpiDataset>();
            DataPoints = new List<BaseDataPoints>();
        }

        public string Title { get; set; }
        public IList<BaseKpiDataset> DataSet { get; set; }
        public IList<BaseDataPoints> DataPoints { get; set; }
        public string KpiTitle { get; set; }
        public double MainValue { get; set; }
        public double MaxValue { get; set; }
     
        public string Unit { get; set; }
        public string Indicator { get; set; }
    }

    public class BaseKpiDataset
    {
        public BaseKpiDataset()
        {
            Details = new List<BaseKpiDetails>();
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public IList<BaseKpiDetails> Details { get; set; }
    }

    public class BaseKpiDetails
    {
        public BaseKpiDetails()
        {
            Unit = new BaseKpiUnit();
        }

        public string Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public double Increment { get; set; }
        public double Decrement { get; set; }
        public BaseKpiUnit Unit { get; set; }
    }

    public class BaseKpiUnit
    {
        public string Type { get; set; }
        public string Symbol { get; set; }
    }

    public class BaseDataPoints
    {
        public double Y { get; set; }
        public string Label { get; set; }
    }
}