using StandardLibraryForDotNetX.Annotations;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StandardLibraryForDotNetX.UtilityHelpers.Complex
{
    [ExcludeFromCodeCoverage]
    public static class ValidationHelper
    {
        private static IServiceProvider? _Provider { get; set; }
        public static void Init(IServiceProvider provider)
        {
            _Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        private static IServiceProvider GetServiceProvider() 
            => _Provider ?? throw new ArgumentNullException($"Failed to init ValidationContext for Null of Provider");
        public static bool IsValid<T>(this T obj, out string[] errors) where T : class
        {
            var listResults = obj.ReflectValidateProperties();
            var isValid = !listResults.Any();

            errors = listResults.Select(x => x?.ErrorMessage ?? "")
                .Where(x => !string.IsNullOrEmpty(x.Trim()))
                .Select(x => {
                    var item = x;
                    if (x.StartsWith("."))
                    {
                        item = $"In {x.TrimStart('.')}";
                    }
                    return item;
                }).ToArray();
            return isValid;
        }
        private static List<ValidationResult> ReflectValidateProperties<T>(this T? inm, string propertyName = "") where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (inm == null) return results;

            var type = inm.GetType();
            if (type.Equals(typeof(string)) || type.IsValueType)
                return results;

            var Properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsClass && !typeof(string).Equals(x.PropertyType)).ToList()
                ;
            foreach (var prop in Properties)
            {
                var prpName = prop.Name;
                var infAttr = prop.GetCustomAttribute<InforNameAttribute>();
                if (infAttr != null)
                {
                    prpName = infAttr.Name;
                }
                var childObject = prop.GetValue(inm);
                if (childObject is IList childObjects)
                {
                    int idx = 0;
                    foreach (var obj in childObjects)
                    {
                        results.AddRange(obj.ReflectValidateProperties($"{prpName}[{idx}]"));
                        idx++;
                    }
                }
                else if (childObject is IEnumerable IEChildren)
                {
                    int idx = 0;
                    var ietor = IEChildren.GetEnumerator();
                    while (ietor.MoveNext())
                    {
                        results.AddRange(ietor.Current.ReflectValidateProperties($"{prpName}[{idx}]"));
                        idx++;
                    }
                }
                else
                    results.AddRange(childObject.ReflectValidateProperties(prpName));
            }
            ValidationContext context = new ValidationContext(inm, GetServiceProvider(), null);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);

            #region
            if (!string.IsNullOrEmpty(propertyName))
            {
                results.ForEach(x =>
                {
                    if (x.ErrorMessage?.Any() ?? false)
                    {
                        var msg = x.ErrorMessage.Length > 1 ?
                            $"{x.ErrorMessage[0].ToString().ToLower()}{x.ErrorMessage[1..]}" : x.ErrorMessage;
                        x.ErrorMessage = $".{propertyName}"
                            + (msg.StartsWith(".") ? $"{msg}" : $", {msg}")
                           ;
                    }
                });
            }
            #endregion

            return results;
        }
        public static List<ValidationResult> SimpleValidateProperties<T>(this T inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(inm, GetServiceProvider(), null);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);
            return results;
        }
    }
}
