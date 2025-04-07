﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tverskova.Database;

#nullable disable

namespace tverskova.Migrations
{
    [DbContext(typeof(TeacherDbContext))]
    partial class TeacherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<DateTime>("DateOfFoundation")
                        .HasColumnType("datetime2");

                    b.Property<int>("HeadTeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.HasIndex("HeadTeacherId")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("tverskova.Models.AcademicDegree", b =>
                {
                    b.Property<int>("AcademicDegreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcademicDegreeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_academic_degree_name")
                        .HasComment("Название ученой степени (Кандидат наук, Доктор наук)");

                    b.HasKey("AcademicDegreeId")
                        .HasName("pk_cd_academic_degree_academic_degree_id");

                    b.ToTable("AcademicDegrees");
                });

            modelBuilder.Entity("tverskova.Models.Discipline", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisciplineId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_discipline_name")
                        .HasComment("Название дисциплины");

                    b.HasKey("DisciplineId")
                        .HasName("pk_cd_discipline_discipline_id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("tverskova.Models.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_staff_name")
                        .HasComment("Название должности (Преподаватель, Доцент, Профессор)");

                    b.HasKey("StaffId")
                        .HasName("pk_cd_staff_staff_id");

                    b.ToTable("Staffers");
                });

            modelBuilder.Entity("tverskova.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<int>("AcademicDegreeId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_teacher_firstname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_teacher_lastname");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar")
                        .HasColumnName("c_teacher_middlename");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId")
                        .HasName("pk_cd_teacher_teacher_id");

                    b.HasIndex("AcademicDegreeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("StaffId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("tverskova.Models.Workload", b =>
                {
                    b.Property<int>("WorkloadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkloadId"));

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("Hours")
                        .HasColumnType("int")
                        .HasColumnName("c_workload_hours")
                        .HasComment("Количество часов нагрузки по дисциплине");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("WorkloadId")
                        .HasName("pk_cd_workload_workload_id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Workloads");
                });

            modelBuilder.Entity("Department", b =>
                {
                    b.HasOne("tverskova.Models.Teacher", "HeadTeacher")
                        .WithOne()
                        .HasForeignKey("Department", "HeadTeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("HeadTeacher");
                });

            modelBuilder.Entity("tverskova.Models.Teacher", b =>
                {
                    b.HasOne("tverskova.Models.AcademicDegree", "AcademicDegree")
                        .WithMany("Teachers")
                        .HasForeignKey("AcademicDegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cd_academic_degree_teacher_id");

                    b.HasOne("Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_cd_teacher_department_id");

                    b.HasOne("tverskova.Models.Staff", "Staff")
                        .WithMany("Teachers")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cd_staff_teacher_id");

                    b.Navigation("AcademicDegree");

                    b.Navigation("Department");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("tverskova.Models.Workload", b =>
                {
                    b.HasOne("tverskova.Models.Discipline", "Discipline")
                        .WithMany("Workloads")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cd_workload_discipline_id");

                    b.HasOne("tverskova.Models.Teacher", "Teacher")
                        .WithMany("Workloads")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cd_workload_teacher_id");

                    b.Navigation("Discipline");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Department", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("tverskova.Models.AcademicDegree", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("tverskova.Models.Discipline", b =>
                {
                    b.Navigation("Workloads");
                });

            modelBuilder.Entity("tverskova.Models.Staff", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("tverskova.Models.Teacher", b =>
                {
                    b.Navigation("Workloads");
                });
#pragma warning restore 612, 618
        }
    }
}
