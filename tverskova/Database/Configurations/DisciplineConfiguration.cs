using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Models;

namespace tverskova.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(p => p.DisciplineId)
                   .HasName($"pk_{TableName}_discipline_id");

            builder.Property(p => p.DisciplineId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnName("c_discipline_name")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(200)
                   .HasComment("Название дисциплины");

            // Внешний ключ - преподаватель, ведущий дисциплину
            builder.HasOne(d => d.Teacher)
                   .WithMany(t => t.Disciplines)
                   .HasForeignKey(d => d.TeacherId)
                   .HasConstraintName($"fk_{TableName}_teacher_id");

            // Связь "один ко многим" - нагрузка в часах
            builder.HasMany(d => d.Workloads)
                   .WithOne(w => w.Discipline)
                   .HasForeignKey(w => w.DisciplineId)
                   .HasConstraintName($"fk_{TableName}_workload_id");
        }
    }
}
