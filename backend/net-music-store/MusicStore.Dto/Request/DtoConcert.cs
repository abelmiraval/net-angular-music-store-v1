using System.ComponentModel.DataAnnotations;

namespace MusicStore.Dto.Request;

public class DtoConcert
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
 
    [Range(1, 999, ErrorMessage = "El valor debe de estar entre 1 y 999")]
    public int TicketsQuantity { get; set; }
    
    [Required]
    public string DateEvent { get; set; }
    
    [Required]
    public string TimeEvent { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? Place { get; set; }
    public int GenreId { get; set; }
    public bool Finalized { get; set; }
}
