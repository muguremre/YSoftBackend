using System.ComponentModel.DataAnnotations;

public class Project
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MinEmployees { get; set; }
    public int MaxEmployees { get; set; }
    public bool IsComplete { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
