using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Reflection;

namespace CCS
{
    public class CsUtils
    {
        /// <summary>
        /// <para>程序的单实例检查。</para>
        /// <para>返回 True：不存在单实例。</para>
        /// <para>返回 False：已存在单实例。</para>
        /// </summary>
        /// <param name="InstanceName">程序标签</param>
        /// <returns></returns>
        public static bool f_SingleInstanceCheckup(string InstanceName)
        {
            bool bResult = false;
            string GlobalName = string.Format(@"Global\{0}", InstanceName);
            Mutex m = new Mutex(true, GlobalName, out bResult);
            return bResult;
        }

        /// <summary>
        /// 数组切割。将"源数组"按"切割大小"进行切割，返回切割后的数组列表。
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="sizeCut">切割大小</param>
        /// <returns></returns>
        /// add by lzw at 2016/4/1
        public static List<T[]> f_ArrayCut<T>(T[] sourceArray, System.Int32 sizeCut)
        {
            List<T[]> _Group = new List<T[]>();
            System.Int32 _sourceArrayLength = sourceArray.Length;
            System.Int32 _cutCount = (_sourceArrayLength / sizeCut) + ((_sourceArrayLength % sizeCut == 0) ? 0 : 1);
            for (System.Int32 _index = 0; _index < _cutCount; _index++)
            {
                if (_index == _sourceArrayLength / sizeCut)
                {
                    T[] temp = new T[_sourceArrayLength % sizeCut];
                    Array.Copy(sourceArray, _index * sizeCut, temp, 0, temp.Length);
                    _Group.Add(temp);
                }
                else
                {
                    T[] temp = new T[sizeCut];
                    Array.Copy(sourceArray, _index * sizeCut, temp, 0, temp.Length);
                    _Group.Add(temp);
                }
            }
            return _Group;
        }
        /// <summary>
        /// 将字节数组转换成以特定分隔符分隔的16进制字符串
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <param name="Separator">字节间的分隔符</param>
        /// <returns>16进制字符串</returns>
        public static string f_ToHexString(byte[] Bytes, string Separator)
        {
            if (Bytes == null) return string.Empty;
            if (Bytes.Length == 0) return string.Empty;
            if (Separator == null) Separator = string.Empty;

            StringBuilder sbTemp = new StringBuilder(null);
            for (int index = 0; index < Bytes.Length; index++)
            {
                if (sbTemp.Length > 0) sbTemp.Append(Separator);
                sbTemp.Append(Bytes[index].ToString("X2"));
            }
            return sbTemp.ToString();
        }
        /// <summary>
        /// 将记录集转换为键值对列表。Key为列名，Value为列值
        /// </summary>
        /// <param name="data">数据集</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> f_ToListKvs(DataSet data)
        {
            if (data == null) return null;
            if (data.Tables.Count == 0) return null;
            if (data.Tables[0].Rows.Count == 0) return null;
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            List<string> columns = new List<string>();
            foreach (DataColumn dc in data.Tables[0].Columns) columns.Add(dc.ColumnName);
            foreach (DataRow dr in data.Tables[0].Rows)
            {
                Dictionary<string, string> record = new Dictionary<string, string>();
                foreach (string key in columns) record.Add(key, (dr[key] == null ? string.Empty : dr[key].ToString()));
                result.Add(record);
            }
            return result;
        }
        /// <summary>
        /// 时间格式字符串转换。"20160618120600123" 转 "2016-06-18 12:06:00.123"
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <returns></returns>
        public static string f_ToDateTimeString(string sDateTime)
        {
            if (string.IsNullOrEmpty(sDateTime)) return string.Empty;
            StringBuilder sb = new StringBuilder(null);
            if (sDateTime.Length >= 4) sb.Append(sDateTime.Substring(0, 4));
            if (sDateTime.Length >= 6) sb.Append("-" + sDateTime.Substring(4, 2));
            if (sDateTime.Length >= 8) sb.Append("-" + sDateTime.Substring(6, 2));
            if (sDateTime.Length >= 10) sb.Append(" " + sDateTime.Substring(8, 2));
            if (sDateTime.Length >= 12) sb.Append(":" + sDateTime.Substring(10, 2));
            if (sDateTime.Length >= 14) sb.Append(":" + sDateTime.Substring(12, 2));
            if (sDateTime.Length >= 17) sb.Append("." + sDateTime.Substring(14, 3));
            return sb.ToString();
        }
        public static T f_KvsToClass<T>(Dictionary<string, string> record, T result, Dictionary<string, string> map) where T : new()
        {
            if (record == null || result == null) return result;
            Dictionary<string, int> PropertyIndexMap = new Dictionary<string, int>();
            PropertyInfo[] arPros = result.GetType().GetProperties();
            for (int index = 0; index < arPros.Length; index++) PropertyIndexMap.Add(arPros[index].Name, index);
            foreach (string FieldName in record.Keys)
            {
                if (map.ContainsKey(FieldName) == false) continue;
                if (map[FieldName] == string.Empty) continue;
                if (PropertyIndexMap.ContainsKey(map[FieldName]) == false) continue;
                int proIndex = PropertyIndexMap[map[FieldName]];
                arPros[proIndex].SetValue(result, record[FieldName], null);
            }
            return result;
        }
        public static T f_KvsToClass<T>(Dictionary<string, string> record, T result) where T : new()
        {
            if (record == null || result == null) return result;
            Dictionary<string, int> PropertyIndexMap = new Dictionary<string, int>();
            PropertyInfo[] arPros = result.GetType().GetProperties();
            for (int index = 0; index < arPros.Length; index++) PropertyIndexMap.Add(arPros[index].Name, index);
            foreach (string FieldName in record.Keys)
            {
                if (PropertyIndexMap.ContainsKey(FieldName) == false) continue;
                int proIndex = PropertyIndexMap[FieldName];
                arPros[proIndex].SetValue(result, record[FieldName], null);
            }
            return result;
        }
        public static T f_KvsToClass<T>(Dictionary<string, object> record) where T : new()
        {
            if (record == null) return default(T);
            T tResult = new T();
            Dictionary<string, int> PropertyIndexMap = new Dictionary<string, int>();
            PropertyInfo[] arPros = tResult.GetType().GetProperties();
            for (int index = 0; index < arPros.Length; index++) 
                PropertyIndexMap.Add(arPros[index].Name, index);
            foreach (string FieldName in record.Keys)
            {
                if (PropertyIndexMap.ContainsKey(FieldName) == false) continue;
                int proIndex = PropertyIndexMap[FieldName];
                arPros[proIndex].SetValue(tResult, record[FieldName], null);
            }
            return tResult;
        }
    }
}
