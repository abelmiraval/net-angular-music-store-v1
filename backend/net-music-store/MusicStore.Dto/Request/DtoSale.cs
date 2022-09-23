namespace MusicStore.Dto.Request;

public class DtoSale
{
    public int ConcertId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}