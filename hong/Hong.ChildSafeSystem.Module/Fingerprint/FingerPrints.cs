using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;

namespace Hong.ChildSafeSystem.Module
{
    public enum PersonType
    {
        Student = 1,
        Relative = 2,
        Teacher = 3,
    }

    /// <summary>
    /// FingerPrint 的摘要说明
    /// ObjectType  1：孩子  2：家长  3：教工
    /// </summary>
    /// 
    [Serializable]
    public class FingerPrint
    {
        public FingerPrint(int id)
        {
            _id = id;
            _objectId = -1;
            _template = new byte[0];
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
        }


        private int _objectId;
        public int ObjectId
        {
            get
            {
                return _objectId;
            }

            set
            {
                _objectId = value;
            }
        }

        //ss 学生
        //rr 家长
        //tt 教工
        private string _objectType;
        public string ObjectType
        {
            get
            {
                return _objectType;
            }

            set
            {
                _objectType = value;
            }
        }

        private int _templateSize;
        public int TemplateSize
        {
            get
            {
                return _templateSize;
            }
            set
            {
                _templateSize = value;
            }
        }

        private byte[] _template;
        public byte[] Template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
        }

        //ss 学生
        //rr 家长
        //tt 教工
        private string _fingerType;
        public string FingerType
        {
            get
            {
                return _fingerType;
            }

            set
            {
                _fingerType = value;
            }
        }

        public static string SQLSelect()
        {
            return " select fingerprint.Id, fingerprint.TemplateSize,                     "
                 + " fingerprint.Template, fingerprint.ObjectType,                        "
                 + " fingerprint.ObjectId, fingerprint.FingerType                         "
                 + " from fingerprint                                                     "
                 + " where (fingerprint.TemplateSize > 0)                                 "
                 ;
        }
/*
        public static FingerPrint[] FingerPrintObtain(string fingerPrintObjectType, int fingerPrintObjectId)
        {
            string sql = SQLSelect() + string.Format(" and (fingerprint.ObjectType = '{0}') and (fingerprint.ObjectId = {1:G})", fingerPrintObjectType, fingerPrintObjectId);
            DataTable dt = SQLHelper.singleton.TableBySQL(sql);
            DataRow[] rowList = dt.Select();
            FingerPrint[] fingerPrints = new FingerPrint[rowList.Length];
            int k = 0;
            foreach (DataRow row in rowList)
            {
                FingerPrint fingerPrint = new FingerPrint(Convert.ToInt32(row["Id"]));
                fingerPrint.TemplateSize = Convert.ToInt32(row["TemplateSize"]);
                fingerPrint.Template = (byte[])row["Template"];
                fingerPrint.ObjectType = row["ObjectType"].ToString();
                fingerPrint.ObjectId = Convert.ToInt32(row["ObjectId"]);
                fingerPrint.FingerType = row["FingerType"].ToString();

                fingerPrints[k] = fingerPrint;
                k = k + 1;
            }
            return fingerPrints;
        }

        public static bool FingerPrintAdd(FingerPrint fingerPrint)
        {
            string sql = "insert into [fingerPrint] "
                       + " (TemplateSize, Template, ObjectType, ObjectId, FingerType)"
                       + " values"
                       + string.Format(" ( @TemplateSize, @Template, '{0}', {1:G}, '{2}')",
                       fingerPrint.ObjectType, fingerPrint.ObjectId, fingerPrint.FingerType);

            SQLHelper.singleton.SQLCommand.CommandText = sql;
            SQLHelper.singleton.SQLCommand.Parameters.Clear();
            SQLHelper.singleton.SQLCommand.Parameters.Add("@TemplateSize", SqlDbType.Int).Value = fingerPrint.TemplateSize;
            SQLHelper.singleton.SQLCommand.Parameters.Add("@Template", SqlDbType.Binary, fingerPrint.Template.Length).Value = fingerPrint.Template;
            return SQLHelper.singleton.SQLCommand.ExecuteNonQuery() > 0;
        }

        public static bool FingerPrintEdit(FingerPrint fingerPrint)
        {
            string sql = "update [fingerPrint] set"
                       + string.Format(" TemplateSize = '{0:G}', Template = @Template, ObjectType = '{1}', ObjectId = {2}, FingerType = '{3}' where Id = {4:G}"
                         , fingerPrint.TemplateSize, fingerPrint.ObjectType, fingerPrint.ObjectId, fingerPrint.FingerType, fingerPrint.Id);

            SQLHelper.singleton.SQLCommand.CommandText = sql;
            SQLHelper.singleton.SQLCommand.Parameters.Clear();
            SQLHelper.singleton.SQLCommand.Parameters.Add("@Template", SqlDbType.Binary, fingerPrint.Template.Length).Value = fingerPrint.Template;

            return SQLHelper.singleton.SQLCommand.ExecuteNonQuery() > 0;
        }

        public static bool FingerPrintDelete(int fingerPrintId)
        {
            string sql = string.Format("delete from [fingerPrint] where Id = {0:G}", fingerPrintId);

            int exec = SQLHelper.singleton.SQLExecute(sql);
            return exec > 0;
        }
 */
    }
}