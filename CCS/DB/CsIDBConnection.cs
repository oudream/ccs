using System;
using System.Data;

namespace CCS.DB
{
    public interface CsIDBConnection
    {
        int ExcuteData(string sql);
        bool ExcuteDataForManySql(string[] sqls);
        DataSet GetData(string sql);
        object GetDataFirst(string sql, int col);
        Exception GetExceptionMessage();
        void SetExceptionMessage(Exception ex);
    }
}

