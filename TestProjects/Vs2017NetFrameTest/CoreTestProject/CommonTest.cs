using CoreTestProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CoreTestProject
{
    public class CommonTest
    {
        [Fact]
        public void TestValidation_Basic()
        {
            var mtm = new FirstBaseClassEx
            {
                ATAge = 8
            };

            ValidationContext context = new ValidationContext(mtm);
            List<ValidationResult> results = new List<ValidationResult>();

            //context.MemberName = "ATAge";
            //context.DisplayName = "ATAge";

            //bool isValid = Validator.TryValidateProperty(mtm.ATAge, context, results);
            bool isValid = Validator.TryValidateObject(mtm, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void TestValidation()
        {
            var mtm = new List<FirstBaseClassEx>{
                new FirstBaseClassEx
                    {
                        ATAge = 4
                    },
                new FirstBaseClassEx
                    {
                        ATAge = 8
                    }
            };

            ValidationContext context = new ValidationContext(mtm);
            var tt = context.ObjectType;
            List<ValidationResult> results = new List<ValidationResult>();

            foreach (var itm in mtm)
            {
                ValidationContext context1 = new ValidationContext(itm);
                bool isValid = Validator.TryValidateObject(itm, context1, results, false);
            }

            //var messs = mtm.Validate(null);

            //Assert.True(isValid);
        }
    }
}
