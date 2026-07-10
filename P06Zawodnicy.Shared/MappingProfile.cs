using AutoMapper;
using P06Zawodnicy.Shared.Data;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Zawodnicy.Shared
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ZawodnikDb -> Zawodnik
            CreateMap<ZawodnikDb, Zawodnik>()
               .ForMember(dest => dest.Id_zawodnika, opt => opt.MapFrom(src => src.Id_zawodnika))
               .ForMember(dest => dest.Id_trenera, opt => opt.MapFrom(src => src.Id_trenera))
               .ForMember(dest => dest.Imie, opt => opt.MapFrom(src => src.Imie))
               .ForMember(dest => dest.Nazwisko, opt => opt.MapFrom(src => src.Nazwisko))
               .ForMember(dest => dest.Kraj, opt => opt.MapFrom(src => src.Kraj));
        }
    }
}
