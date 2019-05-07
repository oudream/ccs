using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace CCS.DB
{
    public class CsDBOracle11g : CsIDBConnection
    {
        private Exception exmsg = null;
        private OracleCommand oraclcmd = null;
        private OracleDataAdapter oracleAd = null;
        private OracleConnection oracleCon = null;
        private OracleDataReader oracledr = null;
        private object thislock = new object();

        public CsDBOracle11g(string constring, string ConType)
        {
            try
            {
                this.oracleCon = new OracleConnection(constring);
                this.oracleCon.Open();
            }
            catch (Exception exception)
            {
                this.SetExceptionMessage(exception);
                CsInterinfo.OutInfoPrompt("连接oracle出现异常:" + exception.Message);
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
                        this.oraclcmd = new OracleCommand(sql, this.oracleCon);
                        return this.oraclcmd.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("执行oracle出现异常" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        this.oracleCon.Close();
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
                    OracleTransaction transaction = this.oracleCon.BeginTransaction();
                    this.oraclcmd = new OracleCommand();
                    this.oraclcmd.Connection = this.oracleCon;
                    this.oraclcmd.Transaction = transaction;
                    try
                    {
                        for (int i = 0; i < sqls.Length; i++)
                        {
                            this.oraclcmd.CommandText = sqls[i];
                            this.oraclcmd.ExecuteNonQuery();
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
                        this.oracleAd = new OracleDataAdapter(sql, this.oracleCon);
                        this.oracleAd.Fill(dataSet);
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("获取oracle数据集时出现异常" + exception.Message);
                    }
                    finally
                    {
                        this.oracleCon.Close();
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
                        this.oraclcmd = new OracleCommand(sql, this.oracleCon);
                        this.oracledr = this.oraclcmd.ExecuteReader();
                        if (this.oracledr.Read())
                        {
                            str = this.oracledr.GetString(col);
                        }
                        this.oracledr.Close();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("oracle11g使用ExecuteReader读取数据失败!" + exception.Message);
                    }
                    return str;
                }
                finally
                {
                    this.oracleCon.Close();
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
            if (this.oracleCon.State == ConnectionState.Closed)
            {
                try
                {
                    this.oracleCon.Open();
                    return true;
                }
                catch (Exception exception)
                {
                    this.SetExceptionMessage(exception);
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
    }
}

