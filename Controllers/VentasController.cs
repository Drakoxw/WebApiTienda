using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTienda.Utils;
using WebApiTienda.Models;
using WebApiTienda.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApiTienda.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppContextDB _context;

        public VentasController(AppContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentasModel>>> GetVentas()
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            return await _context.Ventas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentasModel>> GetVentasModel(int id)
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            var ventasModel = await _context.Ventas.FindAsync(id);

            if (ventasModel == null)
            {
                return NotFound();
            }

            return ventasModel;
        }

        [HttpGet("fullData/{id}")]
        public async Task<ActionResult<ResponseApi<List<VentasFullData>>>> GetVentasModelFull(int id)
        {
            var ventasModel = await _context.Ventas.FindAsync(id);

            if (ventasModel == null)
            {
                return NotFound();
            }

            var data = (from A in _context.ItemVenta
                        join B in _context.Productos on A.ProductoId equals B.Id
                              where A.VentaId == id
                              select  new {
                                  ventaId = A.VentaId,
                                  itemId = A.Id,
                                  productoId = A.ProductoId,
                                  descProduct = B.Descripcion,
                                  numeroItems = A.NumeroItems,
                                  precio = B.Precio,
                                  total = A.NumeroItems * B.Precio
                              }).Take(100);

            if (data.Count() > 0)
            {
                return StatusCode(200, new ResponseApi<IQueryable>(data, "ok", "data found"));
            }

            return NoContent();

        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseApi<string>>> PostVentasModel(VentaInterface ventaNeta)
        {
            VentasModel venta = new VentasModel
            {
                Total = ventaNeta.Total,
                ClienteId = ventaNeta.ClienteId
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            foreach (var item in ventaNeta.Items)
            {
                _context.ItemVenta.Add(new ItemVenta { 
                    NumeroItems = item.NumeroItems,
                    VentaId = venta.Id,
                    ProductoId = item.ProductoId,
                });
            }

            await _context.SaveChangesAsync();

            return StatusCode(201, new ResponseApi<string>("", "ok", "created"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVentasModel(int id)
        {
            Token TokenUtil = new Token(Request.Headers, HttpContext);
            var dataToken = TokenUtil.GetDataToken();

            if (TokenUtil.ValidarOrigen(dataToken.aud) == false)
            {
                return StatusCode(401, new ResponseApi<string>("", "error", "Origen de token desconocido"));
            }

            if (dataToken.isAdmin == false)
            {
                return StatusCode(403, new ResponseApi<string>(dataToken.role, "error", "No autorizado"));
            }

            var ventasModel = await _context.Ventas.FindAsync(id);
            if (ventasModel == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(ventasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentasModelExists(int id)
        {
            return (_context.Ventas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
