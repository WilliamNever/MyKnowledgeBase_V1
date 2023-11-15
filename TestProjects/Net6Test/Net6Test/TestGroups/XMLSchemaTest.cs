using StandardLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Net6Test.TestGroups
{
    public static class XMLSchemaTest
    {
        public static async Task Test1()
        {
            var xmlString = @"<Status><Code id='inforId' respcode='200'>SUCCESS</Code><Info /></Status>";

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var spo = new SPO
            {
                Message = new BSTM<XmlDocument>
                {
                    TPMessage = xmlDoc
                }
            };

            string xml = XMLConversionsHelper.SerializerToXML(spo);

            XNamespace ns = "TSP";
            XDocument xDocument = XDocument.Parse(xml);
            xDocument.Root.Name = ns + xDocument.Root.Name.LocalName;
            string str = xDocument.ToString();
            var nodes = xDocument.Root?.Descendants().ToList();
            if (nodes != null)
                foreach (var node in nodes)
                {
                    if (node.Name.NamespaceName == "")
                    {
                        node.Attributes("xmlns").Remove();
                        node.Name = ns + node.Name.LocalName;
                    }
                }
            string str1 = xDocument.ToString();
        }
    }

    [XmlRoot("STC", Namespace = "TSP")]
    public class SPO : BST<XmlDocument>
    {
    }

    public class BST<T> where T : class
    {
        public BaseSpeedTransactionRouting Routing { get; set; }
        public BSTM<T> Message { get; set; }
    }

    public class BaseSpeedTransactionRouting
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentId { get; set; }
        public string ReferenceId { get; set; }
        public string Environment { get; set; }
        public Guid ConversationId { get; set; }
        public string SenderParty { get; set; }
        public DateTime TimestampUTC { get; set; }
    }

    public class BSTM<T> where T : class
    {
        [XmlElement("TPM")]
        public T TPMessage { get; set; }
    }

}
