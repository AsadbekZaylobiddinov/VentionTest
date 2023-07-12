using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTest.Domain.Entities;
using VentionTest.Service.DTOs.Capital;
using VentionTest.Service.DTOs.City;
using VentionTest.Service.DTOs.Country;
using VentionTest.Service.DTOs.Language;
using VentionTest.Service.DTOs.LanguageCountry;

namespace VentionTest.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Capital, CapitalForCreationDto>().ReverseMap();
            CreateMap<Capital, CapitalForUpdateDto>().ReverseMap();
            CreateMap<Capital, CapitalForResultDto>().ReverseMap();

            CreateMap<City, CityForCreationDto>().ReverseMap();
            CreateMap<City, CityForUpdateDto>().ReverseMap();
            CreateMap<City, CityForResultDto>().ReverseMap();

            CreateMap<Country, CountryForCreationDto>().ReverseMap();
            CreateMap<Country, CountryForUpdateDto>().ReverseMap();
            CreateMap<Country, CountryForResultDto>().ReverseMap();

            CreateMap<Language, LanguageForCreationDto>().ReverseMap();
            CreateMap<Language, LanguageForUpdateDto>().ReverseMap();
            CreateMap<Language, LanguageForResultDto>().ReverseMap();

            CreateMap<LanguageCountry, LanguageCountryForCreationDto>().ReverseMap();
            CreateMap<LanguageCountry, LanguageCountryForUpdateDto>().ReverseMap();
            CreateMap<LanguageCountry, LanguageCountryForResultDto>().ReverseMap();
        }
    }
}
