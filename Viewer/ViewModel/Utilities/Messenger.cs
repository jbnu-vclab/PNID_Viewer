using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer.ViewModel.Utilities
{
    public class Messenger
    {
        private static readonly Dictionary<string, Action<object>> _messageHandlers = new Dictionary<string, Action<object>>();

        public static void Send(string messageName, object parameter = null)
        {
            if (_messageHandlers.ContainsKey(messageName))
            {
                _messageHandlers[messageName]?.Invoke(parameter);
            }
        }

        public static void Subscribe(string messageName, Action<object> handler)
        {
            if (!_messageHandlers.ContainsKey(messageName))
            {
                _messageHandlers.Add(messageName, null);
            }

            _messageHandlers[messageName] += handler;
        }

        public static void Unsubscribe(string messageName, Action<object> handler)
        {
            if (_messageHandlers.ContainsKey(messageName))
            {
                _messageHandlers[messageName] -= handler;
            }
        }
    }

}
