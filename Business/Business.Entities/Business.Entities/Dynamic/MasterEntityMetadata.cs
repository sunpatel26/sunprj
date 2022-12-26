namespace Business.Entities.Dynamic
{
    public class MasterEntityMetadata
    {
        public int MasterListID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public int SortOrder { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsActive { get; set; }

        public short EntryTypeID { get; set; }
        public string EntryTypeName { get; set; }
        public bool IsDefaultSelected { get; set; }
        public EntityDatabaseSetupMetadata Database { get; set; }
    }
}
