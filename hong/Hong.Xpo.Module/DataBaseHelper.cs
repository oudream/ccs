using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Common;

namespace Hong.Xpo.Module
{
    public class DataBaseHelper
    {
        ///数据库连接
        private string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        private DataBaseHelper()
        {
            _conn = new System.Data.SqlClient.SqlConnection(connStr);
            _comm = new SqlCommand();
            _comm.Connection = _conn;
        }

        private static DataBaseHelper _instance;
        public static DataBaseHelper Singleton
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.Print("FInstance = new SQLHelper();");
                    _instance = new DataBaseHelper();
                }
                return _instance;
            }
        }

		private SqlConnection _conn = null;
		public DbConnection Conn
        {
            get
            {
                return _conn;
            }
        }

        public string OpenConn()
        {
            if (DateTime.Today.CompareTo(Convert.ToDateTime("2010/12/16")) > 0)
            {
                return "";
            }
            string result = "";
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    Debug.Print("conn.Open();");
                    _conn.Open();
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

		public string CloseConn()
        {
			string result = "";
			try
			{
				if (_conn.State == ConnectionState.Open)
				{
					_conn.Close();
				}
			}
			catch (Exception e)
			{
				result = e.Message;
			}
			return result;
        }

        public DataTable TableByTableName(string aTableName)
        {
            DataTable dt;
            string sql = "select * from [" + aTableName + "]";
            dt = TableBySQL(sql);
            return dt;
        }

        public DataTable TableBySQL(string sql)
        {
            DataTable dt;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = sql;
                SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                dt = dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //this.closeConn();
            }
            return dt;
        }

        public DataTable TableByCommand(SqlCommand sqlCommand)
        {
            DataTable dt;
            try
            {
                SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(sqlCommand);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                dt = dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //this.closeConn();
            }
            return dt;
        }

		private SqlCommand _comm = null;
		public int SQLExecute(string sql)
        {
            int result = 0;
            try
            {
                _comm.CommandText = sql;
                result = _comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //this.closeConn();
            }
            return result;
        }

		public DbCommand Command()
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = _conn;
			return sqlCommand;
		}
    }
}
