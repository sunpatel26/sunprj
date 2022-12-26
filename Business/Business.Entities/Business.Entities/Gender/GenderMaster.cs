using System;

namespace Business.Entities.Gender
{
    public class GenderMaster
    {
        public int GenderID { get; set; }
        public string GenderText { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public DateTime CreatedOrModifiedDate { get; set; }
    }
}
