using AutoMapper;
using EA.BlogProject.Entities.Dtos;
using EA.BlogProject.WebUI.Areas.Admin.Models;

namespace EA.BlogProject.WebUI.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<ArticleUpdateDto, ArticleUpdateViewModel>();
            CreateMap<ArticleUpdateViewModel, ArticleUpdateDto>();
        }
    }
}
