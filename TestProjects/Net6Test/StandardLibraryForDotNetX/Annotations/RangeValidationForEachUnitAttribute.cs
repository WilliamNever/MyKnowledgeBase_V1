using GridSearchModels.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StandardLibraryForDotNetX.SettingsAndModels;
using System.ComponentModel.DataAnnotations;

namespace StandardLibraryForDotNetX.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RangeValidationForEachUnitAttribute : ValidationBaseAttribute
    {
        private readonly string _dependentFieldname;
        private readonly string _configuredSection;
        private readonly string _prefix;
        /// <summary>
        /// ErrorMessage has 5 parameters that will be replaced.
        /// Default ErrorMessage is "The value of {0} field must be between {1} and {2} for {3} - {4}."
        /// configuredSection - "Appsettings:Dimesions"
        /// </summary>
        public RangeValidationForEachUnitAttribute(string dependentFieldname, string configuredSection, string prefix = "")
        {
            ErrorMessage = "The value of {0} field must be between {1} and {2} for {3} - {4}.";
            _configuredSection = configuredSection;
            _dependentFieldname = dependentFieldname;
            _prefix = prefix;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var rsl = ValidationResult.Success;
            if (value is null)
                return rsl;

            string? dpvl = GetMemberValue(_dependentFieldname, validationContext);
            var ic = validationContext.GetRequiredService<IConfiguration>();
            var dimension = ic.GetSection(_configuredSection).Get<MinMaxValue[]>()?.FirstOrDefault(
                x => x.Unit.ToEquals(dpvl) && x.Key.ToEquals($"{_prefix}{validationContext.MemberName}"));


            if (dimension == null || !TryGetDouble(value, out double validValue))
            {
                return rsl;
            }

            if (!(dimension.Min <= validValue && validValue <= dimension.Max))
            {
                var errFieldName = GetInfoNameIfExists(validationContext);
                errFieldName = string.IsNullOrEmpty(errFieldName) ? validationContext.MemberName : errFieldName;
                var depfn = GetInfoNameIfExists(validationContext, _dependentFieldname);
                rsl = new ValidationResult(string.Format(ErrorMessage,
                    errFieldName, dimension.Min, dimension.Max,
                    string.IsNullOrEmpty(depfn) ? _dependentFieldname : depfn, dpvl));
            }
            return rsl;
        }
    }
}