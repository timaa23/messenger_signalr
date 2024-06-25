using System.Collections.Concurrent;

namespace back_messenger_signalr.Helpers
{
    public class OnlineDB
    {
        private readonly ConcurrentDictionary<int, string> _onlines = new ConcurrentDictionary<int, string>();

        public ConcurrentDictionary<int, string> onlines => _onlines;
    }
}
