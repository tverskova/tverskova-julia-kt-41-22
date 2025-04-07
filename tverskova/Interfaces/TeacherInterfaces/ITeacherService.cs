using tverskova.Database;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;
using tverskova.Filters.TeacherFilters;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherFilter filter, CancellationToken cancellationToken);
        Task<Teacher> AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task<Teacher> UpdateTeacherAsync(int teacherId, Teacher teacher, CancellationToken cancellationToken);
        Task<bool> DeleteTeacherAsync(int teacherId, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeacherDbContext _dbContext;

        public TeacherService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = _dbContext.Teachers
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .Include(t => t.Staff)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.DepartmentName))
            {
                teachers = teachers.Where(t => t.Department.Name == filter.DepartmentName);
            }

            if (filter.AcademicDegreeId.HasValue)
            {
                teachers = teachers.Where(t => t.AcademicDegreeId == filter.AcademicDegreeId.Value);
            }

            if (filter.StaffId.HasValue)
            {
                teachers = teachers.Where(t => t.StaffId == filter.StaffId.Value);
            }

            return teachers.ToArrayAsync(cancellationToken);
        }

        // Добавление преподавателя
        public async Task<Teacher> AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return teacher;
        }

        // Изменение преподавателя
        public async Task<Teacher> UpdateTeacherAsync(int teacherId, Teacher teacher, CancellationToken cancellationToken)
        {
            var existingTeacher = await _dbContext.Teachers.FindAsync(new object[] { teacherId }, cancellationToken);
            if (existingTeacher == null)
                return null; // или можно выбросить исключение, если преподаватель не найден

            // Обновляем свойства преподавателя
            existingTeacher.FirstName = teacher.FirstName;
            existingTeacher.LastName = teacher.LastName;
            existingTeacher.MiddleName = teacher.MiddleName;
            existingTeacher.AcademicDegreeId = teacher.AcademicDegreeId;
            existingTeacher.StaffId = teacher.StaffId;
            existingTeacher.DepartmentId = teacher.DepartmentId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existingTeacher;
        }

        // Удаление преподавателя
        public async Task<bool> DeleteTeacherAsync(int teacherId, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers.FindAsync(new object[] { teacherId }, cancellationToken);
            if (teacher == null)
                return false;

            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
