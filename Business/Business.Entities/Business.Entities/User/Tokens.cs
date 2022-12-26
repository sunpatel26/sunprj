namespace Business.Entities.User
{
    public class Tokens
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string MobileNo { get; set; }
        public string RefreshToken { get; set; }       
    }
}
