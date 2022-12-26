using System;

namespace Business.Entities.Employee
{
    public class EmployeeProfileImage
    {
        public int EmployeeProfileImageID { get; set; }
        public int EmployeeID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public DateTime CreatedOrModifiedDate { get; set; }
    }
}
