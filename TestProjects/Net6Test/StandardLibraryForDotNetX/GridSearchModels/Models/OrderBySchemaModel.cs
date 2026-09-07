using GridSearchModels.Enums;

namespace GridSearchModels.Models
{
    public class OrderBySchemaModel
    {
        public string OrderKey { get; set; }
        public EnOrderBy ByType { get; set; } = EnOrderBy.asc;
    }
}
