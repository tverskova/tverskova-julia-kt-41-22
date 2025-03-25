namespace tverskova.Models
{
    public class Workload
    {
        public int WorkloadId { get; set; }
        public int DisciplineId { get; set; }
        public int Hours { get; set; }
        public int TeacherId { get; set; }

        public Discipline Discipline { get; set; }
        public Teacher Teacher { get; set; }
    }
}
