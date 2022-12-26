using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class AddUpdateEmployee
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Employee code can not be null.")]
        public string EmployeeCode { get; set; }
        public string PrefixName { get; set; }

        [Required(ErrorMessage = "First name can not be null.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle name can not be null.")]
        public string MiddleName { get; set; }
        
        [Required(ErrorMessage = "Last name can not be null.")]
        public string LastName { get; set; }

        public int GenderID { get; set; }
        public int EmployeeBloodGroupID { get; set; }
        public bool IsActive { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentID { get; set; }
        public int DesignationID { get; set; }
        public int ReportingTo { get; set; }

        [Required(ErrorMessage = "Personal mobile no. can not be null.")]
        public string PersonalMobileNo { get; set; }
        
        [Required(ErrorMessage = "Office mobile no. can not be null.")]
        public string OfficeMobileNo { get; set; }

        [Required(ErrorMessage = "Alternative mobile no. can not be null.")]
        public string AlternativeMobileNo { get; set; }
        public bool IsResigned { get; set; }
        public string Note { get; set; }
        public int EmailGroupID { get; set; }

        [Required(ErrorMessage = "Personal email can not be null.")]
        [EmailAddress(ErrorMessage ="Enter valid EmailID.")]
        public string PersonalEmail { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid EmailID.")]
        [Required(ErrorMessage = "Office email can not be null.")]
        public string OfficeEmail { get; set; }


        [Required(ErrorMessage = "Select Birthdate")]
        public DateTime BirthDate { get; set; }
        public string Religion { get; set; }
        public int CompanyID { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

    }
}
