namespace Business.Entities.FormMasterEntitie
{
    public class FormMaster
    {
        public object SrNo { get; set; }
        public int FormID { get; set; }
        public string FormName { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; } = true;
        public int FormTypeID { get; set; }
        public string FormTypeText { get; set; }
        public int PackageID { get; set; }
        public string PackageName { get; set; }
        public int CreatedOrModifiedBy { get; set; }

    }
}
