using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Models;

namespace tverskova.Database.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        private const string TableName = "cd_staff";

        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(p => p.StaffId)
                   .HasName($"pk_{TableName}_staff_id");

            builder.Property(p => p.StaffId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnName("c_staff_name")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(200)
                   .HasComment("Название должности (Преподаватель, Доцент, Профессор)");

            // Связь "один ко многим" - одна должность, много преподавателей
            builder.HasMany(s => s.Teachers)
                   .WithOne(t => t.Staff)
                   .HasForeignKey(t => t.StaffId)
                   .HasConstraintName($"fk_{TableName}_teacher_id");
        }
    }
}
