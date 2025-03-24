namespace tverskova.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        // Заведующий кафедры (один из преподавателей)
        public int HeadTeacherId { get; set; }
        public Teacher HeadTeacher { get; set; }
        public List<Teacher> Teachers { get; set; } = new();
    }
}
