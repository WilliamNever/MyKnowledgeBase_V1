using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Core31TestProject.Utilities
{
    public class XMLConverter<T>
    {
        private XmlSerializer serializer;

        public XMLConverter()
        {
            serializer = new XmlSerializer(typeof(T), "http://www.taylorcorp.com/cno");
        }

        public string Serializer(T obj)
        {
            StringWriter sw;
            string rValue = "";
            using (sw = new StringWriter())
            {
                var nms = new XmlSerializerNamespaces(
                    new XmlQualifiedName[] { new XmlQualifiedName("", "http://www.taylorcorp.com/cno") }
                    );
                serializer.Serialize(sw, obj, nms);
                sw.Flush();
                rValue = sw.ToString();
                sw.Close();
            }
            return rValue;
        }

        public T DeSerializer(string str)
        {
            T rValue = default(T);
            StringReader sr;
            using (sr = new StringReader(str.Trim()))
            {
                var obj = serializer.Deserialize(sr);
                rValue = (T)obj;
            }
            return rValue;
        }
    }

}
