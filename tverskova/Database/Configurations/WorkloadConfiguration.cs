using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Models;

namespace tverskova.Database.Configurations
{
    public class WorkloadConfiguration : IEntityTypeConfiguration<Workload>
    {
        private const string TableName = "cd_workload";

        public void Configure(EntityTypeBuilder<Workload> builder)
        {
            builder.HasKey(p => p.WorkloadId)
                   .HasName($"pk_{TableName}_workload_id");

            builder.Property(p => p.WorkloadId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Hours)
                   .IsRequired()
                   .HasColumnName("c_workload_hours")
                   .HasColumnType("int")
                   .HasComment("Количество часов нагрузки по дисциплине");

            // Внешний ключ - дисциплина
            builder.HasOne(w => w.Discipline)
                   .WithMany(d => d.Workloads)
                   .HasForeignKey(w => w.DisciplineId)
                   .HasConstraintName($"fk_{TableName}_discipline_id");
        }
    }
}
