using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace XMLTester
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
        //static void Main(string[] args)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
        //                "<title>Pride And Prejudice</title>" +
        //                "</book>");

        //    //Create an XML declaration. 
        //    XmlDeclaration xmldecl;
        //    xmldecl = doc.CreateXmlDeclaration("1.0", null, null);

        //    //Add the new node to the document.
        //    XmlElement root = doc.DocumentElement;
        //    doc.InsertBefore(xmldecl, root);

        //    XmlElement elem = doc.CreateElement("price");
        //    //XmlText text = doc.CreateTextNode("19.95");

        //    XmlElement e1 = doc.CreateElement("port");
        //    e1.InnerXml = "4400";
        //    elem.InsertAfter(e1, elem.FirstChild);

        //    e1 = doc.CreateElement("ComNumber");
        //    e1.InnerXml = "COM1";
        //    elem.InsertBefore(e1, elem.FirstChild);

        //    doc.DocumentElement.AppendChild(elem);
        //    //doc.DocumentElement.LastChild.AppendChild(text);


        //    Console.WriteLine("Display the modified XML...");
        //    doc.Save(Console.Out);


        //    Console.Read();
        //}
	}
}
