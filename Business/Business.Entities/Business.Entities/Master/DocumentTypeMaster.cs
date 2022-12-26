namespace Business.Entities.Master
{
    public class DocumentTypeMaster
    {
        public int SrNo { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
