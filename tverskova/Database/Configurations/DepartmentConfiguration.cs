using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Models;

namespace tverskova.Database.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        private const string TableName = "cd_department";

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(p => p.DepartmentId)
                   .HasName($"pk_{TableName}_department_id");

            builder.Property(p => p.DepartmentId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnName("c_department_name")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(200)
                   .HasComment("Название кафедры");

            builder.HasOne(d => d.HeadTeacher)
                   .WithMany()
                   .HasForeignKey(d => d.HeadTeacherId)
                   .HasConstraintName($"fk_{TableName}_head_teacher_id")
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
