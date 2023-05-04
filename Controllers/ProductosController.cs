using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiTienda.Models;
using System.Net;
using WebApiTienda.Utils;

namespace WebApiTienda.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : Controller
    {
        private readonly AppContextDB _context;

        public ProductosController(AppContextDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi<List<ProductosModel>>>> GetAll()
        {
            var list = _context.Productos.FromSqlRaw("exec listarProductos").ToList();

            if (list.Count == 0)
            {
                return NoContent();
            }

            return new ResponseApi<List<ProductosModel>>(list, "ok", "Data found");
        }

        [HttpPost]
        public ActionResult<ResponseApi<ProductosModel>> CreateProd(ProductosModel produc)
        {
            try
            {
                string err = produc.Validate();

                if (err != "")
                {
                    return StatusCode(422, new ResponseApi<ProductosModel>(produc, "error", err));
                }

                var descrp = new SqlParameter("@descripcion", produc.Descripcion);
                var precio = new SqlParameter("@precio", produc.Precio);
                _context.Productos.FromSqlRaw(
                    "exec insertarProducto @descripcion, @precio",
                    descrp, precio
                ).ToList();

                return new ResponseApi<ProductosModel>(produc, "ok", "Created");

            }
            catch (Exception e)
            {
                if (e.Message.Contains("The required column 'Id'"))
                {
                    return StatusCode(201, new ResponseApi<ProductosModel>(produc, "ok", "Created"));
                }
                return StatusCode(400, new ResponseApi<ProductosModel>(produc, "error", e.ToString()));
            }
        }

    }
}
