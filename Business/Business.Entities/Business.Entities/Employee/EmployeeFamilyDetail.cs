using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Employee
{
    public class EmployeeFamilyDetail
    {
        public int EmployeeFamilyDetailID { get; set; }
        public int EmployeeID { get; set; }
        public int MaritalStatusID { get; set; }
        public string MaritalStatusText { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string BrotherName { get; set; }
        public string SisterName { get; set; }
        public int MotherBloodGroupID { get; set; }
        public int FatherBloodGroupID { get; set; }
        public int BrotherBloodGroupID { get; set; }
        public int SisterBloodGroupID { get; set; }
        public int WifeBloodGroupID { get; set; }
        public string FatherContact { get; set; }
        public string BrotherContact { get; set; }
        public string SisterContact { get; set; }
        public string MotherContact { get; set; }
        public string WifeName { get; set; }
        public string WifeContact { get; set; }
        public int NoofChild { get; set; }
        public int NoofBikeScooty { get; set; }
        public int NoofCar { get; set; }

        [Required(ErrorMessage = "Emergency Mobile Number is required")]
        public string EmergencyMobileNumber { get; set; }
        public string WhatsAppNo { get; set; }
        public int CreatedModifiedBy { get; set; }
    }
}
