using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StandardLibraryForDotNetX.UtilityHelpers
{
    [ExcludeFromCodeCoverage]
    public static class ValidationHelper
    {
        public static bool IsValid<T>(this T obj, out string[] errors) where T : class
        {
            var listResults = ReflectValidateProperties(obj);
            var isValid = !listResults.Any();

            errors = listResults.Select(x => x?.ErrorMessage ?? "")
                .Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            return isValid;
        }
        public static List<ValidationResult> ReflectValidateProperties<T>(this T? inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (inm == null) return results;

            var type = inm.GetType();
            if (type.Equals(typeof(string)) || type.IsValueType)
                return results;

            var Properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => x.PropertyType.IsClass && !typeof(string).Equals(x.PropertyType)).ToList()
                ;
            foreach (var prop in Properties)
            {
                var childObject = prop.GetValue(inm);
                if (childObject is IList childObjects)
                    foreach (var obj in childObjects)
                        results.AddRange(ReflectValidateProperties(obj));
                else if (childObject is IEnumerable IEChildren)
                {
                    var ietor = IEChildren.GetEnumerator();
                    while (ietor.MoveNext())
                        results.AddRange(ReflectValidateProperties(ietor.Current));
                }
                else
                    results.AddRange(ReflectValidateProperties(childObject));
            }
            ValidationContext context = new ValidationContext(inm);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);
            return results;
        }
        public static List<ValidationResult> SimpleValidateProperties<T>(this T inm) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(inm);
            bool isValid = Validator.TryValidateObject(inm, context, results, true);
            return results;
        }
    }
}
