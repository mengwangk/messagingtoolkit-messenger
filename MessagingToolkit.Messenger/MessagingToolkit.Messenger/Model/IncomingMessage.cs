using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger.Model
{
    /// <summary>
    /// Incoming SMS.
    /// </summary>
    public class IncomingMessage
    {
        public int ID { get; set; }

        public string MsgContent { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool IsProcessed { get; set; }

        public string ErrorMsg { get; set; }
    }
}
