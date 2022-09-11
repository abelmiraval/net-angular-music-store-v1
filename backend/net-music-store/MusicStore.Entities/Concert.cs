using System.ComponentModel.DataAnnotations;

namespace MusicStore.Entities
{
    public class Concert : EntityBase
    {
        [StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public DateTime DateEvent { get; set; }
        
        public int TicketsQuantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [StringLength(50)]
        public string? Place { get; set; }
        
        public int GenreId { get; set; }
        
        public Genre Genre { get; set; }
        
        public bool Finalized { get; set; }
        
    }
}