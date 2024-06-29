using AutoMapper;
using back_messenger_signalr.Entities;
using back_messenger_signalr.Models.Conversation;

namespace back_messenger_signalr.AutoMapper
{
    public class AutoMapperConversationProfile : Profile
    {
        public AutoMapperConversationProfile()
        {
            CreateMap<ConversationEntity, ConversationViewModel>()
                .ForMember(desc => desc.LastMessage, opt => opt.MapFrom(src => src.Messages.OrderByDescending(m => m.DateCreated).FirstOrDefault()));

        }
    }
}
