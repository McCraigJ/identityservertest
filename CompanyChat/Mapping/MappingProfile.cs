using AutoMapper;
using CompanyChat.Data.Models;
using CompanyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<GroupSM, GroupDM>()
        .ForMember(x => x.GroupName, a => a.MapFrom(b => b.Name))
        .ForMember(x => x.CreatedByUserId, a => a.MapFrom(b => b.CreatedBy))
        .ForMember(cfg => cfg.GroupUsers, opt => opt.Ignore())
        .ReverseMap()
        .ForMember(x => x.Name, a => a.MapFrom(b => b.GroupName))
        .ForMember(x => x.CreatedBy, a => a.MapFrom(b => b.CreatedByUserId));
    }
  }
}
