using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
namespace Vpa
{
    /// <summary>
    /// Vpa.Xml
    /// </summary>
    public class xXml
    {
        // public m_, f_
        // private _m_, _f_

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="_XmlStream">包含对象的XML字符串</param>
        /// <param name="_Encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        /// using System.Xml.Serialization;
        /// using System.IO;
        public static T f_XmlDeserialize<T>(string _XmlStream, Encoding _Encoding)
        {
            T Result = default(T);
            if (string.IsNullOrEmpty(_XmlStream) == false
                && _Encoding != null)
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(_Encoding.GetBytes(_XmlStream)))
                {
                    using (StreamReader sr = new StreamReader(ms, _Encoding))
                    {
                        try
                        {
                            object obj = mySerializer.Deserialize(sr);
                            if (obj != null) Result = (T)obj;
                        }
                        catch (Exception ex) { ; }
                    }
                }
            }
            return Result;
        }
        /// <summary>
        /// 将对象序列化XML字符串
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="_Encoding">编码方式</param>
        /// <returns>序列化后的XML字符串</returns>
        public static string f_XmlSerialize(object o, Encoding _Encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _f_XmlSerializeInternal(stream, o, _Encoding);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, _Encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="o"></param>
        /// <param name="_Encoding"></param>
        private static void _f_XmlSerializeInternal(Stream stream, object o, Encoding _Encoding)
        {
            if (o == null) throw new ArgumentNullException("o");
            if (_Encoding == null) throw new ArgumentNullException("_Encoding");
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = _Encoding;
            settings.IndentChars = "    ";
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }
        /// <summary>
        /// 检查文件是否可以正确加载
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <returns>返回码</returns>
        public static bool f_CheckFile(string _XmlFile)
        {
            bool Result = false;
            if (_XmlFile == null || _XmlFile == string.Empty) return Result;
            if (File.Exists(_XmlFile) == false) return Result;
            XmlDocument doc = new XmlDocument();
            try { doc.Load(_XmlFile); }
            catch (Exception ex) { return Result; }
            Result = true;
            return Result;
        }
        /// <summary>
        /// 浏览XML文件，并返回所有元素全路径名的列表
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <returns>全路径名的列表</returns>
        public static List<string> f_BrowseXml(string _XmlFile)
        {
            List<string> Result = null;
            bool bResult = f_CheckFile(_XmlFile);
            if (bResult == false) return Result;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_XmlFile);
                Result = _f_BrowseElement(string.Empty, doc.DocumentElement);
            }
            catch (Exception ex)
            {
                Result = null;
            }
            return Result;
        }

        public static List<string> f_BrowseXmlStream(string _XmlStream)
        {
            List<string> Result = null;
            if (string.IsNullOrEmpty(_XmlStream)) return Result;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_XmlStream);
                Result = _f_BrowseElement(string.Empty, doc.DocumentElement);
            }
            catch (Exception ex)
            {
                Result = null;
            }
            return Result;
        }
        /// <summary>
        /// 浏览元素，返回元素及元素内所有子元素的全路径名列表
        /// </summary>
        /// <param name="sParent">元素前路径</param>
        /// <param name="xe">元素</param>
        /// <returns>全路径名列表</returns>
        private static List<string> _f_BrowseElement(string sParent, XmlElement xe)
        {
            List<string> Result = new List<string>();
            string tParent = string.Format("{0}{1}/", sParent, xe.Name);
            Result.Add(tParent);
            if (xe.ChildNodes.Count > 0)
            {
                foreach (XmlNode xn in xe.ChildNodes)
                {
                    XmlElement xee = xn as XmlElement;
                    if (xee == null) continue;
                    List<string> tlist = _f_BrowseElement(tParent, xee);
                    Result.AddRange(tlist);
                }
            }
            return Result;
        }
        /// <summary>
        /// 确定指定的节点是否存在。
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <returns></returns>
        public static bool f_Exists(string _XmlFile, string _NodePathName)
        {
            bool Result = false;

            return Result;
        }
        /// <summary>
        /// 确定指定的节点是否存在。扩展条件：指定属性名及属性值。
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <param name="_AttributeName">[in]属性名</param>
        /// <param name="_AttributeValue">[in]属性值</param>
        /// <returns></returns>
        public static bool f_Exists(string _XmlFile, string _NodePathName, string _AttributeName, string _AttributeValue)
        {
            bool Result = false;

            return Result;
        }
        public static string f_Get_Node_InnerXml(string _XmlFile, string _NodePathName)
        {
            string Result = null;
            bool bResult = f_CheckFile(_XmlFile);
            if (bResult == false) return Result;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_XmlFile);
                XmlNode xnResult = doc.SelectSingleNode(_NodePathName);
                Result = (xnResult == null) ? null : xnResult.InnerXml;
            }
            catch (Exception ex)
            {
                Result = null;
            }
            return Result;
        }
        /// <summary>
        /// 提取指定节点的内容
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <returns>节点内容</returns>
        public static string f_Get_Node_InnerText(string _XmlFile, string _NodePathName)
        {
            string Result = null;
            bool bResult = f_CheckFile(_XmlFile);
            if (bResult == false) return Result;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_XmlFile);
                XmlNode xnResult = doc.SelectSingleNode(_NodePathName);
                Result = (xnResult == null) ? null : xnResult.InnerText;
            }
            catch (Exception ex)
            {
                Result = null;
            }
            return Result;
        }
        public static string f_Get_Stream_Node_InnerText(string _XmlStream, string _NodePathName)
        {
            string Result = null;
            if (string.IsNullOrEmpty(_XmlStream)) return Result;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_XmlStream);
                XmlNode xnResult = doc.SelectSingleNode(_NodePathName);
                Result = (xnResult == null) ? null : xnResult.InnerText;
            }
            catch (Exception ex)
            {
                Result = null;
            }
            return Result;
        }
        /// <summary>
        /// 设置指定节点的内容
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <param name="_InnerText">[in]节点内容</param>
        public static bool f_Set_Node_InnerText(string _XmlFile, string _NodePathName, string _InnerText)
        {
            bool Result = false;
            bool bResult = f_CheckFile(_XmlFile);
            if (bResult == false) return Result;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_XmlFile);
                XmlNode xnResult = doc.SelectSingleNode(_NodePathName);
                if (xnResult != null)
                {
                    xnResult.InnerText = _InnerText;
                    doc.Save(_XmlFile);
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }
        /// <summary>
        /// 提取指定节点的属性值
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <param name="_AttributeName">[in]属性名</param>
        /// <returns>节点属性值</returns>
        public static string f_Get_Node_AttributeValue(string _XmlFile, string _NodePathName, string _AttributeName)
        {
            string strResult = string.Empty;
            if (_XmlFile == null || _XmlFile == string.Empty) return strResult;
            if (File.Exists(_XmlFile) == false) return strResult;
            XmlDocument doc = new XmlDocument();
            try { doc.Load(_XmlFile); }
            catch (Exception ex) { return strResult; }
            XmlNode xnResult = null;
            try { xnResult = doc.SelectSingleNode(_NodePathName); }
            catch (Exception ex) { return strResult; }
            if (xnResult == null) return strResult;
            foreach (XmlAttribute xa in xnResult.Attributes)
            {
                if (xa.Name == _AttributeName) strResult = xa.Value;
            }
            return strResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName"></param>
        /// <param name="_AttributeName"></param>
        /// <param name="_AttributeValue"></param>
        /// <returns></returns>
        public static bool f_Set_Node_AttributeValue(string _XmlFile, string _NodePathName, string _AttributeName, string _AttributeValue)
        {
            bool bResult = false;
            if (_XmlFile == null || _XmlFile == string.Empty) return bResult;
            if (File.Exists(_XmlFile) == false) return bResult;
            XmlDocument doc = new XmlDocument();
            try { doc.Load(_XmlFile); }
            catch (Exception ex) { return bResult; }
            XmlNode xnResult = null;
            try { xnResult = doc.SelectSingleNode(_NodePathName); }
            catch (Exception ex) { return bResult; }
            if (xnResult == null) return bResult;
            foreach (XmlAttribute xa in xnResult.Attributes)
            {
                if (xa.Name == _AttributeName)
                {
                    xa.Value = _AttributeValue;
                    doc.Save(_XmlFile);
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }
        /// <summary>
        /// 提取指定节点
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <returns>单个节点</returns>
        public static xNode f_Get_SingleNode(string _XmlFile, string _NodePathName)
        {
            xNode nodeResult = null;
            if (_XmlFile == null || _XmlFile == string.Empty) return nodeResult;
            if (File.Exists(_XmlFile) == false) return nodeResult;
            XmlDocument doc = new XmlDocument();
            try { doc.Load(_XmlFile); }
            catch (Exception ex) { return nodeResult; }
            XmlNode xnResult = null;
            try { xnResult = doc.SelectSingleNode(_NodePathName); }
            catch (Exception ex) { return nodeResult; }
            if (xnResult == null) return nodeResult;
            string ssssss = xnResult.LocalName;
            nodeResult = new xNode();
            nodeResult.m_Index = 1;
            nodeResult.m_Name = xnResult.Name;
            nodeResult.m_Text = xnResult.InnerText;
            if (xnResult.Attributes != null)
            {
                foreach (XmlAttribute xa in xnResult.Attributes)
                {
                    nodeResult.m_Attributes.Add(xa.Name, xa.Value);
                }
            }
            return nodeResult;
        }
        /// <summary>
        /// 提取指定节点下的所有子节点，不包含二级子节点。
        /// </summary>
        /// <param name="_XmlFile">[in]文件绝对路径名，包含文件名、扩展名</param>
        /// <param name="_NodePathName">[in]节点全路径名</param>
        /// <returns>所有子节点</returns>
        public static xNode[] f_Get_ChildNodes(string _XmlFile, string _NodePathName)
        {
            List<xNode> listResult = new List<xNode>();
            if (_XmlFile == null || _XmlFile == string.Empty) return listResult.ToArray();
            if (File.Exists(_XmlFile) == false) return listResult.ToArray();
            XmlDocument doc = new XmlDocument();
            try { doc.Load(_XmlFile); }
            catch (Exception ex) { return listResult.ToArray(); }
            XmlNode xnResult = null;
            try { xnResult = doc.SelectSingleNode(_NodePathName); }
            catch (Exception ex) { return listResult.ToArray(); }
            if (xnResult == null) return listResult.ToArray();
            int index = 0;
            foreach (XmlNode xn in xnResult.ChildNodes)
            {
                index++;
                xNode node = new xNode();
                node.m_Index = index;
                node.m_Name = xn.Name;
                node.m_Text = xn.InnerText;
                if (xn.Attributes != null)
                {
                    foreach (XmlAttribute xa in xn.Attributes)
                    {
                        node.m_Attributes.Add(xa.Name, xa.Value);
                    }
                }
                listResult.Add(node);
            }
            return listResult.ToArray();
        }


        /// <summary>
        /// 提取指定节点路径下，与指定节点名称同名的所有子节点列表
        /// </summary>
        /// <param name="_XmlStream">[in]XML文件流</param>
        /// <param name="_NodePath">[in]节点路径</param>
        /// <param name="_NodeName">[in]节点名称</param>
        /// <returns>键值集合列表</returns>
        public static List<Dictionary<string, string>> f_GetNodeList(string _XmlStream, string _NodePath, string _NodeName)
        {
            List<Dictionary<string, string>> listResult = new List<Dictionary<string, string>>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_XmlStream);
                XmlNode xnResult = doc.SelectSingleNode(_NodePath);
                if (xnResult != null)
                {
                    foreach (XmlNode xn in xnResult.ChildNodes)
                    {
                        if (xn.Name == _NodeName)
                        {
                            if (xn.HasChildNodes)
                            { 
                                Dictionary<string, string> dic = new Dictionary<string,string>();
                                foreach (XmlNode child in xn.ChildNodes)
                                {
                                    dic.Add(string.Format("{0}/{1}/{2}", _NodePath, _NodeName, child.Name), child.InnerText);
                                }
                                if (dic.Count > 0) listResult.Add(dic);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listResult.Clear();
            }

            return listResult;
        }


        /// <summary>
        /// 将字符串型键值集合转换为标准的键值集合结构
        /// </summary>
        /// <param name="_Source">键值集合</param>
        /// <param name="_Separator">分隔符</param>
        /// <returns></returns>
        public static Dictionary<string, string> f_GetDictionary(string _Source, string _Separator)
        {
            string[] sep = new string[] { _Separator };
            string[] sepkv = new string[] { "=" };
            Dictionary<string, string> _List = new Dictionary<string, string>();
            _Source = _Source.Replace("\"", "");
            string[] items = _Source.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                int index = item.IndexOf("=");
                if (index == 0) continue;
                string[] kv = item.Split(sepkv, StringSplitOptions.None);
                _List.Add(kv[0], kv[1]);
            }
            return _List;
        }
        /// <summary>
        /// 提取字符串型键值集合中指定键的值
        /// </summary>
        /// <param name="_Source">键值集合</param>
        /// <param name="_Separator">分隔符</param>
        /// <param name="_Key">键</param>
        /// <returns></returns>
        public static string f_GetDictionaryValue(string _Source, string _Separator, string _Key)
        {
            string strResult = string.Empty;
            if (_Source == string.Empty) return strResult;
            string[] sep = new string[] { _Separator };
            string[] sepkv = new string[] { "=" };
            string[] items = _Source.ToLower().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                int index = item.IndexOf("=");
                if (index == 0) continue;
                string[] kv = item.Split(sepkv, StringSplitOptions.None);
                if (_Key.ToLower() == kv[0])
                {
                    strResult = kv[1].Replace("\"", "");
                    break;
                }
            }
            return strResult;
        }
        /// <summary>
        /// 节点
        /// </summary>
        public class xNode
        {
            /// <summary>
            /// 节点索引
            /// </summary>
            public int m_Index = -1;
            /// <summary>
            /// 节点名称（不包含节点路径）
            /// </summary>
            public string m_Name = string.Empty;
            /// <summary>
            /// 节点路径（不包含节点名称）
            /// </summary>
            public string m_Path = string.Empty;
            /// <summary>
            /// 节点全路径名（即包含节点路径又包含节点名称）
            /// </summary>
            public string m_PathName = string.Empty;
            /// <summary>
            /// 节点内容
            /// </summary>
            public string m_Text = string.Empty;
            /// <summary>
            /// 节点属性表
            /// </summary>
            public Dictionary<string, string> m_Attributes = new Dictionary<string, string>();
        }
    }
}
