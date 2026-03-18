using GridSearchModels.Enums;
using GridSearchModels.Models;

namespace GridSearchModels.Helpers
{
    public static class SearcherHelper
    {
        public static SearchModel ToSearchModel(this string? queryString, string? gidName = null)
        {
            //?TimeStamp-later-than-or-equal=aaaa&TimeStamp-op=and&TimeStamp-earlier-than-or-equal=bbbb
            //&OrderId-equals=ssss&EndpointUrl-contains=vvvv&sort=TimeStamp+asc&page=1&rows=20

            SearchModel searchModel = new() { queryString = queryString };
            if (string.IsNullOrEmpty(queryString)) return searchModel;

            var query = queryString.TrimStart('?');
            List<UrlPart> parts = query.Split("&", StringSplitOptions.RemoveEmptyEntries).Select(x => new UrlPart(x, gidName)).ToList();

            //page
            var pageStr = parts.FirstOrDefault(x => x.Key?.Equals("page", StringComparison.OrdinalIgnoreCase) ?? false)?.Value ?? "";
            searchModel.page = int.TryParse(pageStr, out var page) ? page : searchModel.page;
            //page size
            var rowsStr = parts.FirstOrDefault(x => x.Key?.Equals("rows", StringComparison.OrdinalIgnoreCase) ?? false)?.Value ?? "";
            searchModel.pageSize = int.TryParse(rowsStr, out var rows) ? rows : searchModel.pageSize;
            //sort
            if (parts.Any(x => x.Key?.Equals("sort", StringComparison.OrdinalIgnoreCase) ?? false))
            {
                searchModel.sort ??= new List<OrderBySchemaModel>();
                searchModel.sort.AddRange(GetSorts(parts.FindAll(x => x.Key?.Equals("sort", StringComparison.OrdinalIgnoreCase) ?? false)));
            }
            //filters
            if (parts.Any(x => x.KeyPartCount == 2))
            {
                searchModel.filters ??= new List<FilterSchemaModel>();
                foreach (var grp in parts.FindAll(x => x.KeyPartCount == 2).GroupBy(x => x.ColumnName))
                {
                    searchModel.filters.AddRange(GetFilers(grp.ToList()));
                }
            }
            return searchModel;
        }

        private static IEnumerable<OrderBySchemaModel> GetSorts(List<UrlPart> parts)
        {
            List<OrderBySchemaModel> sorts = new();
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
            List<FilterSchemaModel> filters = new();
            FilterSchemaModel? filter = null;
            int order = 1;
            foreach (var part in urlParts)
            {
                var relation = ConversionsHelper.ConvertToEnum<EnCompRelations>((part.Relation ?? "").Replace("-", "_"), true);
                var isconnlog = !relation.HasValue && (part.Relation ?? "").Equals("op", StringComparison.OrdinalIgnoreCase);

                filter ??= new FilterSchemaModel();
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
                    filter.Orders = order;
                    if (filter.ConnLogic == null)
                        filter.Orders = 0;
                    if (!string.IsNullOrEmpty(filter.SearchKey))
                        filters.Add(filter);

                    filter = null;
                    order++;
                }
            }
            return filters;
        }
    }
}
