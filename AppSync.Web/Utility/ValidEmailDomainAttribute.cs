using System.ComponentModel.DataAnnotations;

namespace AppSync.Web.Utility
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false; // or handle the null case as needed
            }

            string[]? strings = value.ToString()?.Split('@');

            if (strings == null || strings.Length != 2)
            {
                return false; // The email address should have exactly one "@" symbol or it might be null.
            }

            if (strings[1]?.ToUpper() == allowedDomain.ToUpper())
            {
                return true;
            }
            return false;
        }

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
    }
}
