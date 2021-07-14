using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JM.Integration.Common.Interfaces
{
    public interface IBaseDataAccess
    {
        public int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);

        public object ExecuteScalar(string procedureName, List<SqlParameter> parameters);

        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType);

        public DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);

        public SqlParameter GetParameter(string parameter, object value);

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput);

        public SqlConnection GetConnection();
    }
}