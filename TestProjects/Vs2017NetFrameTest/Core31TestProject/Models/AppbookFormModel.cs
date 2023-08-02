using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core31TestProject.Models
{
    [XmlType("FormNumber")]
    public class AppbookFormModel
    {
        [XmlAttribute]
        public int Sequence { get; set; }
        [XmlText]
        public string FormNumber { get; set; }
    }

    [XmlType("AppBook")]
    public class AppBookModel
    {
        [XmlAttribute]
        public int FeedId { get; set; }
        public string AppBookForm { get; set; }
        public string CoverLeft { get; set; }
        public string CoverRight { get; set; }
        public string Division { get; set; }
        public string Title { get; set; }
        public List<AppbookFormModel> Forms { get; set; }
    }

    [XmlType("AppBooks")]
    public class AppBooksModel
    {
        [XmlElement("AppBook")]
        public List<AppBookModel> AppBooks { get; set; }
    }
}

