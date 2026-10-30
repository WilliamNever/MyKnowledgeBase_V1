using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StandardLibraryForDotNetX.Annotations
{
    public class ValidationBaseAttribute : ValidationAttribute
    {
        protected static string? GetInfoNameIfExists(ValidationContext validationContext, string? memberName = null)
        {
            if (string.IsNullOrEmpty(memberName?.Trim()))
                memberName = validationContext.MemberName;

            if (string.IsNullOrEmpty(memberName?.Trim()))
                return null;


            var member = validationContext.ObjectType.GetMember(
                memberName,
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance);

            var infoNameAttr = member?.FirstOrDefault()?
                .GetCustomAttributes(typeof(InforNameAttribute), false)?
                .FirstOrDefault() as InforNameAttribute;

            return infoNameAttr?.Name?.Trim();
        }

        protected static string? GetMemberValue(string memberName, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(memberName, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var field = validationContext.ObjectType.GetField(memberName, System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            string? dvl = null;
            if (property != null)
            {
                dvl = property?.GetValue(validationContext.ObjectInstance)?.ToString();
            }
            else if (field != null)
            {
                dvl = field?.GetValue(validationContext.ObjectInstance)?.ToString();
            }
            return dvl?.Trim();
        }

        protected static bool TryGetDouble(object value, out double parsed)
        {
            if (value is double d)
            {
                parsed = d;
                return true;
            }

            return double.TryParse(
                       value.ToString(),
                       NumberStyles.Float | NumberStyles.AllowThousands,
                       CultureInfo.InvariantCulture,
                       out parsed)
                   || double.TryParse(
                       value.ToString(),
                       NumberStyles.Float | NumberStyles.AllowThousands,
                       CultureInfo.CurrentCulture,
                       out parsed);
        }
    }
}
