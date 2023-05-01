using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTienda.Models;

namespace WebApiTienda.Utils
{
    public class Token
    {

        public ResponseLogin CreateLogin(UsuariosModel user)
        {
            string pass = "402yTrEm^bEPZgsSMwp2";
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pass));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var date = DateTime.Now.AddHours(5);

            var claims = new[]
            {
                new Claim("UserName", user.Nombre),
                new Claim("role", user.Role),
                new Claim("User", user.User),
                new Claim("UserId", Convert.ToString(user.Id)),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                                             issuer: "lorem",
                                             audience: "localhost",
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
    }
}
