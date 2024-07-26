using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace StandardLibrary.Extensions
{
    public static class UrlQueryStringHelper
    {
        public static SearchModel ToSearchModel(this string queryString)
        {
            //?TimeStamp-later-than-or-equal=aaaa&TimeStamp-op=and&TimeStamp-earlier-than-or-equal=bbbb
            //&OrderId-equals=ssss&EndpointUrl-contains=vvvv&sort=TimeStamp+asc&page=1&rows=20

            SearchModel searchModel = new SearchModel() { queryString = queryString };
            if (string.IsNullOrEmpty(queryString)) return searchModel;

            var query = queryString.TrimStart('?');
            List<UrlPart> parts = query.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new UrlPart(x)).ToList();

            //page
            var pageStr = parts.FirstOrDefault(x => x.Key?.Equals("page", StringComparison.OrdinalIgnoreCase) ?? false)?.Value ?? "";
            searchModel.page = int.TryParse(pageStr, out var page) ? page : searchModel.page;
            //page size
            var rowsStr = parts.FirstOrDefault(x => x.Key?.Equals("rows", StringComparison.OrdinalIgnoreCase) ?? false)?.Value ?? "";
            searchModel.pageSize = int.TryParse(rowsStr, out var rows) ? rows : searchModel.pageSize;
            //sort
            if (parts.Any(x => x.Key?.Equals("sort", StringComparison.OrdinalIgnoreCase) ?? false))
            {
                searchModel.sort = searchModel.sort == null ? new List<OrderBySchemaModel>() : searchModel.sort;
                searchModel.sort.AddRange(GetSorts(parts.FindAll(x => x.Key?.Equals("sort", StringComparison.OrdinalIgnoreCase) ?? false)));
            }
            //filters
            if (parts.Any(x => x.KeyPartCount == 2))
            {
                searchModel.filters = searchModel.filters == null ? new List<FilterSchemaModel>() : searchModel.filters;
                foreach (var grp in parts.FindAll(x => x.KeyPartCount == 2).GroupBy(x => x.ColumnName))
                {
                    searchModel.filters.AddRange(GetFilers(grp.ToList()));
                }
            }
            return searchModel;
        }

        private static IEnumerable<OrderBySchemaModel> GetSorts(List<UrlPart> parts)
        {
            List<OrderBySchemaModel> sorts = new List<OrderBySchemaModel>();
            OrderBySchemaModel orderBy;
            foreach (var part in parts.FindAll(x => x.Key?.Equals("sort", StringComparison.OrdinalIgnoreCase) ?? false))
            {
                orderBy = new OrderBySchemaModel
                {
                    OrderKey = part.ColumnName,
                    ByType = ConversionsHelper.ConvertToEnum<EnOrderBy>(part.Relation ?? "", true) ?? EnOrderBy.asc
                };
                if (!string.IsNullOrEmpty(orderBy.OrderKey))
                {
                    sorts.Add(orderBy);
                }
            }
            return sorts;
        }

        private static IEnumerable<FilterSchemaModel> GetFilers(List<UrlPart> urlParts)
        {
            List<FilterSchemaModel> filters = new List<FilterSchemaModel>();
            FilterSchemaModel filter = null;
            foreach (var part in urlParts)
            {
                var relation = ConversionsHelper.ConvertToEnum<EnCompRelations>((part.Relation ?? "").Replace("-", "_"), true);
                var isconnlog = !relation.HasValue && (part.Relation ?? "").Equals("op", StringComparison.OrdinalIgnoreCase);

                filter = filter == null ? new FilterSchemaModel() : filter;
                filter.SearchKey = part.ColumnName;
                filter.SearchValue = part.Value;
                filter.Relations = relation;

                if (isconnlog)
                {
                    filter.ConnLogic = ConversionsHelper.ConvertToEnum<EnConnectLogics>(part.Value ?? "", true);
                    filter.SearchValue = null;
                }
                else
                {
                    if (!string.IsNullOrEmpty(filter.SearchKey)) filters.Add(filter);
                    filter = null;
                }
            }
            return filters;
        }

        public static QueryBuilderRuleModel ToQueryBuilderRuleModel<TModel>(this IEnumerable<FilterSchemaModel> filters)
        {
            var type = typeof(TModel);

            var model = new QueryBuilderRuleModel
            {
                condition = "AND",
                rules = new List<QueryBuilderRuleModel>(),
            };
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    var pInfo = type.GetProperty(filter.SearchKey)?.PropertyType;
                    model.rules.Add(new QueryBuilderRuleModel()
                    {
                        id = filter.SearchKey,
                        field = filter.SearchKey,
                        type = GetMappingType(pInfo),
                        input = "text",
                        @operator = filter.Relations.HasValue && RelationMappingDictionary.ContainsKey(filter.Relations.Value) ? RelationMappingDictionary[filter.Relations.Value] : "equal",
                        value = new List<string> { GetMappingValue(pInfo, filter.SearchValue) }
                    });
                }
            }

            return model;
        }

        #region Query Builder Rule mappings

        private static string GetMappingValue(Type type, string val)
        {
            string rsl = val;
            //if (type == typeof(bool))
            //{
            //    rsl = bool.TryParse(val, out bool bVal) ? (bVal ? 1 : 0).ToString() : val;
            //}
            return rsl;
        }
        private static string GetMappingType(Type type)
        {
            return type != null && TypeMappingDictionary.ContainsKey(type) ? TypeMappingDictionary[type] : "string";
        }
        private static readonly Dictionary<Type, string> TypeMappingDictionary =
            new Dictionary<Type, string>() {
                { typeof(bool), "boolean" },
                { typeof(int), "integer" },
                { typeof(string), "string" },
            };
        private static readonly Dictionary<EnCompRelations, string> RelationMappingDictionary =
            new Dictionary<EnCompRelations, string>()
            {
                { EnCompRelations.Contains, "contains"},

                { EnCompRelations.Earlier_than, "less"},
                { EnCompRelations.Earlier_than_or_equal, "less_or_equal"},
                { EnCompRelations.Ends_with, "ends_with"},
                { EnCompRelations.Equals, "equal"},

                { EnCompRelations.Greater_Than, "greater"},
                { EnCompRelations.Greater_Than_Or_Equal, "greater_or_equal"},
                { EnCompRelations.Later_than, "greater"},
                { EnCompRelations.Later_than_or_equal, "greater_or_equal"},

                { EnCompRelations.Less_Than, "less"},
                { EnCompRelations.Less_Than_Or_Equal, "less_or_equal"},

                { EnCompRelations.Not_Equals, "not_equal"},
                { EnCompRelations.Starts_with, "begins_with"},
            };

        #endregion
    }

    public class UrlPart
    {
        public UrlPart()
        {

        }
        public UrlPart(string part)
        {
            Part = part;
            ToSeparateValues();
        }
        public string Part { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ColumnName { get; set; }
        public string Relation { get; set; }
        public int KeyPartCount { get; set; }

        public void ToSeparateValues()
        {
            var leftAndRightPart = Part?.Split('=') ?? Array.Empty<string>();
            if (leftAndRightPart.Length < 1) return;

            Key = leftAndRightPart[0];
            if (leftAndRightPart.Length > 1) Value = HttpUtility.UrlDecode(leftAndRightPart[1] ?? "");
            var skmprt = Key.Split("-".ToArray(), 2);
            KeyPartCount = skmprt.Length;
            ColumnName = KeyPartCount > 0 ? skmprt[0] : "";
            Relation = KeyPartCount > 1 ? skmprt[1] : "";

            if (Key.Equals("sort", StringComparison.OrdinalIgnoreCase))
            {
                var vls = Value?.Replace("+", " ").Split(" ".ToCharArray());
                if (vls != null && vls.Length > 0)
                {
                    ColumnName = vls[0];
                    Relation = vls.Length > 1 ? vls[1] : "";
                }
            }
        }
    }

    public static class ConversionsHelper
    {
        public static T DeepCopy<T>(object obj) where T : class
        {
            return Deserialize<T>(Serialize(obj));
        }
        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        public static T Deserialize<T>(string str) where T : class
        {
            T result;
            try
            {
                result = JsonSerializer.Deserialize<T>(str, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public static T? ConvertToEnum<T>(string str, bool ignoreCases = true) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, ignoreCases, out T re))
            {
                resl = re;
            }
            return resl;
        }
    }

}
