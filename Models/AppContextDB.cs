using Microsoft.EntityFrameworkCore;

namespace WebApiTienda.Models
{
    public class AppContextDB: DbContext
    {
        public AppContextDB(DbContextOptions<AppContextDB> options): base(options) { }

        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }
    }
}
