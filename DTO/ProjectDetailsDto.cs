namespace HRSystem.Domain.DTOs
{
    public class ProjectDetailsDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinEmployees { get; set; }
        public int MaxEmployees { get; set; }
        public bool IsComplete { get; set; }
    }
}
