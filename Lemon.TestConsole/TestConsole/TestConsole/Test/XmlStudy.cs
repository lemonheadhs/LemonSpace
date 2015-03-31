using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace TestConsole
{
    public static class XmlStudy
    {
        public static string xmlLocation = "../../Resource/Account.xml";

        // 使用 XmlTextReader读取Xml
        public static void DoTest()
        {
            using (XmlTextReader reader = new XmlTextReader(xmlLocation))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement)//Element, Text, EndElement
                    {
                        Console.WriteLine("{0}:{1}, {2}", reader.Name, reader.Value, reader.IsStartElement());
                        Console.ReadKey();
                    }
                }
            }
        }

        // 进行Xml架构验证
        public static void DoTest1()
        {
            Console.WriteLine("XmlStudy  Start");

            string schemaPath = "../../Resource/SqlMapConfig.xsd";
            string xmlPath = "../../Resource/SqlMap.config";

            //XmlSchema schema = XmlSchema.Read(
            //    new FileStream(schemaPath, FileMode.Open),
            //    delegate(object sender, ValidationEventArgs e)
            //    {
            //        Console.WriteLine(e.Exception.Message);
            //    });

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add("http://ibatis.apache.org/dataMapper", schemaPath);
            //settings.Schemas.Add(schema);
            //settings.ValidationEventHandler += delegate(object sender, ValidationEventArgs args)
            //            {
            //                Console.WriteLine(args.Exception.Message);
            //            };  当读取器遇到验证错误时发生。如果未指定 ValidationEventHandler，则引发 XmlSchemaException

            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                try
                {
                    while (reader.Read()) { }
                }
                catch (XmlSchemaException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("XmlStudy End");
        }
    }
}
