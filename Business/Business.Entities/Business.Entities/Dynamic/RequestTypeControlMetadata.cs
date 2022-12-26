using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class RequestTypeControlMetadata
    {
        public int RequestTypeControlID { get; set; }
        public int RequestTypeTitleID { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
        public string DataKey { get; set; }
        public bool IsActive { get; set; }
        public string AccessByRoles { get; set; }
        public short ControlValidationRuleID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Tooltip { get; set; }
        public short? MinLength { get; set; }
        public short? MaxLength { get; set; }
        public short? SortOrder { get; set; }

        public string RequestTypeName { get; set; }
        public string RequestTypeTitle { get; set; }
        public string ValidationRuleName { get; set; }
        public string ValidationRuleFormula { get; set; }
        public string ValidationRuleErrorMessage { get; set; }

    }
}
