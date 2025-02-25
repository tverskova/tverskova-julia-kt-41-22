using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Database.Helpers;
using tverskova.Models;


namespace tverskova.Database.Configurations
{
    public class TeacherConfiguration
    {
        private const string TableName = "Teacher";
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            // Задаем первичный ключ
            builder
                .HasKey(p => p.TeacherID)
                .HasName($"pk_(TableName)_teacher_id");

            // Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1) builder. Property (p => p. TeacherId)
            builder.Property(p => p.TeacherID)
                .ValueGeneratedOnAdd();

            // Расписываем как будут называться колонки в БД, а так же их обязательность и тд builder.Property
            builder.Property(p => p.TeacherID)
                .HasColumnName("teacher_id")
                .HasComment("Идентификатор записи преподавателя");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию) builder.Property(p > p.FirstName)
            builder.Property(p => p.AcademicDegreeID)
                .IsRequired()
                .HasColumnName("academic_degree")
                .HasColumnType(ColumnType.Int).HasMaxLength(5)
                .HasComment("Ученая степень преподавателя");

            builder.Property(p => p.PostID)
                .IsRequired()
                .HasColumnName("post")
                .HasColumnType(ColumnType.Int).HasMaxLength(5)
                .HasComment("Должность преподавателя");


            builder.ToTable(TableName)
                .HasOne(p => p.Cafedra)
                .WithMany()
                .HasForeignKey(p => p.CafedraID)
                .HasConstraintName("cafedra")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.CafedraID, $"idx_(TableName)_cafedra");

            builder.Navigation(p => p.Cafedra)
                .AutoInclude();
        }
    }
}
