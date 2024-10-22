
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace APIPRUEBAS.Models
{
    public partial class DBAPIContext : DbContext
    {
        public DBAPIContext()
        {
        }


        public DBAPIContext(DbContextOptions<DBAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> persona { get; set; } = null!;
        private string connetion = "Server=(local); DataBase=pactia;Integrated Security=true";
        SqlDataReader leer;
        DataTable tabla = new DataTable();


        public DataTable getList()
        {
            SqlConnection conexion = new SqlConnection(connetion);
            conexion.Open();
            string cadena = "select * from personas";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.Close();
            return tabla;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__");

                entity.ToTable("personas");

                entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

               entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Celular)
                 .HasMaxLength(50)
                 .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
