using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using CCS;

namespace CCS.DB
{
    public class CsDBOdbc : CsIDBConnection
    {
        private Exception exmsg = null;
        private OdbcConnection odbccon = null;
        private OleDbConnection olecon = null;
        private object thislock = new object();

        public CsDBOdbc(string ConString, string ConType)
        {
            try
            {
                this.dbCon = ConString;
                this.odbcType = ConType;
                if (ConType.ToUpper().Equals("ODBC"))
                {
                    this.odbccon = new OdbcConnection(ConString);
                    this.odbccon.Open();
                }
                else
                {
                    this.olecon = new OleDbConnection(ConString);
                    this.olecon.Open();
                }
            }
            catch (Exception exception)
            {
                this.SetExceptionMessage(exception);
                CsInterinfo.OutInfoPrompt("初始化access数据库失败:" + exception.Message);
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
                        OleDbCommand command = new OleDbCommand(sql, this.olecon);
                       
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("ole方式执行sql" + sql + "出错:" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        if (this.olecon.State == ConnectionState.Open)
                            this.olecon.Close();
                    }

                }
                return 0;
            }
        }

        public bool ExcuteDataForManySql(string[] sqls)
        {
            return true;
        }

        public int ExcuteDataForODBC(string sql)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        OdbcCommand command = new OdbcCommand(sql, this.odbccon);
                        //if (this.olecon.State == ConnectionState.Open)
                         //   this.olecon.Close();
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("odbc方式执行sql" + sql + "出错:" + exception.Message);
                        return 0;
                    }
                    finally
                    {
                        if (this.olecon.State == ConnectionState.Open)
                            this.olecon.Close();
                    }
                }
                return 0;
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
                        OleDbCommand selectCommand = new OleDbCommand(sql, this.olecon);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                       // if (this.olecon.State == ConnectionState.Open)
                         //   this.olecon.Close();
                        return dataSet;
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("ole方式执行sql" + sql + "出错:" + exception.Message);
                        return null;
                    }
                    finally
                    {
                        if (this.olecon.State == ConnectionState.Open)
                            this.olecon.Close();
                    }
                }
                return null;
            }
        }

        public object GetDataFirst(string sql, int col)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        OleDbCommand command = new OleDbCommand(sql, this.olecon);
                        return command.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("ole方式执行sql" + sql + "出错:" + exception.Message);
                        return null;
                    }
                    finally
                    {
                        if (this.olecon.State == ConnectionState.Open)
                            this.olecon.Close();
                    }
                }
                return null;
            }
        }

        public DataSet GetDataForODBC(string sql)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        OdbcCommand selectCommand = new OdbcCommand(sql, this.odbccon);
                        OdbcDataAdapter adapter = new OdbcDataAdapter(selectCommand);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        //if (this.olecon.State == ConnectionState.Open)
                          //  this.olecon.Close();
                        return dataSet;
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("odbc方式执行sql" + sql + "出错:" + exception.Message);
                        return null;
                    }
                    finally
                    {
                        this.odbccon.Close();
                    }
                }
                return null;
            }
        }

        public object GetDataForODBCFirst(string sql)
        {
            lock (this.thislock)
            {
                if (this.IsOpen())
                {
                    try
                    {
                        OdbcCommand command = new OdbcCommand(sql, this.odbccon);
                        //if (this.olecon.State == ConnectionState.Open)
                          //  this.olecon.Close();
                        return command.ExecuteScalar();
                    }
                    catch (Exception exception)
                    {
                        CsInterinfo.OutInfoPrompt("odbc方式执行sql" + sql + "出错:" + exception.Message);
                        return null;
                    }
                    finally
                    {
                        this.odbccon.Close();
                    }
                }
                return null;
            }
        }

        public Exception GetExceptionMessage()
        {
            return this.exmsg;
        }

        private bool IsOpen()
        {
            if (this.olecon.State == ConnectionState.Closed)
            {
                try
                {
                    if (this.odbcType.ToUpper().Equals("ODBC"))
                    {
                        this.odbccon.Open();
                    }
                    else
                    {
                        this.olecon.Open();
                    }
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

        private string odbcType { get; set; }
    }
}

