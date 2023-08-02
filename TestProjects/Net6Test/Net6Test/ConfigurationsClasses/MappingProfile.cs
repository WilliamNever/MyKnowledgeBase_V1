using AutoMapper;
using Net6Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.ConfigurationsClasses
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExtClassModel, Base2>()
                .ForMember(d => d.Base0_Name, s => s.MapFrom(p => p.Name))
                .ForMember(d => d.Base0_HiFiInfor, s => s.MapFrom(p => p.HiFiInfor ?? "default HIFI"))
                //.ForMember(d => d.Acgx, s => s.MapFrom(p => p.HiFiInfor))
                .ForMember(d => d.Acgx, s => s.MapFrom(p => "To Ask..."))
                .ReverseMap();
            CreateMap<ExtClassModel, Base0>()
                .ForMember(d => d.Base0_Name, s => s.MapFrom(p => p.Name))
                .ForMember(d => d.Base0_HiFiInfor, s => s.MapFrom(p => p.HiFiInfor))
                .ReverseMap();
            CreateMap<ExtClassModel, BaseOX>()
                .ForMember(d => d.BaseOX_Name, s => s.MapFrom(p => p.Name))
                .ForMember(d => d.BaseOX_HiFiInfor, s => s.MapFrom(p => p.HiFiInfor))
                .ReverseMap();
        }
    }

    public class MappingProfile1 : Profile
    {
        public MappingProfile1()
        {
            CreateMap<ExtClassModel, BaseOXSep1>()
                .ForMember(d => d.BaseOXSep1_Name, s => s.MapFrom(p => p.Name))
                .ForMember(d => d.BaseOXSep1_HiFiInfor, s => s.MapFrom(p => p.HiFiInfor))
                .ReverseMap();
        }
    }
}
