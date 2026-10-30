using GridSearchModels.Helpers;
using System.ComponentModel.DataAnnotations;

namespace StandardLibraryForDotNetX.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumValueValidationAttribute : ValidationBaseAttribute
    {
        private readonly Type _type;
        public bool IgnoreCases { get; set; } = false;
        public EnumValueValidationAttribute(Type type)
        {
            _type = type;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var infoName = GetInfoNameIfExists(validationContext);
            infoName = string.IsNullOrEmpty(infoName) ? validationContext.MemberName : infoName;

            var itmStr = value.ToString();
            var names = Enum.GetNames(_type);
            var rsl = names.Any(x => x.ToEquals(itmStr, IgnoreCases));
            return rsl ? ValidationResult.Success : new ValidationResult(
                string.IsNullOrEmpty(ErrorMessage) ?
                $"The {infoName} field allowed values: {string.Join(", ", names)}." : ErrorMessage
                );
        }
    }
}
