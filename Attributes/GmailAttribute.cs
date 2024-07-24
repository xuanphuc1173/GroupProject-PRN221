using System.ComponentModel.DataAnnotations;

namespace Attributes
{
    public class GmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();
                if (email.EndsWith("@gmail.com"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Tên người dùng phải kết thúc bằng @gmail.com");
        }
    }
}
