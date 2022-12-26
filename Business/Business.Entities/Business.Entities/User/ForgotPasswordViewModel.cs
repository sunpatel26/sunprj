using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
