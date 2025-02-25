namespace tverskova.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }

        public int AcademicDegreeID { get; set; }

        public int PostID { get; set; }

        public int CafedraID { get; set; }
        public Cafedra Cafedra { get; set; }
    }
}
