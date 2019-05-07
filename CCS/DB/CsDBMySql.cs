using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CCS.DB
{
    public class CsDBMySql : CsIDBConnection
    {
        private Exception exmsg = null;
        private MySqlDataAdapter mysqlAd = null;
        private MySqlCommand mysqlCmd = null;
        private MySqlConnection mysqlCon = null;
        private MySqlDataReader mysqldr = null;
        private object thislock = new object();

        public CsDBMySql(string constring, string ConType)
        {
            try
            {
                this.mysqlCon = new MySqlConnection(constring);
                this.mysqlCon.Open();
            }
            catch (Exception exception)
            {
                this.SetExceptionMessage(exception);
                CsInterinfo.OutInfoPrompt("连接mysql出现异常:" + exception.Message);
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
                        this.mysqlCmd = new MySqlCommand(sql, this.mysqlCon);
                        return this.mysqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("执行mysql出现异常" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        this.mysqlCon.Close();
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
                    MySqlTransaction transaction = this.mysqlCon.BeginTransaction();
                    this.mysqlCmd = new MySqlCommand();
                    this.mysqlCmd.Connection = this.mysqlCon;
                    this.mysqlCmd.Transaction = transaction;
                    try
                    {
                        for (int i = 0; i < sqls.Length; i++)
                        {
                            this.mysqlCmd.CommandText = sqls[i];
                            this.mysqlCmd.ExecuteNonQuery();
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
                DataSet dataSet = new DataSet();
                if (this.IsOpen())
                {
                    try
                    {
                        this.mysqlAd = new MySqlDataAdapter(sql, this.mysqlCon);
                        this.mysqlAd.Fill(dataSet);
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("获取mysql数据集时出现异常" + exception.Message);
                    }
                    finally
                    {
                        this.mysqlCon.Close();
                    }
                }
                else
                {
                    return null;
                }
                return dataSet;
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
                        this.mysqlCmd = new MySqlCommand(sql, this.mysqlCon);
                        this.mysqldr = this.mysqlCmd.ExecuteReader();
                        if (this.mysqldr.Read())
                        {
                            str = this.mysqldr.GetString(col);
                        }
                        this.mysqldr.Close();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("mysql使用ExecuteReader读取数据失败!" + exception.Message);
                    }
                    return str;
                }
                finally
                {
                    this.mysqlCon.Close();
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
            if (this.mysqlCon.State == ConnectionState.Closed)
            {
                try
                {
                    this.mysqlCon.Open();
                    return true;
                }
                catch (Exception exception)
                {
                    this.SetExceptionMessage(exception);
                    CsInterinfo.OutInfoPrompt("打开数据库mysql失败:" + exception.Message);
                    return false;
                }
            }
            return true;
        }

        public void SetExceptionMessage(Exception ex)
        {
            this.exmsg = ex;
        }
    }
}

