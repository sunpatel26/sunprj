using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.ItemCategory
{
    public class ItemCategory
    {
        public int ItemCategoryID { get; set; }
        public string ItemCategoryText { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int CreatedOrModifiedBy { get; set; }
    }
}
