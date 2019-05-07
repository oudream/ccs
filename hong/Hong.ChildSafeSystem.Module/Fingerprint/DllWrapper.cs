using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Hong.ChildSafeSystem.Module
{
    /*
     * Bool value:1-True,0-False
     */

    /// <summary>
    /// 动态库包装器
    /// </summary>
    /// <remarks>
    /// LONDEN公司提供的动态库为fpsiml.dll
    /// 适用指纹机:DSD3400,etc.
    /// 函数返回值未特别说明,则1-True,0-False
    /// </remarks>
    public static class DllWrapper
    {
        private const string DllName = "fpsiml.dll";

        //保存图像到文件
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveImage(byte[] pFileName);
        //保存参考模板到文件
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveReferenceTemplate(byte[] pFileName);
        //从文件载入参考模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadReferenceTemplate(byte[] pFileName);
        //保存匹配模板到文件
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveMatchTemplate(byte[] pFileName);
        //从文件载入匹配模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadMatchTemplate(byte[] pFileName);

        /// <summary>
        /// 采集图像后，获取匹配模板
        /// </summary>
        /// <param name="pMatVal">指纹特征值的存储区</param>
        /// <param name="pSizeVal">存储区大小</param>
        /// <returns></returns>
        /// <remarks>用户刷指纹后获取的指纹特征值</remarks>
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int GetMatchTemplate(byte[]  pMatVal, ref long pSizeVal);
        public static extern int GetMatchTemplate(ref byte pMatVal, ref int pSizeVal);

        //设置SDK当前使用的匹配模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int SetMatchTemplate(byte[] newMatVal, long newSizeVal);
        public static extern int SetMatchTemplate(ref byte newMatVal, int newSizeVal);
        //登记模板后，获取参考模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int GetReferenceTemplate(byte[] pRefVal,ref long pSizeVal);
        public static extern int GetReferenceTemplate(ref byte pRefVal, ref int pRefSize);
        //设置SDK当前使用的参考模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int SetReferenceTemplate(byte[] newRefVal,long newSizeVal);	
        public static extern int SetReferenceTemplate(ref byte newRefVal, int newSizeVal);
        //获取图像
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetImage(byte[] newRefVal, ref long pSizeVal);
        //在SDK内比对当前的参考模板和匹配模板是否是同一手指
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int VerifyTemplateInner();
        //比对参考模板和匹配模板是否是同一手指
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        //public static extern int VerifyTemplateOneToOne(byte[] RefVal, long RefSize, byte[] MatVal, long MatSize, int bRotateMatch);
        public static extern int VerifyTemplateOneToOne(ref byte RefVal, int RefSize, ref byte MatVal, int MatSize, int bRotateMatch);
        //设置登记次数（已关闭该功能）
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetEnrollCount(long count);

        /// <summary>
        /// 设置接受指纹处理消息的窗口句柄
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        /// 
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMainWnd(IntPtr hWnd);

        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitAndReady();

        /// <summary>
        /// 初始化指纹处理过程
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitTransaction();

        //开始处理指定的指纹过程
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int StartTransaction(long workmode);
        //结束指纹处理过程
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int EndTransaction();
        //结束指纹处理过程
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseTransaction();
        //校准指纹设备
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int CalibrateSensor();
        //设置比对中的有关参数
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetCriteriaFAR(long newVal);
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetCriteriaFRR(long newVal);
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRotateMatch(int bVal);
        //设置回调函数 typedef VOID (__stdcall *FPSPROC)(LONG,PFPSRESULT,void * pContext);
        //[System.Runtime.InteropServices.DllImport("DllWrapper.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern int SetFpsCallBack(FPSPROC fTransProc,void * pContext);
        //设置图像数据，从外部导入图像到SDK
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetImage(byte[] pRawImage, int wImageWidth, int wImageHeight);
        //画指纹图像
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int DrawImage(IntPtr hdc, int left, int top);

        //获取图像
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int CaptureImages();
        //登记模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnrollTemplates();
        //识别SDK内部的参考模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int VerifyTemplates();

        //3次登记模板功能有效
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableMinTouchEnroll(int bMinTouchEnroll);

        //SDK 1:N 
        //设置1:N的个数
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetOneToManyCount(int nCount);
        //添加1个模板
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddOneToManyTemplate(byte[] lptemplate, long templatesize);
        //释放模板空间
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int FreeOneToManyTemplates();
        //开始1:N识别 识别结果由消息返回 
        //高16位为1表示识别成功，为0表示识别失败，
        //低16表示返回的模板内部编号，
        //内部编号，是由AddOneToManyTemplate函数添加模板时累加的
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int IdentifyTemplates();

        //以BASE64编码模版数据的函数
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_GetMatchTemplate(StringBuilder pMatVal, ref long pSizeVal);
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_SetMatchTemplate(StringBuilder newMatVal);

        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_GetReferenceTemplate(StringBuilder pRefVal, ref long pSizeVal);
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_SetReferenceTemplate(StringBuilder newRefVal);

        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_VerifyTemplateOneToOne(StringBuilder RefVal, StringBuilder MatVal, int bRotateMatch);
        [System.Runtime.InteropServices.DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Base64_AddOneToManyTemplate(StringBuilder lptemplate);
    }
}
