using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StandardLibrary.Helpers
{
    public static class XMLConversionsHelper
    {
        /*
         * inbound model - 

    [XmlRoot( ElementName = "response" ,Namespace = "http://www.xxx.com/xxx/response", IsNullable = true)]
    public class TKMExx
    {
        [XmlElement(ElementName = "id")]
        public string? xxItem { get; set; }
        public string? Item { get; set; }
        public string? TKMEx_Item { get; set; }
        public string? BaseItem { get; set; }
    }

        outbound xml - 
        <response xmlns="http://www.xxx.com/xxx/response">
          <Item>ds</Item>
          <id>xxID</id>
          <TKMEx_Item>TKMEx_Item_ex</TKMEx_Item>
          <BaseItem>b_Item</BaseItem>
        </response>

         */
        public static string SerializerToXML<T>(T model)
        {
            XmlWriterSettings XWS = new XmlWriterSettings();
            XWS.OmitXmlDeclaration = true;
            XWS.Indent = true;
            XWS.NamespaceHandling = NamespaceHandling.Default;
            XWS.Encoding = Encoding.UTF8;
            return SerializerToXML(model, XWS);
        }
        public static string SerializerToXML<T>(T model, XmlWriterSettings XWS)
        {
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww, XWS))
                {
                    XmlSerializer xSerial = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces nsx = new XmlSerializerNamespaces();
                    nsx.Add("", "");
                    xSerial.Serialize(writer, model, nsx);
                    writer.Flush();
                    sww.Flush();
                    return sww.ToString();
                }
            }
        }
    }
}
