using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Request
{
    public record DtoConcert(string Title, string Description, string DateEvent, string TimeEvent, int TicketsQuantity, decimal UnitPrice, string? ImageUrl, string? Place, int GenreId, bool Finalized);
}
