using StandardLibraryForDotNetX.UtilityHelpers;
using System.ComponentModel.DataAnnotations;

namespace Net6Test.TestGroups
{
    public class ValidationAttributeTestGroup
    {
        public async static Task AnnotationAttributeTest1()
        {
            var fts = new ForTesting();
            var results = fts.SimpleValidateProperties();
        }
    }

    [Display(Name = "ACstringClass")]
    public class ForTesting
    {
        [Display(Name = "ACstring")]
        [ArrayLengthAttribute(3, 5)]
        public string[] Strings { get; set; } = new string[] { "a", "b" };
        [ArrayLengthAttribute("ListStrings must contain at least 3 elements, and 5 elements in max.", 3, 5)]
        public List<string> ListStrings { get; set; } = new List<string>() { "a","b","c","d","e" };
        [ArrayLengthAttribute("IEnumStrings must contain at least 3 elements, and 5 elements in max.", 3, 5)]
        public IEnumerable<string> IEnumStrings { get; set; } = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h" };
    }


    public class ArrayLengthAttribute : ValidationAttribute
    {
        protected int MinCount, MaxCount;
        public ArrayLengthAttribute(int minCount, int maxCount)
        {
            MinCount = minCount; 
            MaxCount = maxCount;
            ErrorMessage = $"{{0}} must contain at least {minCount} elements, and {maxCount} elements in max.";
        }
        public ArrayLengthAttribute(string messages, int minCount, int maxCount)
            :this(minCount, maxCount)
        {
            ErrorMessage = messages ;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var iem = value as System.Collections.IList;
            if (iem == null) return ValidationResult.Success;
            if (MinCount <= iem.Count && iem.Count <= MaxCount) return ValidationResult.Success;
            return new ValidationResult(string.Format(ErrorMessage ?? "", validationContext.DisplayName));
        }
    }
}
