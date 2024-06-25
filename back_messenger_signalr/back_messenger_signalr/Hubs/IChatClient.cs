namespace back_messenger_signalr.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user_name, string message, DateTime date_time);
        Task ReceiveConnectedUsers(IEnumerable<string>? users);
    }
}
