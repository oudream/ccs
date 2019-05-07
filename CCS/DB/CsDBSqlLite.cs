using System;
using System.Data;
using System.Data.SQLite;

namespace CCS.DB
{
    public class CsDBSqlLite : CsIDBConnection
    {
        private Exception exmsg = null;
        private SQLiteDataAdapter SQLiteAd = null;
        private SQLiteCommand SQLiteCmd = null;
        private SQLiteConnection SQLiteCon = null;
        private SQLiteDataReader SQLitedr = null;
        private object thislock = new object();

        public CsDBSqlLite(string constring, string ConType)
        {
            try
            {
                this.SQLiteCon = new SQLiteConnection(constring);
                this.SQLiteCon.Open();
            }
            catch (Exception exception)
            {
                this.SetExceptionMessage(exception);
                CsInterinfo.OutInfoPrompt("连接SQLite出现异常:" + exception.Message);
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
                        this.SQLiteCmd = new SQLiteCommand(sql, this.SQLiteCon);
                        return this.SQLiteCmd.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("执行SQLite出现异常" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        this.SQLiteCon.Close();
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
                    SQLiteTransaction transaction = this.SQLiteCon.BeginTransaction();
                    this.SQLiteCmd = new SQLiteCommand();
                    this.SQLiteCmd.Connection = this.SQLiteCon;
                    this.SQLiteCmd.Transaction = transaction;
                    try
                    {
                        for (int i = 0; i < sqls.Length; i++)
                        {
                            this.SQLiteCmd.CommandText = sqls[i];
                            this.SQLiteCmd.ExecuteNonQuery();
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
                        this.SQLiteAd = new SQLiteDataAdapter(sql, this.SQLiteCon);
                        this.SQLiteAd.Fill(dataSet);
                    }
                    catch (Exception exception)
                    {
                        this.SetExceptionMessage(exception);
                        CsInterinfo.OutInfoPrompt("获取SQLite数据集时出现异常" + exception.Message);
                    }
                    finally
                    {
                        this.SQLiteCon.Close();
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
                        this.SQLiteCmd = new SQLiteCommand(sql, this.SQLiteCon);
                        this.SQLitedr = this.SQLiteCmd.ExecuteReader();
                        if (this.SQLitedr.Read())
                        {
                            str = this.SQLitedr.GetString(col);
                        }
                        this.SQLitedr.Close();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("SQLite使用ExecuteReader读取数据失败!" + exception.Message);
                    }
                    return str;
                }
                finally
                {
                    this.SQLiteCon.Close();
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
            if (this.SQLiteCon.State == ConnectionState.Closed)
            {
                try
                {
                    this.SQLiteCon.Open();
                    return true;
                }
                catch (Exception exception)
                {
                    this.SetExceptionMessage(exception);
                    CsInterinfo.OutInfoPrompt("打开数据库SQLite失败:" + exception.Message);
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

