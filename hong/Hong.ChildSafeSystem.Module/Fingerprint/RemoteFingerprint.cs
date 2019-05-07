using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hong.ChildSafeSystem.Module
{
    public interface IRemoteFingerprint
    {
        /// <summary>
        /// 取得指纹数据对应的人物对象
        /// </summary>
        /// <param name="matBuf"></param>
        /// <param name="matSize"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        int GetObject(byte[] matBuf, int matSize, string objectType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrint"></param>
        /// <returns></returns>
        bool AddData(FingerPrint fingerPrint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrint"></param>
        /// <returns></returns>
        bool EditData(FingerPrint fingerPrint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrintId"></param>
        /// <returns></returns>
        bool DeleteData(int fingerPrintId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        FingerPrint[] GetFingers(string type, int id);
    }

    /// <summary>
    /// 
    /// </summary>
    public class RemoteFingerprint : MarshalByRefObject, IRemoteFingerprint
    {
        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler TestEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        #region IRemoteFingerprint 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matBuf"></param>
        /// <param name="matSize"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public int GetObject(byte[] matBuf, int matSize, string objectType)
        {
            int objectId = -1;
            if (matSize <= 0 || matBuf == null)
            {
                return objectId;
            }

            string sql = "select fingerprint.Id, fingerprint.ObjectId , fingerprint.Template , fingerprint.TemplateSize from fingerprint where fingerprint.ObjectType = '" + objectType + "'";
            DataTable dt = SQLHelper.singleton.TableBySQL(sql);
            DataRow[] rowList = dt.Select();

            byte[] tpBuf;
            int tpSize;
            DataRow tmp = null;
            bool found = false;
            foreach (DataRow row in rowList)
            {
                tpBuf = (byte[])row["Template"];
                tpSize = Convert.ToInt32(row["TemplateSize"]);
                if (FingerprintWrapper.Default().VerifyTemplateOneToOne(ref tpBuf[0], tpSize, ref matBuf[0], matSize))
                {
                    objectId = Convert.ToInt32(row["ObjectId"]);
                    break;
                }
                //string refStr = ASCIIEncoding.ASCII.GetString(tpBuf);
                //string matStr = ASCIIEncoding.ASCII.GetString(MatVal);
                //if (FingerprintWrapper.Default().Base64_VerifyTemplateOneToOne(new StringBuilder(refStr), new StringBuilder(matStr)))
                //{
                //    found = true;
                //    tmp = row;
                //    break;
                //}
            }
            return objectId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrint"></param>
        /// <returns></returns>
        public bool AddData(FingerPrint fingerPrint)
        {
            string sql = "insert into [fingerPrint] "
                       + " (TemplateSize, Template, ObjectType, ObjectId, FingerType)"
                       + " values"
                       + string.Format(" ( @TemplateSize, @Template, '{0}', {1:G}, '{2}')",
                       fingerPrint.ObjectType, fingerPrint.ObjectId, fingerPrint.FingerType);
            SQLHelper.singleton.SQLCommand.Parameters.Clear();
            SQLHelper.singleton.SQLCommand.CommandText = sql;
            SQLHelper.singleton.SQLCommand.Parameters.Add("@TemplateSize", DbType.Int32).Value = fingerPrint.TemplateSize;
            SQLHelper.singleton.SQLCommand.Parameters.Add("@Template", SqlDbType.Binary, fingerPrint.Template.Length).Value = fingerPrint.Template;
            int result = SQLHelper.singleton.SQLCommand.ExecuteNonQuery();
            return result > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public FingerPrint[] GetFingers(string type, int id)
        {
            string sql = FingerPrint.SQLSelect() + string.Format(" and (fingerprint.ObjectType = '{0}') and (fingerprint.ObjectId = {1:G})", type, id);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrint"></param>
        /// <returns></returns>
        public bool EditData(FingerPrint fingerPrint)
        {
            string sql = "update [fingerPrint] set"
                       + string.Format(" TemplateSize = '{0:G}', Template = @Template, ObjectType = '{1}', ObjectId = {2}, FingerType = '{3}' where Id = {4:G}"
                         , fingerPrint.TemplateSize, fingerPrint.ObjectType, fingerPrint.ObjectId, fingerPrint.FingerType, fingerPrint.Id);

            SQLHelper.singleton.SQLCommand.Parameters.Clear();
            SQLHelper.singleton.SQLCommand.CommandText = sql;
            SQLHelper.singleton.SQLCommand.Parameters.Add("@Template", SqlDbType.Binary, fingerPrint.Template.Length).Value = fingerPrint.Template;

            int exec = SQLHelper.singleton.SQLExecute(sql);
            return exec > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fingerPrintId"></param>
        /// <returns></returns>
        public bool DeleteData(int fingerPrintId)
        {
            string sql = string.Format("delete from [fingerPrint] where Id = {0:G}", fingerPrintId);

            int exec = SQLHelper.singleton.SQLExecute(sql);
            return exec > 0;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class FingerprintEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string Info;

        /// <summary>
        /// 
        /// </summary>
        public FingerprintEventArgs()
        {
        }
    }
}