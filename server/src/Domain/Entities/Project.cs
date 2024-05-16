namespace Timelogger.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public DateOnly Deadline { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public ICollection<TimeRegistration> TimeRegistrations { get; set; } = new List<TimeRegistration>();
}
