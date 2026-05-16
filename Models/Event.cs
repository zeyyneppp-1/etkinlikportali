namespace EtkinlikPortali.Models;

public class Event {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
   public int? CategoryId { get; set; } 
    public Category? Category { get; set; }
    public string? ImageUrl { get; set; }
}