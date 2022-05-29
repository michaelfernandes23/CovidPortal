using AutoMapper;
using CovidPortal.Domain.Entity;

namespace CovidPortal.Domain
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CovidData, CovidCountryDetail>().ReverseMap();
        }
    }
}
