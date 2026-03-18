using GridSearchModels.Enums;

namespace GridSearchModels.Models
{
    public class SearchModel
    {
        public int pageSize { get; set; } = 20;//default page size is 20
        public int page { get; set; } = 1;   //the beginning page number is 1.
        public List<OrderBySchemaModel> sort { get; set; }
        public string queryString { get; set; }
        public List<FilterSchemaModel> filters { get; set; }
        public EnConnectLogics filtersConnLogic { get; set; } = EnConnectLogics.And;
    }
}
