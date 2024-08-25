using AutoMapper;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;

namespace BooksLib.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BookDTO, ExternalBookDTO>().ReverseMap();
            CreateMap<AuthorDTO, ExternalAuthorDTO>().ReverseMap();
            CreateMap<OrderDTO, ExternalOrderDTO>().ReverseMap();
            CreateMap<OrderLineDTO, ExternalOrderLineDTO>().ReverseMap();
        }
    }
}
