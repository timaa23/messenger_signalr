namespace back_messenger_signalr
{
    public class ServiceResponse<T>
    {
        public T Payload { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = String.Empty;
    }
}
