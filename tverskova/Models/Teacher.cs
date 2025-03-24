namespace tverskova.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public int AcademicDegreeId { get; set; }
        public AcademicDegree AcademicDegree { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
    }
}
