using System;

namespace Business.Entities
{
    public class ErrorMetadata
    {
        public DateTime ErrorDateTime { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
    }
}
