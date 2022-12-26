namespace Business.Entities
{
    public class CompanyLogoMasterMetadata
    {
        public int CompanyLogoID { get; set; }
        public byte[] CompanyLogoImage { get; set; }
        public string CompanyLogoName { get; set; }
        public string CompanyLogoImagePath { get; set; }
        public int CompanyID { get; set; }
    }
}
