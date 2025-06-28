using AutoMapper;
using HotelListing.Api.Data.DTO;
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
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<HotelDTO, Hotel>().ReverseMap();

            CreateMap<UpdateCountryDTO, Country>().ReverseMap();
        }
    }
}
