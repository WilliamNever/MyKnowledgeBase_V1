using AutoMapper;
using Core31TestProject.Models.AutoMapperTestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SourceCompany, DestCompany>()
                .ForMember(d => d.Des_Name, s => s.MapFrom(opt => string.IsNullOrWhiteSpace(opt.Ss_Name) ? null : opt.Ss_Name))
                ;
            CreateMap<SDepart, DDepart>()
                .ForMember(d => d.Total_Numbers, s => s.MapFrom(opt => opt.Numbers + opt.Serv_Numbers))
                ;
        }
    }
}
