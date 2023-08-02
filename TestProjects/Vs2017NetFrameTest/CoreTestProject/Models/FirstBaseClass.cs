using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CoreTestProject.Models
{
    public class FirstBaseClass
    {
        [MutipleRange(3, 5, EnWeekDay.Sunday, "{0} {1} {2} is wrong in Base.")]
        public virtual int ATAge { get; set; }
    }

    public class FirstBaseClassEx : FirstBaseClass, IValidatableObject
    {
        [MutipleRange(3, 5, EnWeekDay.Sunday, "{0} {1} {2} is wrong in Base Ex ---.")]
        public override int ATAge { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            //bool isValid = Validator.TryValidateObject(this,
            //    validationContext   //new ValidationContext(this)
            //, results);
            bool isValid = Validator.TryValidateProperty(this.ATAge,
                   new ValidationContext(this) { MemberName = nameof(ATAge) },
                   results);
            //foreach (var rsl in results)
            if (!isValid)
            {
                var err = new ValidationResult(
                    "- " + string.Join("\r\n - ", results.Select(x => x.ErrorMessage))
                    , results.SelectMany(x => x.MemberNames).Distinct()
                    );
                yield return err;
            }
            else yield return null;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class MutipleRangeAttribute : RangeAttribute
    {
        private EnWeekDay WeekDay;
        private string ErrorPattern;
        public MutipleRangeAttribute(int minimum, int maximum, EnWeekDay weekday, string ErrorPattern)
            : base(minimum, maximum)
        {
            WeekDay = weekday;
            this.ErrorPattern = ErrorPattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            if (value == null) return new ValidationResult(string.Format(ErrorPattern, TypeId.ToString(), validationContext.MemberName, "NULL"));

            int tmp = (int)value;
            if (WeekDay == EnWeekDay.Sunday)
            {
                result = base.IsValid(value, validationContext);
                if (result != ValidationResult.Success)
                {
                    result.ErrorMessage = string.Format(ErrorPattern, TypeId.ToString(), validationContext.MemberName, value.ToString());
                    //validationContext.Items.Add(new KeyValuePair<object, object>("err-"+validationContext.MemberName, value));
                }
            }
            return result;
        }
        private object typeid;
        public override object TypeId
        {
            get
            {
                typeid = typeid ?? WeekDay.ToString();
                return typeid;
            }
        }
    }

    public enum EnWeekDay
    {
        Sunday = 0,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }
}
