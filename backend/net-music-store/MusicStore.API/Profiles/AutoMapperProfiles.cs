using AutoMapper;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Complex;

namespace MusicStore.API.Profiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Concert, DtoResponseConcert>()
            .ForMember(dto => dto.Title, ent => ent.MapFrom(x => x.Title))
            .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
            .ForMember(dto => dto.DateEvent, ent => ent.MapFrom(x => x.DateEvent.ToString("yyyy-MM-dd")))
            .ForMember(dto => dto.TimeEvent, ent => ent.MapFrom(x => x.DateEvent.ToString("HH:mm:ss")))
            .ForMember(dto => dto.TicketsQuantity, ent => ent.MapFrom(x => x.TicketsQuantity))
            .ForMember(dto => dto.UnitPrice, ent => ent.MapFrom(x => x.UnitPrice))
            .ForMember(dto => dto.Place, ent => ent.MapFrom(x => x.Place))
            .ForMember(dto => dto.ImageUrl, ent => ent.MapFrom(x => x.ImageUrl))
            .ForMember(dto => dto.Finalized, ent => ent.MapFrom(x => x.Finalized))
            .ForMember(dto => dto.Genre, ent => ent.MapFrom(x => x.Genre.Description));

        CreateMap<DtoConcert, Concert>()
           .ForMember(dto => dto.Title, ent => ent.MapFrom(x => x.Title))
           .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
           .ForMember(dto => dto.DateEvent, ent => ent.MapFrom(x => Convert.ToDateTime($"{x.DateEvent} {x.TimeEvent}")))
           .ForMember(dto => dto.TicketsQuantity, ent => ent.MapFrom(x => x.TicketsQuantity))
           .ForMember(dto => dto.UnitPrice, ent => ent.MapFrom(x => x.UnitPrice))
           .ForMember(dto => dto.Place, ent => ent.MapFrom(x => x.Place))
           .ForMember(dto => dto.GenreId, ent => ent.MapFrom(x => x.GenreId))
           .ReverseMap();

        CreateMap<Concert, ConcertInfo>()
            .ForMember(dto => dto.Id, ent => ent.MapFrom(x => x.Id))
            .ForMember(dto => dto.Title, ent => ent.MapFrom(x => x.Title))
            .ForMember(dto => dto.Description, ent => ent.MapFrom(x => x.Description))
            .ForMember(dto => dto.DateEvent, ent => ent.MapFrom(x => x.DateEvent.ToLongDateString()))
            .ForMember(dto => dto.TimeEvent, ent => ent.MapFrom(x => x.DateEvent.ToString("HH:mm:ss")))
            .ForMember(dto => dto.UnitPrice, ent => ent.MapFrom(x => x.UnitPrice))
            .ForMember(dto => dto.TicketsQuantity, ent => ent.MapFrom(x => x.TicketsQuantity))
            .ForMember(dto => dto.Place, ent => ent.MapFrom(x => x.Place))
            .ForMember(dto => dto.Status, ent => ent.MapFrom(x => x.Status ? "Habilitado" : "Inhabilitado"))
            .ForMember(dto => dto.Genre, ent => ent.MapFrom(x => x.Genre.Description))
            .ForMember(dto => dto.ImageUrl, ent => ent.MapFrom(x => x.ImageUrl));

        CreateMap<Concert, ConcertMinimalInfo>();
        
        // Genres
        
        CreateMap<Genre, DtoResponseGenre>();

        CreateMap<DtoGenre, Genre>();

        CreateMap<Sale, SaleInfo>()
            .ForMember(dto => dto.Id, ent => ent.MapFrom(p => p.Id))
            .ForMember(dto => dto.Title, ent => ent.MapFrom(p => p.Concert.Title))
            .ForMember(dto => dto.DateEvent, ent => ent.MapFrom(p => p.Concert.DateEvent))
            .ForMember(dto => dto.Genre, ent => ent.MapFrom(p => p.Concert.Genre.Description))
            .ForMember(dto => dto.Quantity, ent => ent.MapFrom(p => p.Quantity))
            .ForMember(dto => dto.TotalSale, ent => ent.MapFrom(p => p.TotalSale))
            .ForMember(dto => dto.OperationNumber, ent => ent.MapFrom(p => p.OperationNumber));

        CreateMap<SaleInfo, DtoSaleInfo>()
            .ForMember(o => o.Id, d => d.MapFrom(x => x.Id))
            .ForMember(o => o.DateEvent, d => d.MapFrom(x => x.DateEvent.ToString(Constants.DateFormat)))
            .ForMember(o => o.TimeEvent, d => d.MapFrom(x => x.DateEvent.ToString(Constants.TimeFormat)))
            .ForMember(o => o.Quantity, d => d.MapFrom(x => x.Quantity))
            .ForMember(o => o.Title, d => d.MapFrom(x => x.Title))
            .ForMember(o => o.SaleDate, d => d.MapFrom(x => x.SaleDate.ToString(Constants.DateFormat)))
            .ForMember(o => o.SaleTime, d => d.MapFrom(x => x.SaleDate.ToString(Constants.TimeFormat)))
            .ForMember(o => o.TotalSale, d => d.MapFrom(x => x.TotalSale))
            .ForMember(o => o.Genre, d => d.MapFrom(x => x.Genre))
            .ForMember(o => o.OperationNumber, d => d.MapFrom(x => x.OperationNumber))
            .ForMember(o => o.FullName, d => d.MapFrom(x => x.FullName))
            .ForMember(o => o.ImageUrl, d => d.MapFrom(x => x.ImageUrl));

        CreateMap<DtoSale, Sale>()
            .ForMember(dto => dto.ConcertId, ent => ent.MapFrom(x => x.ConcertId))
            .ForMember(dto => dto.Quantity, ent => ent.MapFrom(x => x.Quantity))
            .ForMember(dto => dto.UnitPrice, ent => ent.MapFrom(x => x.UnitPrice));
    }
}