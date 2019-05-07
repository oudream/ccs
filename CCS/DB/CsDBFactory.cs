using System;
using System.Runtime.InteropServices;

namespace CCS.DB
{
    public class CsDBFactory
    {
        public static CsIDBConnection InitFactory(string DBType, string ConnectionString, string Type = "OLEDB")
        {
            CsIDBConnection connection = null;
            string str = DBType.ToUpper().Trim();
            if (str == null)
            {
                return connection;
            }
            if (!(str == "ACCESS"))
            {
                if (str != "ORACLE")
                {
                    if (str == "ORACLE11G")
                    {
                        return new CsDBOracle11g(ConnectionString, Type);
                    }
                    if (str == "SQLSERVER")
                    {
                        return new CsDBSqlServer(ConnectionString, Type);
                    }
                    if (str == "MYSQL")
                    {
                        return new CsDBMySql(ConnectionString, Type);
                    }
                    if (str != "SQLLITE")
                    {
                        return connection;
                    }
                    return new CsDBSqlLite(ConnectionString, Type);
                }
            }
            else
            {
                return new CsDBOdbc(ConnectionString, Type);
            }
            return new CsDBOracle(ConnectionString, Type);
        }
    }
}

