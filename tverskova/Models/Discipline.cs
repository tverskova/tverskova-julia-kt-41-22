namespace tverskova.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Workload> Workloads { get; set; } = new List<Workload>();
    }
}
