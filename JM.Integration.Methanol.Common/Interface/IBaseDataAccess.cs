using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JM.Integration.Common.Interfaces
{
    /// <summary>
    /// Contains methods for data access implementation
    /// </summary>
    public interface IBaseDataAccess
    {
        /// <summary>
        /// Executes SqlCommand against connection object
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected</returns>
        int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Executes the SqlCommand to return first column of first row as result set
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns first column of first row as result set</returns>
        object ExecuteScalar(string procedureName, List<SqlParameter> parameters);

        /// <summary>
        /// Builds SqlCommand to execute
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns>Returns DbCommand object with command to be executed</returns>
        DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType);

        /// <summary>
        /// Executes SqlCommand to obtain stream of rows
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns>Returns the stream of rows through DbDataReader object</returns>
        DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Builds SqlParamter object to be used as input along the query
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns>Returns SqlParameter object to be used along with SQL Command</returns>
        SqlParameter GetParameter(string parameter, object value);

        /// <summary>
        /// Build SqlParameter object to be used for input or output along with query
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="parameterDirection"></param>
        /// <returns>Returns SqlParameter object to be used along with SQL Command</returns>
        SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput);

        /// <summary>
        /// Generates SqlConnection Object to allow successful SQL connection
        /// </summary>
        /// <returns>Returns SqlConnection object to invoke SqlCommand</returns>
        SqlConnection GetConnection();
    }
}