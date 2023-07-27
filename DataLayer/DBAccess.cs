using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;



namespace DataLayer
{
    public class DBAccess
    {

        #region Identifiers Declaration ---------------------------
        private IDataReader iDataReader;
        private Dictionary<string, string> sqlParamValues;
        private List<string> sqlParamNames;
        private string queryId;
        private string queryType;
        private string connnectionString;

        #endregion

        #region Constructor ---------------------------------------

        public DBAccess()
        {
            connnectionString = ConfigurationManager.ConnectionStrings["server"].ConnectionString;
        }

        #endregion

        #region Public Properties ---------------------------------

        public string QueryId
        {
            get { return queryId; }
            set { sqlParamValues = null; sqlParamNames = null; queryId = value; }
        }

        public string QueryType
        {
            get { return queryType; }
            set { queryType = value; }
        }

        #endregion

        #region Execute Data Reader -------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataReader ExecuteDataReader()
        {
            try
            {
                SqlConnection dbConnection = new SqlConnection(connnectionString);
                SqlCommand dbCommand = GetDbCommand(dbConnection);

                dbConnection.Open();
                iDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iDataReader;
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataSet()
        {
            DataSet dataSet = new DataSet();
            try
            {

                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    SqlCommand dbCommand = GetDbCommand(dbConnection);
                    dbCommand.CommandTimeout = 0;
                    SqlDataAdapter dbDataAdapter = new SqlDataAdapter(dbCommand);
                    dbDataAdapter.Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }
        #endregion


        #region InlineExecuteScalar
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object InlineExecuteScalar(string sQry)
        {
            object scalarValue = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    // SqlCommand dbCommand = GetDbCommand(dbConnection);
                    SqlCommand dbCommand = new SqlCommand(sQry, dbConnection);
                    //dbCommand.CommandText = sQry;
                    dbCommand.CommandTimeout = 0;
                    dbConnection.Open();

                    scalarValue = dbCommand.ExecuteScalar();
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return scalarValue;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ExecuteScalar()
        {
            object scalarValue = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    SqlCommand dbCommand = GetDbCommand(dbConnection);
                    dbCommand.CommandTimeout = 0;
                    dbConnection.Open();
                    scalarValue = dbCommand.ExecuteScalar();
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return scalarValue;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            int recordsAffected = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    SqlCommand dbCommand = GetDbCommand(dbConnection);
                    dbCommand.CommandTimeout = 0;
                    dbConnection.Open();
                    recordsAffected = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordsAffected;
        }
        #endregion

        #region GetDbCommand
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlCommand GetDbCommand(SqlConnection dbConnection)
        {
            SqlCommand dbCommand = null;
            try
            {
                if (QueryId == string.Empty)
                    throw new Exception("QueryID is empty");

                dbCommand = new SqlCommand(QueryId, dbConnection);

                //if (QueryType == Constants.InlineQuery)
                //    dbCommand.CommandType = CommandType.Text;
                //else if (QueryType == Constants.StoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

                if (sqlParamNames != null)
                {
                    for (int paramsCount = 0; paramsCount < sqlParamNames.Count; paramsCount++)
                    {
                        string parameterName = sqlParamNames[paramsCount];
                        string parameterValue = sqlParamValues[parameterName];
                        SqlParameter dbParam = new SqlParameter(parameterName, parameterValue);
                        dbCommand.Parameters.Add(dbParam);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbCommand;
        }
        #endregion

        #region AddParameters
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        public void AddParameters(string paramName, string paramValue)
        {
            if (sqlParamValues == null || sqlParamNames == null)
            {
                sqlParamValues = new Dictionary<string, string>();
                sqlParamNames = new List<string>();
            }

            sqlParamNames.Add(paramName);
            sqlParamValues.Add(paramName, paramValue);
        }
        #endregion


        #region InlineExecuteDataSet
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet InlineExecuteDataSet(string sQry)
        {
            DataSet dataSet = new DataSet();
            try

            {
                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    SqlCommand dbCommand = GetDbCommand(dbConnection);
                    dbCommand.CommandTimeout = 0;
                    SqlDataAdapter dbDataAdapter = new SqlDataAdapter(sQry, dbConnection);
                    dbDataAdapter.Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataSet;
        }
        #endregion


        #region InlineExecuteNonQuery
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int InlineExecuteNonQuery(string sQry)
        {
            int recordsAffected = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connnectionString))
                {
                    SqlCommand dbCommand = new SqlCommand(sQry, dbConnection);
                    dbCommand.CommandTimeout = 0;
                    dbConnection.Open();
                    recordsAffected = dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordsAffected;
        }
        #endregion




    }
}
