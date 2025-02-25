using Microsoft.EntityFrameworkCore;
using tverskova.Database.Configurations;
using tverskova.Models;

namespace tverskova.Database

{
    public class TeacherDbContext: DbContext
    {
        DbSet<Teacher> teachers { get; set; }

        DbSet<Cafedra> cafedras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new CafedraConfiguration());
        }

        public TeacherDbContext(DbContextOptions<TeacherDbContext> options) : base(options)
        {

        }
    }
}
