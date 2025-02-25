using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tverskova.Database.Helpers;
using tverskova.Models;


namespace tverskova.Database.Configurations
{
    public class CafedraConfiguration
    {
        private const string TableName = "Cafedra";
        public void Configure(EntityTypeBuilder<Cafedra> builder)
        {
            // Задаем первичный ключ
            builder
                .HasKey(p => p.CafedraID)
                .HasName($"pk_(TableName)_cafedra_id");

            // Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1) builder. Property 
            builder.Property(p => p.CafedraID)
                .ValueGeneratedOnAdd();

            // Расписываем как будут называться колонки в БД, а так же их обязательность и тд builder.Property
            builder.Property(p => p.CafedraID)
                .HasColumnName("cafedra_id")
                .HasComment("Идентификатор кафедры");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию) builder.Property(p > p.FirstName)
           
            builder.Property(p => p.HeadCafedraID)
                .IsRequired()
                .HasColumnName("head_cafedra_id")
                .HasColumnType(ColumnType.Int).HasMaxLength(5)
                .HasComment("Идентификатор заведующего кафедрой");


            builder.ToTable(TableName)
                .HasOne(p => p.Cafedra)
                .WithMany(c => c.Teacher)
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
