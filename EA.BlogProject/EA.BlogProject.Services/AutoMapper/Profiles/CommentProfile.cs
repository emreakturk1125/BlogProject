using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EA.BlogProject.Entities.Concrete;
using EA.BlogProject.Entities.Dtos;

namespace EA.BlogProject.Services.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddDto, Comment>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.ModifiedByName, opt => opt.MapFrom(x => x.CreatedByName))
                .ForMember(dest=>dest.IsActive,opt=>opt.MapFrom(x=>false));
            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Comment, CommentUpdateDto>();

        }
    }
}
