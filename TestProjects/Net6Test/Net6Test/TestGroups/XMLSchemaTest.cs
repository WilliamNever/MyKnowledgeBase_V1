using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StandardLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Net6Test.TestGroups
{
    public static class XMLSchemaTest
    {
        public static async Task Test2()
        {
            var xmlString = @"<Status><Code id='inforId' respcode='200'>SUCCESS</Code><Info /></Status>";
            var jsStr = ConvertXmlToJson(xmlString);

            var jsObj = JObject.Parse(jsStr);
            string xml = XMLConversionsHelper.SerializerToXML(jsObj);
        }

        public static string ConvertXmlToJson(string xml)
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(xmlDoc);
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return string.Empty;
            }
        }

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

        public async static Task Test3()
        {
            Console.WriteLine($"Test3 - ThreadID = {Thread.CurrentThread.ManagedThreadId}");
            var tsk = Task.Run(() =>
            {
                var xmlString = @"<Status><Code id='inforId' respcode='200'>SUCCESS</Code><Info /></Status>";
                //throw new Exception("HHere");
                return xmlString;
            });

            Working("AP");

            await Task.Delay(30000);

            var str = await tsk.ContinueWith(async t =>
            {
                var ptSt = t.Status;
                var rsl = await t;
                Working("APX");
                return rsl;
            }).Result;
        }

        /// <summary>
        /// async void should only be used for event handlers.
        /// async void is the only way to allow asynchronous event handlers to work 
        /// because events do not have return types (thus cannot make use of Task and Task<T>). 
        /// Any other use of async void does not follow the TAP model and can be challenging to use, such as:
        /// 
        /// Exceptions thrown in an async void method can't be caught outside of that method.
        /// async void methods are difficult to test.
        /// async void methods can cause bad side effects if the caller isn't expecting them to be async.
        /// </summary>
        /// <param name="header"></param>
        private async static void Working(string header = "")
        {
            await Task.Run(() =>
            {
                int i = 0;
                while (i < 20)
                {
                    Console.WriteLine($"{header} i = {i} // ThreadID = {Thread.CurrentThread.ManagedThreadId}");
                    i++;
                    Task.Delay(1000).Wait();
                }
            });
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
