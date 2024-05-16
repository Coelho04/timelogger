namespace Timelogger.Domain.Entities;

public class TimeRegistration
{
    public int Id { get; set; }
    
    public int ProjectId { get; set; }
    
    public Project? Project { get; set; }
    
    public double Duration { get; set; }
    
    public string? Description { get; set; }
}
