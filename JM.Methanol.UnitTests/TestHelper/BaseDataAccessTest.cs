using JM.Integration.Common.Interfaces;
using JM.Integration.Common.Services;
using System;
using System.Data.SqlClient;
using Xunit;
using JM.Integration.Common.TestHelper.Constants;

namespace JM.Methanol.UnitTests.TestHelper
{
    public class BaseDataAccessTest
    {
        private readonly IBaseDataAccess _baseDataAccess;
        private readonly string commandText;

        public BaseDataAccessTest()
        {
            this._baseDataAccess = new BaseDataAccess(CommonConstant.ConnectionString);
            this.commandText = "StoredProc";
        }

        [Fact]
        public void GetCommand_ForValidRequest_ReturnValidCommand()
        {
           

            SqlConnection connection = new SqlConnection();
            var command = this._baseDataAccess.GetCommand(connection, commandText, System.Data.CommandType.StoredProcedure);
            Assert.Equal(command.CommandText, commandText);
            Assert.Equal(command.CommandType, System.Data.CommandType.StoredProcedure);
        }

        [Fact]
        public void GetParameter()
        {
            string paramName = "test";
            string paramValue = "test";
            var param = this._baseDataAccess.GetParameter(paramName, paramValue);
            Assert.Equal(param.ParameterName, paramName);
            Assert.Equal(param.Value, paramValue);
        }

        [Fact]
        public void GetParameterOut_ForDBNullValue_ReturnParamwithNullValue()
        {
            string paramName = "test";
            string paramValue = null;
            var parameterObject = this._baseDataAccess.GetParameterOut(paramName, System.Data.SqlDbType.VarChar);
            Assert.Equal(parameterObject.ParameterName, paramName);
            Assert.Equal(parameterObject.Value, DBNull.Value);
        }
    }
}