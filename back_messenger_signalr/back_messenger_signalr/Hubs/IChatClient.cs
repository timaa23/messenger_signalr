using back_messenger_signalr.Models.Message;

namespace back_messenger_signalr.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user_name, string message, DateTime date_time);
        Task ReceiveMessage(ServiceResponse<MessageViewModel> model);
        Task ReceiveConnectedUsers(IEnumerable<string>? users);
    }
}
