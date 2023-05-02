namespace WebApiTienda.Models.Interfaces
{
    public class TokenInterface
    {
        public int userId { get; set; }
        public string user { get; set; }
        public string role { get; set; }
        public string userName { get; set; }
        public long exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public bool isAdmin { get; set; }
        public bool isSuperAdmin { get; set; }
    }
}
