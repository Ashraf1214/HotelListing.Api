using AutoMapper;
using HotelListing.Api.Data.DTO.Country;
using HotelListing.Api.Data.DTO.Hotel;
using HotelListing.Api.Data.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HotelListing.Api.Configuration
{
    //Creating a mapping profile
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<CreateCountry, Country>().ReverseMap();
            CreateMap<GetCountryDTO, Country>().ReverseMap();
            CreateMap<GetAllCountriesDTO, Country>().ReverseMap();
            CreateMap<UpdateCountryDTO, Country>().ReverseMap();
            CreateMap<Hotel, GetHotelDTO>().ReverseMap();
            CreateMap<UpdateHotelDTO, Hotel>().ReverseMap();
            CreateMap<CreateHotelDTO, Hotel>().ReverseMap();
            CreateMap<GetAllHotelsDTO, Hotel>().ReverseMap();
        }
    }
}
