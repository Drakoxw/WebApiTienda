using Microsoft.AspNetCore.Mvc;
using WebApiTienda.Models;
using WebApiTienda.Utils;

namespace WebApiTienda.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly AppContextDB _context;

        public LoginController(AppContextDB context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<ResponseApi<ResponseLogin>> Login(LoginModel payload)
        {
            try
            {
                if (payload == null || payload.Password == "" || payload.User == "")
                {
                    return BadRequest(new ResponseApi<string>("", "error", "Se requiere usuario y contraseña"));
                }

                #pragma warning disable CS8600
                UsuariosModel user = _context.Usuarios.Where(
                    user => user.User == payload.User 
                    && user.Password == payload.Password
                ).FirstOrDefault();

                if (user == null)
                {
                    return NotFound(new ResponseApi<string>("", "error", "usuario o contraseña incorrectos"));
                }

                Token TokenUtil = new Token(Request.Headers, HttpContext);
                ResponseLogin credentials = TokenUtil.CreateLogin(user);

                return StatusCode(200, new ResponseApi<ResponseLogin>(credentials, "ok", "Usuario autenticado"));

            }
            catch (Exception e)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", e.Message ));
            }

        }

    }
}
