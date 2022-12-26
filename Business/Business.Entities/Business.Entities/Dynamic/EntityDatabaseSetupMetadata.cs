using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Dynamic
{
    public class EntityDatabaseSetupMetadata
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string Label { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }

        public string CaseTypeRole_DatabaseName { get; set; }
        public string CaseTypeRole_TableName { get; set; }
        public string CaseTypeRole_ValueField { get; set; }
        public string CaseTypeRole_TextField { get; set; }

        public string AssignedTo_DatabaseName { get; set; }
        public string AssignedTo_TableName { get; set; }
        public string AssignedTo_ValueField { get; set; }
        public string AssignedTo_TextField { get; set; }
    }
}
