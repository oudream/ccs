using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCS.DB
{
    public class CsDBBase
    {
        /// <summary>
        /// 获取数据集通过SQL语句
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="connection">连接字符串</param>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static DataSet GetDataSetBySQl(string dbtype, string connection, string sql)
        {
            CsIDBConnection Idb = CsDBFactory.InitFactory(dbtype, connection);
            DataSet ds = Idb.GetData(sql);
            return ds;

        }

        public static DataSet GetDataSetBySQl(string dbtype, string connection, string sql, string type)
        {
            CsIDBConnection Idb = CsDBFactory.InitFactory(dbtype, connection, type);
            DataSet ds = Idb.GetData(sql);
            return ds;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcuteSQl(string dbtype, string connection, string sql)
        {
            CsIDBConnection Idb = CsDBFactory.InitFactory(dbtype, connection);
            int i = Idb.ExcuteData(sql);
            return i;
        }

        /// <summary>
        /// 批量执行SQL语句,支持事务回滚
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="connection"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static bool ExcuteSQlByManaySql(string dbtype, string connection, string[] sqls)
        {
            CsIDBConnection Idb = CsDBFactory.InitFactory(dbtype, connection);
            return Idb.ExcuteDataForManySql(sqls);
        }

    }
}
