namespace WebApiTienda.Utils
{
    public class ResponseLogin
    {
        private string username;
        private string role;
        private int id;
        private int exp;
        private string token;

        public ResponseLogin(string username, string role, int id, int exp, string token)
        {
            this.username = username;
            this.role = role;
            this.id = id;
            this.exp = exp;
            this.token = token;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Role
        {
            get { return role; }    
            set { role = value; }
        }

        public int Id { 
            get { return id; } 
            set {  id = value; } 
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public string Token
        { 
            get { return token; }
            set { token = value; } 
        }
    }
}
