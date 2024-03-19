
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


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

        public virtual DbSet<Employee> list { get; set; } = null!;
        private string connetion = "Server=(local); DataBase=castor;Integrated Security=true";
        SqlDataReader leer;
        DataTable tabla = new DataTable();


        public DataTable getList()
       {
            SqlConnection conexion = new SqlConnection(connetion);
            conexion.Open();
            string cadena = "select * from cargos";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.Close();
            return tabla;

        }

        public DataTable getListEmployees()
        {
            SqlConnection conexion = new SqlConnection(connetion);
            conexion.Open();
            string cadena = "select * from empleados";
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


            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PK__");

                entity.ToTable("empleados");

                entity.Property(e => e.identification).HasColumnType("int(10, 2)");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.date).HasColumnType("date");

                entity.Property(e => e.position).HasColumnType("int(10, 2)");




            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
