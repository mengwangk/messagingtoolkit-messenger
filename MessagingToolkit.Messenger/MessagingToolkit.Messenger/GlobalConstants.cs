using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger
{
    public static class GlobalConstants
    {
        /// <summary>
        /// Default gateway name referencing the gateway in Gateway database table which will be used to send and receive message.
        /// </summary>
        public const string DefaultGatewayName = "Default";


        /// <summary>
        /// Name for outgoing message poller
        /// </summary>
        public const string OutgoingMsgPollerName = "OutgoingMsgPoller";
    }
}
