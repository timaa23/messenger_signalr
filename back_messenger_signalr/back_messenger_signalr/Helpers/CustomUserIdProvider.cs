using Microsoft.AspNetCore.SignalR;

namespace back_messenger_signalr.Helpers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection) => connection.User.FindFirst("id").Value;
    }
}
