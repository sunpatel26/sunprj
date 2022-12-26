using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Entities
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public byte[] Content { get; set; }
    }
}
