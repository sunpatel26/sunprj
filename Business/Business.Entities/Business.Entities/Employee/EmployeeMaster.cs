namespace Business.Entities.Employee
{
    public class EmployeeMaster
    {
        public int SrNo { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string GenderText { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
