using Microsoft.AspNetCore.Http;
using System;

namespace Business.Entities.Customer
{
    public class CustomerMaster
    {
        public int CustomerID { get; set; }
        public string UnitNoName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string GroupName { get; set; }
        public string OwnerName { get; set; }
        public string ContactPhone1 { get; set; }
        public string Mobile1 { get; set; }
        public string FaxNo { get; set; }
        public int? IndustryTypeID { get; set; }
        public int? BusinessTypeID { get; set; }
        public string LogoImagePath { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
        public IFormFile LogoImage { get; set;}
        public string LogoImageName { get; set; }
    }
}
