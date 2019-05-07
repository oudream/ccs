using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCS
{
    /// <summary>
    /// 全局缓存区
    /// <para>每个缓存数据以唯一标签标识，缓存区不会自动清理数据，无用数据需自行清理。</para>
    /// </summary>
    public class CsGlobalCache
    {
        /// <summary>
        /// 清空缓存区
        /// </summary>
        public static void f_Clear()
        {
            lock(_v_sync) { _v_cache.Clear(); }
        }
        /// <summary>
        /// 检查缓存区内是否存在指定标签的缓存数据
        /// </summary>
        /// <param name="_Tag">标签</param>
        /// <returns>是否已存在</returns>
        public static bool f_Exist(string _Tag)
        {
            if (string.IsNullOrEmpty(_Tag)) return false;
            return _v_cache.ContainsKey(_Tag);
        }
        /// <summary>
        /// 从缓存区中读取指定标签的缓存数据
        /// </summary>
        /// <param name="_Tag">标签</param>
        /// <returns>缓存数据</returns>
        public static object f_GetData(string _Tag)
        {
            if (f_Exist(_Tag) == false) return null;
            return _v_cache[_Tag];
        }
        /// <summary>
        /// 设置缓存区中指定标签的缓存数据
        /// </summary>
        /// <param name="_Tag">标签</param>
        /// <param name="_Data">缓存数据</param>
        public static bool f_SetData(string _Tag, object _Data)
        {
            if (!f_Exist(_Tag)) return false;
            lock (_v_sync) { _v_cache.Add(_Tag, _Data); }
            return true;
        }
        /// <summary>
        /// 向缓存区中添加缓存数据
        /// </summary>
        /// <param name="_Tag">标签</param>
        /// <param name="_Data">缓存数据</param>
        public static bool f_AddData(string _Tag, object _Data)
        {
            if (f_Exist(_Tag)) return false;
            lock (_v_sync) { _v_cache.Add(_Tag, _Data); }
            return true;
        }
        /// <summary>
        /// 移除缓存区中指定标签的缓存数据
        /// </summary>
        /// <param name="_Tag">标签</param>
        public static void f_RemoveData(string _Tag)
        {
            if (f_Exist(_Tag)) lock (_v_sync) { _v_cache.Remove(_Tag); }
        }
        /// <summary>
        /// 提取缓存区中的所有标签
        /// </summary>
        /// <returns>标签列表</returns>
        public static List<string> f_GetTags()
        {
            return _v_cache.Keys.ToList<string>();
        }

        /// <summary>
        /// 静态缓存区，KEY=标签，VALUE=标签数据
        /// </summary>
        private static Dictionary<string, object> _v_cache = new Dictionary<string, object>();
        /// <summary>
        /// 同步锁对象，屏蔽同时多个写操作
        /// </summary>
        private readonly static object _v_sync = new object(); 
    }
}
