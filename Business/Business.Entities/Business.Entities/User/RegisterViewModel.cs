using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class RegisterViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please select role")]
        public int RoleID { get; set; }
        [Required(ErrorMessage ="Please enter user email address")]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Please enter surname")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please enter phone number")]
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
