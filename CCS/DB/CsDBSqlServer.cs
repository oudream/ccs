using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace CCS.DB
{
    public class CsDBSqlServer : CsIDBConnection
    {
        private Exception exmsg = null;
        private SqlDataAdapter sqlad = null;
        private SqlCommand sqlcommand = null;
        private SqlConnection sqlconnection = null;
        private SqlDataReader sqldr = null;
        private object thislock = new object();

        public CsDBSqlServer(string ConString, string ConType)
        {
            this.dbCon = ConString;
            try
            {
                this.sqlconnection = new SqlConnection(ConString);
                this.sqlconnection.Open();
                CsInterinfo.OutInfoPrompt("SQLserver数据连接状态：" + this.sqlconnection.State);
            }
            catch (Exception exception)
            {
                CsInterinfo.OutInfoPrompt("初始化SQLserver数据库失败:" + exception.Message);
            }
        }

        public int ExcuteData(string sql)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        this.sqlcommand = new SqlCommand(sql, this.sqlconnection);
                        return this.sqlcommand.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("执行sql:" + sql + "出错" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        this.sqlconnection.Close();
                    }
                }
                return 0;
            }
        }

        public bool ExcuteDataForManySql(string[] sqls)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    SqlTransaction transaction = this.sqlconnection.BeginTransaction();
                    this.sqlcommand = new SqlCommand();
                    this.sqlcommand.Connection = this.sqlconnection;
                    this.sqlcommand.Transaction = transaction;
                    try
                    {
                        for (int i = 0; i < sqls.Length; i++)
                        {
                            this.sqlcommand.CommandText = sqls[i];
                            this.sqlcommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("执行批量SQL出错,异常原因:" + exception.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
                else
                {
                    CsInterinfo.OutInfoPrompt("执行批量SQL出错,异常原因:SQLServer数据库打开失败!");
                    return false;
                }
            }
        }

        public DataSet GetData(string sql)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        this.sqlad = new SqlDataAdapter(sql, this.sqlconnection);
                        DataSet dataSet = new DataSet();
                        this.sqlad.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("执行sql:" + sql + "出错" + exception.Message);
                        return null;
                    }
                    finally
                    {
                        this.sqlconnection.Close();
                    }
                }
                return null;
            }
        }

        public object GetDataFirst(string sql, int col)
        {
            string str = string.Empty;
            lock (this.thislock)
            {
                if (!this.IsOpen())
                {
                    return str;
                }
                try
                {
                    try
                    {
                        this.sqlcommand = new SqlCommand(sql, this.sqlconnection);
                        this.sqldr = this.sqlcommand.ExecuteReader();
                        if (this.sqldr.Read())
                        {
                            str = this.sqldr.GetString(col);
                        }
                        this.sqldr.Close();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("sqlserver使用ExecuteReader读取数据失败!" + exception.Message);
                    }
                    return str;
                }
                finally
                {
                    this.sqlconnection.Close();
                }
            }
            return str;
        }

        public Exception GetExceptionMessage()
        {
            return this.exmsg;
        }

        private bool IsOpen()
        {
            if (this.sqlconnection.State == ConnectionState.Closed)
            {
                try
                {
                    this.sqlconnection.Open();
                    return true;
                }
                catch (Exception exception)
                {
                    CsInterinfo.OutInfoPrompt("打开数据库失败:" + exception.Message);
                    return false;
                }
            }
            return true;
        }

        public void SetExceptionMessage(Exception ex)
        {
            this.exmsg = ex;
        }

        private string dbCon { get; set; }
    }
}

