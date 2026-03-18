using GridSearchModels.Enums;

namespace GridSearchModels.Models
{
    public class FilterSchemaModel
    {
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public EnCompRelations? Relations { get; set; }
        public EnConnectLogics? ConnLogic { get; set; }
        public int Orders { get; set; } = 1;
    }
}
