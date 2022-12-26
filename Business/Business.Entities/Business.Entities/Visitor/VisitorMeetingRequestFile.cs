namespace Business.Entities
{
    public class VisitorMeetingRequestFile
    {
        public int VisitorMeetingRequestFileID { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Name { get; set; }
        public int VisitorMeetingRequestID { get; set; }
    }
}
