namespace tverskova.Filters.TeacherFilters
{
    public class DepartmentFilter
    {
        public DateTime? MinDateOfFoundation { get; set; }   // Фильтрация по минимальной дате основания кафедры
        public DateTime? MaxDateOfFoundation { get; set; }   // Фильтрация по максимальной дате основания кафедры
        public int? MinTeachersCount { get; set; }           // Фильтрация по минимальному количеству преподавателей
        public int? MaxTeachersCount { get; set; }           // Фильтрация по максимальному количеству преподавателей
        public int? HeadTeacherId { get; set; }              // Фильтрация по ID заведующего кафедрой
        public string? Name { get; set; }
    }
}
