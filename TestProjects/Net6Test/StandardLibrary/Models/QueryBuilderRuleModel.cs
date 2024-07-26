using StandardLibrary.Extensions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StandardLibrary.Models
{
    /// <summary>
    /// !!!!!! this class schema was defined in Castle.DynamicLinqQueryBuilder pkg. please use the class in the pkg !!!!!!
    /// !!!!!! this schema can be used in Castle.DynamicLinqQueryBuilder pkg to query data via EF/core. !!!!!!
    /// </summary>
    public class QueryBuilderRuleModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object condition { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<QueryBuilderRuleModel> rules { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string field { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string type { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string input { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string @operator { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> value { get; set; }

        public override string ToString()
        {
            return ConversionsHelper.Serialize(this);
        }
    }

    public class QueryRuleSearchModel
    {
        public int pageSize { get; set; } = 50;//default page size is 50
        public int page { get; set; } = 1;   //the beginning page number is 1.
        public string sortfield { get; set; }
        public string sortOrder { get; set; }

        public QueryBuilderRuleModel qRuleBuilder { get; set; } = null;

        public override string ToString()
        {
            return ConversionsHelper.Serialize(this);
        }
    }
}
