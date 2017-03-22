using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClientProject.Utils
{
    /// <summary>
    /// Valid if the E-Mail is valid.
    /// </summary>
    public class CustomValidationEmailAttribute : ValidationAttribute
    {
        public CustomValidationEmailAttribute()
        {
        }
        public override bool IsValid(object value)
        {
            Regex regEx = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", RegexOptions.IgnoreCase);

            return regEx.IsMatch(value.ToString());
        }
    }
}