namespace Business.Entities.OurProduct
{
    public class OurProduct
    {
        public int ItemID { get; set; }
        public string ItemText { get; set; }
        public int UOMID { get; set; }
        public string UOMText { get; set; }
        public string Quantity { get; set; }
        public string Descrription { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public int SrNo { get; set; }
    }
}
