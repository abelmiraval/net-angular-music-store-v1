namespace MusicStore.Entities;

public class Sale : EntityBase
{
    public DateTime SaleDate { get; set; }

    public Concert Concert { get; set; }

    public int ConcertId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalSale { get; set; }

    public string UserId { get; set; }

    public string OperationNumber { get; set; }
}