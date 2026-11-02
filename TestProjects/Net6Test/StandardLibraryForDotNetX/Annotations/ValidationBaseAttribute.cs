using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace StandardLibraryForDotNetX.Annotations
{
    public class ValidationBaseAttribute : ValidationAttribute
    {
        /// <summary>
        /// get the value of InfoName attribute from a Property or Field
        /// </summary>
        /// <param name="validationContext"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        protected static string? GetInfoNameIfExists(ValidationContext validationContext, string? memberName = null)
        {
            if (string.IsNullOrEmpty(memberName?.Trim()))
                memberName = validationContext.MemberName;

            if (string.IsNullOrEmpty(memberName?.Trim()))
                return null;

            var member =
                GetMemberInfos(validationContext.ObjectType, memberName, MemberTypes.Property | MemberTypes.Field
                , BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.FirstOrDefault();

            var infoNameAttr = member?
                .GetCustomAttributes(typeof(InforNameAttribute), false)?
                .FirstOrDefault() as InforNameAttribute;

            return infoNameAttr?.Name?.Trim();
        }

        private static MemberInfo[] GetMemberInfos(
            Type type, string memberName, MemberTypes memberTypes, BindingFlags bindingFlags) =>
            type.GetMember(memberName, memberTypes, bindingFlags);

        /// <summary>
        /// get the value from a Property or Field member 
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected static string? GetMemberValue(string memberName, ValidationContext validationContext)
        {
            var member =
                GetMemberInfos(validationContext.ObjectType, memberName, MemberTypes.Property | MemberTypes.Field
                , BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.FirstOrDefault();

            string? dvl = null;
            if (member is null) return dvl?.Trim();

            if (member is PropertyInfo property)
            {
                dvl = property?.GetValue(validationContext.ObjectInstance)?.ToString();
            }
            else if (member is FieldInfo field)
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
