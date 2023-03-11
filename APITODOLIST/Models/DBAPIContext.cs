using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace APITODOLIST.Models
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

        public virtual DbSet<List> list { get; set; } = null!;
        private string connetion = "Server=(local); DataBase=ToDoListBD;Integrated Security=true";
        SqlDataReader leer;
        DataTable tabla = new DataTable();


        public DataTable getList(int IdList)
        {
            SqlConnection conexion = new SqlConnection(connetion);
            conexion.Open();
            string cadena = "select * from items where idList=" + IdList;
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


            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .HasName("PK__");

                entity.ToTable("items");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.IdList).HasColumnType("int(10, 2)");

            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
