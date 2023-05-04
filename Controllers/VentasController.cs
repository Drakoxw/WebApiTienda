using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTienda.Utils;
using WebApiTienda.Models;
using WebApiTienda.Models.Interfaces;

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

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentasModel(int id, VentasModel ventasModel)
        {
            if (id != ventasModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(ventasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasModelExists(id))
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
        public async Task<IActionResult> DeleteVentasModel(int id)
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
