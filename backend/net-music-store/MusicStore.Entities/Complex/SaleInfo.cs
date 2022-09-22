namespace MusicStore.Entities.Complex;

public class SaleInfo
{
    public int Id { get; set; }
    public DateTime DateEvent { get; set; }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string OperationNumber { get; set; }
    public string FullName { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalSale { get; set; }
}