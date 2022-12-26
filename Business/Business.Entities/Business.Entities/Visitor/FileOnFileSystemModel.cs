using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class FileOnFileSystemModel
    {
        public int VisitorMeetingRequestFileID { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FilePath { get; set; }
    }
}
