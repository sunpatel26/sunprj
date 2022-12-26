namespace Business.Entities.Employee
{
	public class EmployeeBankDetails
    {
        public int SrNo { get; set; }
        public int EmployeeBankDetailsID { get; set; }
        public int EmployeeID { get; set; }
        public string BankName { get; set; }
        public string EmpNameAsperBank { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNO { get; set; }
        public string BranchLocation { get; set; }
        public string City { get; set; }
        public string BankCode { get; set; }
        public string BICSwiftCode { get; set; }
        public int CountryID { get; set; }
        public bool IsDefaultBankAccount { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
