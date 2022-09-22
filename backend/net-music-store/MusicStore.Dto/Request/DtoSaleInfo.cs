namespace MusicStore.Dto.Request;

public class DtoSaleInfo
{
    public int Id { get; set; }

    public string DateEvent { get; set; }

    public string TimeEvent { get; set; }

    public string Genre { get; set; }

    public string ImageUrl { get; set; }

    public string Title { get; set; }

    public string OperationNumber { get; set; }

    public string FullName { get; set; }

    public int Quantity { get; set; }

    public string SaleDate { get; set; }

    public string SaleTime { get; set; }

    public decimal TotalSale { get; set; }
}