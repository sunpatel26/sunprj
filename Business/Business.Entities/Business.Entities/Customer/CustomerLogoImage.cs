namespace Business.Entities.Customer
{
    public class CustomerLogoImage
    {
        public int CustomerLogoImageID { get; set; }
        public int CustomerID { get; set; }
        public string LogoImageName { get; set; }
        public string LogoImagePath { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedOrModifiedBy { get; set; }
    }
}
