﻿namespace back_messenger_signalr
{
    public class ServiceResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Payload { get; set; }
    }
}
