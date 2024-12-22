using HRSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Role { get; set; }
    [ForeignKey("Project")]
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}
