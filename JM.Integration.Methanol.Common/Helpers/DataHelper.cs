using System.Data.Common;

namespace JM.Integration.Methanol.Common.Helpers
{
    public static class DataHelper
    {
        public static double ConvertDoubleValue(DbDataReader reader, string propertyName)
        {
            object value = reader.GetValue(reader.GetOrdinal(propertyName));
            return (value is null || value.Equals(0)) ? (double)0 : reader.GetDouble(reader.GetOrdinal(propertyName));
        }

        public static string ConvertStringValue(DbDataReader reader, string propertyName)
        {
            object value = reader[propertyName];
            return (value is null) ? string.Empty : value.ToString();
        }
    }
}