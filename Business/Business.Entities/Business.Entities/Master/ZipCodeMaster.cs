using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class ZipCodeMaster
    {
        public int ZipCodeID { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Taluko { get; set; }
        public string Country { get; set; }
    }
}
