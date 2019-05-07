using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCS.DB
{
    public class CsDBManager
    {
        private static CsIDBConnection _defaultDb = null;

        private static string _defaultConn;

        /// <summary>
        /// 获取默认数据连接
        /// </summary>
        /// <param name="sDBType">数据库类型</param>
        /// <param name="sConn">连接字符串</param>
        /// <returns></returns>
        public static CsIDBConnection defaultDb(string sDBType, string sConn, string sConnType = "OLEDB")
        {
            if (_defaultConn == null || !_defaultConn.Equals(sConn))
            {
                _defaultDb = CsDBFactory.InitFactory(sDBType, sConn, sConnType);
                _defaultConn = sConn;
            }
            return _defaultDb;
        }

        public static CsIDBConnection defaultDb()
        {
            return _defaultDb;
        }

        /// <summary>
        /// 获取数据集通过SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static DataSet GetDataSetBySQl(string sql)
        {
            DataSet ds = _defaultDb.GetData(sql);
            return ds;

        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcuteSQl(string sql)
        {
            int i = _defaultDb.ExcuteData(sql);
            return i;
        }

        /// <summary>
        /// 批量执行SQL语句,支持事务回滚
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static bool ExcuteSQlByManaySql(string[] sqls)
        {
            return _defaultDb.ExcuteDataForManySql(sqls);
        }
    }
}
