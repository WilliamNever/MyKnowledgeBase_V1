using GridSearchModels.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace StandardLibraryForDotNetX.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class RequiredForConditionFieldAttribute : ValidationBaseAttribute
    {
        private readonly string _dependedFieldname;
        private readonly string _configuredSection;
        /// <summary>
        /// ErrorMessage has 3 parameters that will be replaced with the error field name, depended field name and the value of depended field.
        /// Default ErrorMessage is "The {0} field is required for {1} - {2}."
        /// configuredSection - "Appsettings:ConditionalValues"
        /// </summary>
        public RequiredForConditionFieldAttribute(string dependedFieldname, string configuredSection)
        {
            ErrorMessage = "The {0} field is required for {1} - {2}.";
            _configuredSection = configuredSection;
            _dependedFieldname = dependedFieldname;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var ic = validationContext.GetRequiredService<IConfiguration>();
            var validstrs = ic.GetValue<string>(_configuredSection)?.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            string? dvl = GetMemberValue(_dependedFieldname, validationContext);

            var rsl = ValidationResult.Success;
            var validValue = value?.ToString();
            if (validstrs?.Any(x => x.ToEquals(dvl)) ?? false)
            {
                var errFieldName = GetInfoNameIfExists(validationContext);
                errFieldName = string.IsNullOrEmpty(errFieldName) ? validationContext.MemberName : errFieldName;
                if (string.IsNullOrEmpty(validValue))
                {
                    var depfn = GetInfoNameIfExists(validationContext, _dependedFieldname);
                    rsl = new ValidationResult(string.Format(ErrorMessage, errFieldName,
                        string.IsNullOrEmpty(depfn) ? _dependedFieldname : depfn, $"{dvl}"));
                }
            }
            return rsl;
        }
    }
}