using AutoMapper;
using DataLayer.Entities;
using TutorLinkAPI.ViewModel;
using TutorLinkAPI.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountViewModel>().ReverseMap();
        CreateMap<Apply, ApplyViewModel>().ReverseMap();
        CreateMap<PostRequest, PostRequestViewModel>().ReverseMap();

    }
}
