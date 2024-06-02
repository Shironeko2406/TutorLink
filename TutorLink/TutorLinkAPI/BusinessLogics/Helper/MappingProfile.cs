using AutoMapper;
using DataLayer.Entities;
using TutorLinkAPI.ViewModel;

namespace TutorLinkAPI.BusinessLogics.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Tutor
        CreateMap<TutorViewModel, Tutor>()
            .ForMember(dest => dest.Qualifications, opt => opt.MapFrom(src => src.Qualifications))
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<AddTutorViewModel, Tutor>()
            .ForMember(dest => dest.TutorId, opt => opt.MapFrom(src => src.TutorId))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForAllOtherMembers(opt => opt.Ignore()); 

        //Qualification
        CreateMap<QualificationViewModel, Qualification>().ReverseMap();
    }
}