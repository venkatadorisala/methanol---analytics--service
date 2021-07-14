using JM.Integration.Common.Interfaces;
using JM.Integration.Methanol.Services.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JM.Methanol.UnitTests.TestHelper
{
    public class MockBaseDataAccess : IBaseDataAccess
    {
        public int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return 1;
        }

        public object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            BaseKpiResponse obj = new BaseKpiResponse();
            return obj;
        }

        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            return connection;
        }

        public DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds = null;
            return ds;
        }

        public SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value ?? DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            return parameterObject;
        }

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            throw new NotImplementedException();
        }
    }
}