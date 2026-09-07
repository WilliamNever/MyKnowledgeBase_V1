using GridSearchModels.Enums;
using GridSearchModels.Helpers;
using GridSearchModels.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace StandardLibraryForDotNetX.DBRepos
{
    /// <summary>
    /// This BaseRepo works for processing DB with sql client in Microsoft.Data.SqlClient pkg.
    /// It can works with DbProcessingFactory and SearchModel in this Library.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepo<T>
    {
        protected ILogger<T>? _logger;

        protected BaseRepo(ILogger<T>? logger)
        {
            _logger = logger;
        }

        protected static SqlCommand AddSqlCommandParams(SqlCommand com, List<FilterSchemaModel>? filters, Dictionary<string, SqlDbType> paramTypes, SqlDbType defaultType = SqlDbType.NVarChar)
        {
            if (filters != null)
                foreach (var ft in filters)
                {
                    var dbtype = paramTypes.GetValueOrDefault(ft.SearchKey, defaultType);
                    if (dbtype == defaultType)
                    {
                        dbtype = paramTypes.FirstOrDefault(x => x.Key.ToEquals(ft.SearchKey)).GetValueOrDefault(defaultType);
                    }
                    if (ValidateSqlCommandParams(ft.SearchValue, dbtype))
                    {
                        com.Parameters.Add($"@{ft.SearchKey.ReplaceStr(".", "_")}{ft.Orders}", dbtype);
                        if (ft.Relations == EnCompRelations.Contains)
                            com.Parameters[$"@{ft.SearchKey.ReplaceStr(".", "_")}{ft.Orders}"].Value = $"%{ft.SearchValue}%";
                        else if (ft.Relations == EnCompRelations.Starts_with)
                            com.Parameters[$"@{ft.SearchKey.ReplaceStr(".", "_")}{ft.Orders}"].Value = $"{ft.SearchValue}%";
                        else if (ft.Relations == EnCompRelations.Ends_with)
                            com.Parameters[$"@{ft.SearchKey.ReplaceStr(".", "_")}{ft.Orders}"].Value = $"%{ft.SearchValue}";
                        else
                            com.Parameters[$"@{ft.SearchKey.ReplaceStr(".", "_")}{ft.Orders}"].Value = ft.SearchValue;
                    }
                }
            return com;
        }
        protected static bool ValidateSqlCommandParams(string sValue, SqlDbType sqlType)
        {
            bool isValid = true;
            switch (sqlType)
            {
                case SqlDbType.UniqueIdentifier:
                    isValid = Guid.TryParse(sValue, out _);
                    break;
                case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                    isValid = int.TryParse(sValue, out _);
                    break;

                case SqlDbType.Bit:
                    isValid = bool.TryParse(sValue, out _);
                    break;

                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.Timestamp:
                case SqlDbType.SmallDateTime:
                case SqlDbType.DateTime:
                    isValid = DateTime.TryParse(sValue, out _);
                    break;
                case SqlDbType.Decimal:
                    isValid = decimal.TryParse(sValue, out _);
                    break;
                case SqlDbType.Float:
                    isValid = float.TryParse(sValue, out _);
                    break;
                default:
                    isValid = true;
                    break;

                    #region not validate sql data type
                    //case SqlDbType.Char:
                    //    break;
                    //case SqlDbType.Binary:
                    //    break;
                    //case SqlDbType.NVarChar:
                    //    isValid = true;
                    //    break;

                    //case SqlDbType.Image:
                    //    break;

                    //case SqlDbType.Money:
                    //    break;
                    //case SqlDbType.NChar:
                    //    break;
                    //case SqlDbType.NText:
                    //    break;
                    //case SqlDbType.Real:
                    //    break;


                    //case SqlDbType.SmallMoney:
                    //    break;
                    //case SqlDbType.Text:
                    //    break;

                    //case SqlDbType.VarBinary:
                    //    break;
                    //case SqlDbType.VarChar:
                    //    break;
                    //case SqlDbType.Variant:
                    //    break;
                    //case SqlDbType.Xml:
                    //    break;
                    //case SqlDbType.Udt:
                    //    break;
                    //case SqlDbType.Structured:
                    //    break;
                    #endregion
            }
            return isValid;
        }

        protected static string CreateOrderbies(List<OrderBySchemaModel> sorts, string defaultOrderByColumn)
        {
            string orderby = "";
            if (sorts?.Any() ?? false)
            {
                foreach (var sort in sorts)
                {
                    orderby = $"{orderby},{sort.OrderKey} {sort.ByType}";
                }
            }
            orderby = orderby.Trim(", ".ToCharArray());
            if (!string.IsNullOrEmpty(defaultOrderByColumn?.Trim())
                && (sorts == null || !sorts.Any(o =>
                    o.OrderKey.ToEquals(defaultOrderByColumn.Trim().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[0]))))
            {
                if (string.IsNullOrEmpty(orderby))
                    orderby = $"{defaultOrderByColumn}";
                else
                    orderby = $"{orderby},{defaultOrderByColumn}";
            }
            return orderby.Trim(", ".ToCharArray());
        }

        protected static string CreateCompFlag(EnCompRelations? relation)
        {
            EnCompRelations rle = relation ?? EnCompRelations.Equals;
            string condition = rle switch
            {
                EnCompRelations.Later_than or EnCompRelations.Greater_than => " > ",
                EnCompRelations.Earlier_than or EnCompRelations.Less_than => " < ",
                EnCompRelations.Greater_than_or_equal or EnCompRelations.Later_than_or_equal => " >= ",
                EnCompRelations.Not_Equals => " <> ",
                EnCompRelations.Ends_with or EnCompRelations.Contains or EnCompRelations.Starts_with => " like ",
                EnCompRelations.Less_than_or_equal or EnCompRelations.Earlier_than_or_equal => " <= ",
                _ => " = ",
            };
            return condition;
        }

        protected static string CreateFilters(List<FilterSchemaModel> filters, Dictionary<string, SqlDbType> paramTypes
            , SqlDbType defaultType = SqlDbType.NVarChar, EnConnectLogics ConnLogic = EnConnectLogics.And)
        {
            string conditions = "";
            if (filters != null)
            {
                foreach (var grp in filters.GroupBy(x => x.SearchKey))
                {
                    string express = "";
                    var grpfilters = grp.OrderBy(x => x.Orders).ToList();
                    if (grpfilters.Count > 0)
                    {
                        var dbtype = paramTypes.GetValueOrDefault(grpfilters[0].SearchKey, defaultType);
                        if (dbtype == defaultType)
                        {
                            dbtype = paramTypes.FirstOrDefault(x => x.Key.ToEquals(grpfilters[0].SearchKey)).GetValueOrDefault(defaultType);
                        }
                        if (ValidateSqlCommandParams(grpfilters[0].SearchValue, dbtype))
                        {
                            var firCond = grpfilters[0];
                            express = $" {firCond.SearchKey}{CreateCompFlag(firCond.Relations)}" +
                                $"@{firCond.SearchKey.ReplaceStr(".", "_")}{firCond.Orders} ";
                        }
                        foreach (var item in grpfilters.Skip(1))
                        {
                            var sdbtype = paramTypes.GetValueOrDefault(item.SearchKey, defaultType);
                            if (dbtype == defaultType)
                            {
                                dbtype = paramTypes.FirstOrDefault(x => x.Key.ToEquals(item.SearchKey)).GetValueOrDefault(defaultType);
                            }
                            if (ValidateSqlCommandParams(item.SearchValue, sdbtype))
                            {
                                if (!string.IsNullOrEmpty(express))
                                    express += $" {item.ConnLogic ?? EnConnectLogics.And}";
                                express += $" {item.SearchKey}{CreateCompFlag(item.Relations)}" +
                                    $"@{item.SearchKey.ReplaceStr(".", "_")}{item.Orders} ";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(express))
                    {
                        if (grpfilters.Count > 1)
                            conditions += $" {ConnLogic} ({express})";
                        else
                            conditions += $" {ConnLogic} {express}";
                    }
                }
            }
            return conditions.Trim().Trim($"{ConnLogic}".ToCharArray()).Trim();
        }

        public static SqlCommand CreateSqlCommand(
            string selectCommand, SearchModel searcher, Dictionary<string, SqlDbType> paramTypes, SqlDbType defaultType = SqlDbType.NVarChar)
        {
            var command = selectCommand;
            var filters = CreateFilters(searcher.filters, paramTypes, defaultType, searcher.filtersConnLogic);
            command += string.IsNullOrEmpty(filters) ? ""
                : selectCommand.IndexOf("where", StringComparison.OrdinalIgnoreCase) > -1 ? $" and ({filters}) " : $" where {filters}";

            var com = new SqlCommand(command);
            com = AddSqlCommandParams(com, searcher.filters, paramTypes, defaultType);
            return com;
        }
        public static SqlCommand CreateSqlPagedCommand(
            string selectCommand, SearchModel searcher, Dictionary<string, SqlDbType> paramTypes
            , string defaulOrderbyColumn, SqlDbType defaultType = SqlDbType.NVarChar)
        {
            var command = selectCommand;
            var filters = CreateFilters(searcher.filters, paramTypes, defaultType, searcher.filtersConnLogic);
            command += string.IsNullOrEmpty(filters) ? ""
                : selectCommand.IndexOf("where", StringComparison.OrdinalIgnoreCase) > -1 ? $" and ({filters}) " : $" where {filters}";
            command += $" order by {CreateOrderbies(searcher.sort, defaulOrderbyColumn)}";
            command += PagingCommand(searcher.page, searcher.pageSize);

            var com = new SqlCommand(command);
            com = AddSqlCommandParams(com, searcher.filters, paramTypes, defaultType);
            return com;
        }
        protected static string PagingCommand(int page, int pageSize)
        {
            return $" offset {(page - 1) * pageSize} rows FETCH NEXT {pageSize} rows only";
        }
    }
}
