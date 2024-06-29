using AutoMapper;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.AutoMapper
{
    public class AutoMapperMessageProfile : Profile
    {
        public AutoMapperMessageProfile()
        {
            CreateMap<MessageEntity, MessageViewModel>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.ConversationGuid, opt => opt.MapFrom(src => src.Conversation.Guid));
        }
    }
}
