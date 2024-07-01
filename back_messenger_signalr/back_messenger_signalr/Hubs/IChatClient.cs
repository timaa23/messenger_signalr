using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(ServiceResponse<MessageViewModel> model);
    }
}
