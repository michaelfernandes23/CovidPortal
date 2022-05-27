using AutoMapper;
using CovidPortal.Domain.DTO;
using CovidPortal.Domain.Entity;

namespace CovidPortal.Domain.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CovidData, CovidCountryDetail>().ReverseMap();
        }
    }
}
