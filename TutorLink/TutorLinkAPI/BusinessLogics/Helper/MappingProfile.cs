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

        CreateMap<UpdateTutorViewModel, Tutor>()
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.Fullname, opt => opt.Condition(src => src.Fullname != null && src.Fullname != "string"))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null && src.Email != "string"))
            .ForMember(dest => dest.Phone, opt => opt.Condition(src => src.Phone != null && src.Phone != "string"))
            .ForMember(dest => dest.Address, opt => opt.Condition(src => src.Address != null && src.Address != "string"))
            .ReverseMap();



        //Qualification
        CreateMap<QualificationViewModel, Qualification>().ReverseMap();
    }
}