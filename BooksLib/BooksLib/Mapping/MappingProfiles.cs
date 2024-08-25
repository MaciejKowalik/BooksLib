﻿using AutoMapper;
using BooksLib.Domain.ExternalModels;
using BooksLib.Domain.Models;

namespace BooksLib.Mapping
{
    /// <summary>
    /// Class implementing mapping between DTO models, using AutoMapper
    /// </summary>
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
