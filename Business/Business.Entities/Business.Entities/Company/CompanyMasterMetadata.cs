using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class CompanyMasterMetadata
    {
        public int CompanyRegistrationID { get; set; }
        public int CompanyLogoID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Please enter company name")]
        public string CompanyName { get; set; }

        public string CompanyWebsiteText { get; set; }

              
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please choose company logo")]
        [NotMapped]
        [Display(Name ="Upload File")]
        public IFormFile ImageFile { get; set; }
        public string CompanyLogoImagePath{get; set; }
        public string CompanyLogoName { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^[\w-+'$""]+(\.[\w-']+)*@([a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*?\.[a-zA-Z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$",ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and compare password does not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select business type")]
        public int BusinessTypeID { get; set; }
        [Required(ErrorMessage = "Please select industry type")]
        public int IndustryTypeID { get; set; }
        public string CompanyGroupName { get; set; }
        public string UnitName { get; set; }
       
        public string PANNo { get; set; }
        public string GSTINNo { get; set; }
        public string GSTINType { get; set; }
        public string FactoryLicenseNo { get; set; }
        public string FactoryRegNo { get; set; }
        public string ARNNo { get; set; }
        public string ECCNo { get; set; }
        public string MSMENo { get; set; }
        public string SSINo { get; set; }
        public string TANNo { get; set; }
        public string ExportNo { get; set; }
        public string ImportNo { get; set; }
        public string TaxRange { get; set; }
        public string TaxDivisio { get; set; }
        public string TaxCommisionerate { get; set; }
    }
}
