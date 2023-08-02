using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TestCoreProject.Models
{
    public class TModel
    {
        public string Infor { get; set; }
        [XmlElement("string")]
        public List<string> NaList { get; set; }
    }

    public class TModelVa
    {
        public int Infor { get; set; }
    }

    public class NodeAttr
    {
        [System.Xml.Serialization.XmlElement]
        public string name { get; set; }
        [System.Xml.Serialization.XmlElement]
        public string value { get; set; }
        public bool? Gender { get; set; }
    }
}
