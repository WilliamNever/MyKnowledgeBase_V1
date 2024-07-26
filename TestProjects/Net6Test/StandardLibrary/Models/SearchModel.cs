using System.Collections.Generic;

namespace StandardLibrary.Models
{
    public class SearchModel
    {
        public int pageSize { get; set; } = 50;//default page size is 50
        public int page { get; set; } = 1;   //the beginning page number is 1.
        public List<OrderBySchemaModel> sort { get; set; }
        public string queryString { get; set; }
        public List<FilterSchemaModel> filters { get; set; }
    }

    public class OrderBySchemaModel
    {
        public string OrderKey { get; set; }
        public EnOrderBy ByType { get; set; } = EnOrderBy.asc;
    }

    public enum EnOrderBy
    {
        asc = 0,
        desc = 1,
    }

    public class FilterSchemaModel
    {
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public EnCompRelations? Relations { get; set; }
        public EnConnectLogics? ConnLogic { get; set; }
    }

    public enum EnCompRelations
    {
        Less_Than_Or_Equal = -35,
        Less_Than = -30,
        Later_than_or_equal = -25,
        Later_than = -20,
        Ends_with = -10,

        Not_Equals = -1,
        Equals = 0,
        Contains = 1,

        Starts_with = 10,
        Earlier_than = 20,
        Earlier_than_or_equal = 25,
        Greater_Than = 30,
        Greater_Than_Or_Equal = 35
    }

    public enum EnConnectLogics
    {
        And = 0,
        Or = 1,
    }
}
