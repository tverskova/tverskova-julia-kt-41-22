using tverskova.Database;
using tverskova.Filters.TeacherFilters;
using tverskova.Models;
using Microsoft.EntityFrameworkCore;

namespace tverskova.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        Task<Teacher[]> GetTeacherAsync(CancellationToken cancellationToken);
        Task<Teacher> GetTeacherByIdAsync(int id, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherFilter filter, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByAcademicDegreeAsync(TeacherFilter filter, CancellationToken cancellationToken);
        Task<Teacher[]> GetTeacherByStaffAsync(TeacherFilter filter, CancellationToken cancellationToken);

        Task AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task DeleteTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly TeacherDbContext _dbContext;

        public TeacherService(TeacherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Получение всех преподавателей
        public async Task<Teacher[]> GetTeacherAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                                   .ToArrayAsync(cancellationToken);
        }

        // Получение преподавателя по ID
        public async Task<Teacher>? GetTeacherByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                       .FirstOrDefaultAsync(d => d.TeacherId == id, cancellationToken);
        }

        // Получение преподавателей по кафедре
        public async Task<Teacher[]> GetTeacherByDepartmentAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
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

            return await teachers.ToArrayAsync(cancellationToken);
        }

        // Получение преподавателей по академической степени
        public async Task<Teacher[]> GetTeacherByAcademicDegreeAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Teachers
                .Where(t => t.AcademicDegreeId == filter.AcademicDegreeId)  
                .ToArrayAsync(cancellationToken);
        }

        // Получение преподавателей по должности
        public async Task<Teacher[]> GetTeacherByStaffAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = _dbContext.Teachers
                .Include(t => t.Staff)
                .Include(t => t.Department)
                .Include(t => t.AcademicDegree)
                .AsQueryable();

            if (filter.StaffId.HasValue)
            {
                teachers = teachers.Where(t => t.StaffId == filter.StaffId.Value);
            }

            return await teachers.ToArrayAsync(cancellationToken);
        }

        // Добавление преподавателя
        public async Task AddTeacherAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Обновление преподавателя
        public async Task UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            var existingTeacher = await _dbContext.Teachers.FindAsync(new object[] { teacher.TeacherId }, cancellationToken);
            if (existingTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {teacher.TeacherId} not found.");
            }

            existingTeacher.FirstName = teacher.FirstName;
            existingTeacher.LastName = teacher.LastName;
            existingTeacher.MiddleName = teacher.MiddleName;
            existingTeacher.AcademicDegreeId = teacher.AcademicDegreeId;
            existingTeacher.StaffId = teacher.StaffId;
            existingTeacher.DepartmentId = teacher.DepartmentId;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // Удаление преподавателя
        public async Task DeleteTeacherAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            var existingTeacher = await _dbContext.Teachers.FindAsync(new object[] { teacher.TeacherId }, cancellationToken);
            if (existingTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {teacher.TeacherId} not found.");
            }

            _dbContext.Teachers.Remove(existingTeacher);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
