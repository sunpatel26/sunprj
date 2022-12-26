using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Employee
{
    public class EmployeeAddressTxn
    {
        public int SrNo { get; set; }
        public int AddressID { get; set; }
        public int? EmployeeID { get; set; }
        public int EmployeeAddressTxnID { get; set; }
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Please enter plot name and number.")]
        public string PlotNoName { get; set; }
        public string StreetNoName { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Area { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Please select State.")]
        public int StateID { get; set; }

        [Required(ErrorMessage ="Please select address type.")]
        public int AddressTypeID { get; set; }
        public string AddressType { get; set; }
        [Required(ErrorMessage = "Please enter District.")]
        public string DistrictName { get; set; }
        public string Landmark { get; set; }
        public string Street { get; set; }
        public string StateName { get; set; }
        [Required(ErrorMessage = "Please enter Taluka.")]
        public string Taluka { get; set; }
        [Required(ErrorMessage = "Please select Country.")]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public string MainAddress { get; set; }
    }
}
