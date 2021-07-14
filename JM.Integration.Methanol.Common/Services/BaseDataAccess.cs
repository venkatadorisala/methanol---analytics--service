using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Common.Constants;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JM.Integration.Common.Services
{
    /// <inheritdoc cref="IBaseDataAccess"/>
    public class BaseDataAccess : IBaseDataAccess
    {
        private string _ConnectionString;

        public BaseDataAccess(string connectionString)
        {
            _ConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _ConnectionString;
            connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net").Result;
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        public SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value ?? DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            return parameterObject;
        }

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }
            return parameterObject;
        }

        public int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;
            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Constants.ErrorMessage, nameof(GetDataReader), procedureName, ex.Message));
            }
            return returnValue;
        }

        public object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;
            try
            {
                using (DbConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, CommandType.StoredProcedure);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Constants.ErrorMessage, nameof(GetDataReader), procedureName, ex.Message));
            }
            return returnValue;
        }

        public DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds = null;
            try
            {
                SqlConnection connection = this.GetConnection();
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    ds = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(Constants.ErrorMessage, nameof(GetDataReader), procedureName, ex.Message));
            }
            return ds;
        }
    }
}