using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Enums
{
    public enum ActivityType
    {
        [Description("Created Case")]
        CreatedCase = 1,

        [Description("View Case")]
        ViewCase = 2,

        [Description("Assign Case")]
        AssignCase = 3,

        [Description("Edit Case")]
        EditCase = 4,

        [Description("Close Case")]
        CloseCase = 5
    }
}
