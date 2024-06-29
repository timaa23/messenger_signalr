using AutoMapper;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Participant;

namespace back_messenger_signalr.AutoMapper
{
    public class AutoMapperParticipantProfile : Profile
    {
        public AutoMapperParticipantProfile()
        {
            CreateMap<ParticipantEntity, ParticipantViewModel>()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(desc => desc.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(desc => desc.Image, opt => opt.MapFrom(src => src.User.Image));
        }
    }
}
