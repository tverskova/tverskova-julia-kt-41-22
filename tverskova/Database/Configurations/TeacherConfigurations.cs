using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Models;

namespace tverskova.Database.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        private const string TableName = "cd_teacher";

        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(p => p.TeacherId)
                   .HasName($"pk_{TableName}_teacher_id");

            builder.Property(p => p.TeacherId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasColumnName("c_teacher_firstname")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasColumnName("c_teacher_lastname")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(p => p.MiddleName)
                   .HasColumnName("c_teacher_middlename")
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.HasOne(t => t.AcademicDegree)
                   .WithMany(ad => ad.Teachers)
                   .HasForeignKey(t => t.AcademicDegreeId)
                   .HasConstraintName($"fk_{TableName}_academic_degree_id");

            builder.HasOne(t => t.Staff)
                   .WithMany(p => p.Teachers)
                   .HasForeignKey(t => t.StaffId)
                   .HasConstraintName($"fk_{TableName}_position_id");

            builder.HasOne(t => t.Department)
                   .WithMany(d => d.Teachers)
                   .HasForeignKey(t => t.DepartmentId)
                   .HasConstraintName($"fk_{TableName}_department_id")
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
