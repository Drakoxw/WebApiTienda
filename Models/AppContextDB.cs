using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace WebApiTienda.Models
{
    public class AppContextDB: DbContext
    {
        public AppContextDB(DbContextOptions<AppContextDB> options): base(options) { }

        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }
        public DbSet<LogsModel> LogDb { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<LogsModel>()
        //        .Property(b => b.Fecha)
        //        .HasDefaultValueSql("getdate()");
        //}
    }
}
