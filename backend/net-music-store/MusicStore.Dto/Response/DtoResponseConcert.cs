namespace MusicStore.Dto.Response;

public class DtoResponseConcert
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TicketsQuantity { get; set; }
    public string DateEvent { get; set; }
    public string TimeEvent { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? Place { get; set; }
    public string Genre { get; set; }
    public bool Finalized { get; set; }
}