using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTienda.Models;

namespace WebApiTienda.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppContextDB _context;

        public UsuariosController(AppContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosModel>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosModel>> GetUsuariosModel(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuariosModel = await _context.Usuarios.FindAsync(id);

            if (usuariosModel == null)
            {
                return NotFound();
            }

            return usuariosModel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuariosModel(int id, UsuariosModel usuariosModel)
        {
            if (id != usuariosModel.Id)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UsuariosModel>> PostUsuariosModel(UsuariosModel usuariosModel)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'AppContextDB.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuariosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuariosModel", new { id = usuariosModel.Id }, usuariosModel);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuariosModel(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuariosModel = await _context.Usuarios.FindAsync(id);
            if (usuariosModel == null)
            {
                return NotFound();
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
