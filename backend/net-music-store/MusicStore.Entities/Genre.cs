using System.ComponentModel.DataAnnotations;

namespace MusicStore.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool Status { get; set; }
    }
}