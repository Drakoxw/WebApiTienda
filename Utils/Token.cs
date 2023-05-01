using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiTienda.Models;

namespace WebApiTienda.Utils
{
    public class Token
    {
        private IHeaderDictionary _headers;
        private HttpContext _context;

        public Token(IHeaderDictionary Headers, HttpContext context) {
            _headers = Headers;
            _context = context;
        }

        public ResponseLogin CreateLogin(UsuariosModel user)
        {
            string pass = "402yTrEm^bEPZgsSMwp2";
            var ipAddress = _context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            string ip = ipAddress?.ToString() ?? "";

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
                                             audience: Aud(ip),
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

        private string Aud(string ip)
        {
            string host = _headers["Host"].ToString() ?? "";
            string userAget = _headers["User-Agent"].ToString() ?? "";
            string origin = _headers["Origin"].ToString() ?? "";

            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(host + userAget + origin + ip)));
        }
    }
}
