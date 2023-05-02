using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTienda.Models;
using WebApiTienda.Models.Interfaces;
using WebApiTienda.Utils;

namespace WebApiTienda.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly AppContextDB _context;

        public UsuariosController(AppContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuariosModel>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NoContent();
            }
            List<UsuariosModel> list = await _context.Usuarios.ToListAsync();
            return StatusCode(200, new ResponseApi<List<UsuariosModel>>(list, "ok", "lista de usuarios"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseApi<UsuariosModel>>> GetUsuariosModel(int id)
        {
            if (_context.Usuarios == null)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario"));
            }
            var usuariosModel = await _context.Usuarios.FindAsync(id);

            if (usuariosModel == null)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario"));
            }

            return StatusCode(400, new ResponseApi<UsuariosModel>(usuariosModel, "ok", "Usuario encontrado"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuariosModel(int id, UsuariosModel usuariosModel)
        {
            if (id != usuariosModel.Id)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario"));
            }

            _context.Entry(usuariosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosModelExists(id))
                {
                    return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi<UsuariosModel>>> PostUsuariosModel(UsuariosModel usuariosModel)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppContextDB.Usuarios'  is null.");
            }
            _context.Usuarios.Add(usuariosModel);
            await _context.SaveChangesAsync();

            return StatusCode(201, new ResponseApi<UsuariosModel>(usuariosModel, "ok", "Usuario creado"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuariosModel(int id)
        {
            Token TokenUtil = new Token(Request.Headers, HttpContext);
            var dataToken = TokenUtil.GetDataToken();

            if (TokenUtil.ValidarOrigen(dataToken.aud) == false)
            {
                return StatusCode(401, new ResponseApi<string>("", "error", "Origen de token desconocido"));
            }

            if (dataToken.isAdmin != false)
            {
                return StatusCode(403, new ResponseApi<TokenInterface>(dataToken, "error", "No autorizado"));
            }

            if (_context.Usuarios == null)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario")); ;
            }
            var usuariosModel = await _context.Usuarios.FindAsync(id);
            if (usuariosModel == null)
            {
                return StatusCode(400, new ResponseApi<string>("", "error", "No se encontro el usuario")); ;
            }

            _context.Usuarios.Remove(usuariosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosModelExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
