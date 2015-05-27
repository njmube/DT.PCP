using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DT.PCP.Web.Core
{
    public class IntLengthAttribute:ValidationAttribute
    {
        private readonly int _passedValue;
        public IntLengthAttribute(int value)
        {
            _passedValue = value;
        }

        public override bool IsValid(object value)
        {
            var countOfDigits = value.ToString().Length;
            return _passedValue == countOfDigits;
        }
    }
}
