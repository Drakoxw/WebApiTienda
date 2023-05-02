using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiTienda.Models;
using WebApiTienda.Models.Interfaces;

namespace WebApiTienda.Utils
{
    public class Token
    {
        private IHeaderDictionary _headers;
        private HttpContext _httpcontext;
        private IConfiguration _config;

        public Token(IHeaderDictionary headers, HttpContext context) {
            _headers = headers;
            _httpcontext = context;
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
        }

        public ResponseLogin CreateLogin(UsuariosModel user)
        {
            string pass = _config.GetValue<string>("Jwt:Key");
            string ip = GetIp(); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pass));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var date = DateTime.Now.AddHours(5);

            var claims = new[]
            {
                new Claim("userName", user.Nombre),
                new Claim("role", user.Role),
                new Claim("user", user.User),
                new Claim("userId", Convert.ToString(user.Id)),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                                             issuer: ip,
                                             audience: Aud(),
                                             claims,
                                             expires: date,
                                             signingCredentials: credentials);

            long expires = ConvertToTimestamp(date);
            string strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseLogin(user.Nombre, user.Role, user.Id, expires, strToken);
        }

        private long ConvertToTimestamp(DateTime value)
        {
            DateTime timeInit = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (value - timeInit);
            return (long)Convert.ToDouble(span.TotalSeconds);
        }

        public string GetIp()
        {
            var ipAddress = _httpcontext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            string ip = ipAddress?.ToString() ?? "";
            return ip;
        }

        private string Aud()
        {
            string ip = GetIp();
            string host = _headers["Host"].ToString() ?? "";
            string userAget = _headers["User-Agent"].ToString() ?? "";
            string origin = _headers["Origin"].ToString() ?? "";

            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(host + userAget + origin + ip)));
        }

        public bool ValidarOrigen(string audToken)
        {
            return audToken != Aud();
        }

        public TokenInterface? GetDataToken()
        {
            var identity = _httpcontext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                string roleToken = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == "role")?.Value ?? "");
                if (roleToken == "")
                {
                    roleToken = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "");
                }

                TokenInterface dataToken = new TokenInterface
                {
                    userName = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == "userName")?.Value ?? ""),
                    userId   = Convert.ToInt32(userClaims.FirstOrDefault(c => c.Type == "userId")?.Value),
                    user     = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == "user")?.Value ?? ""),
                    role     = roleToken,
                    exp      = Convert.ToInt64(userClaims.FirstOrDefault(c => c.Type == "exp")?.Value ?? ""),
                    iss      = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == "iss")?.Value ?? ""),
                    aud      = Convert.ToString(userClaims.FirstOrDefault(c => c.Type == "aud")?.Value ?? ""),
                    isAdmin  = roleToken == "SUPER-ADMIN" || roleToken == "SUPER-ADMIN",
                    isSuperAdmin  = roleToken == "SUPER-ADMIN"
                };
                return dataToken;
            }
            return null;
        }
    }
}
