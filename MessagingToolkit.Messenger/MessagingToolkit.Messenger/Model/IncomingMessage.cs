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
        public enum ProcessingStatus {
            NotProcessed = 0,
            Processed = 2,
            Error = 3
        }
        public IncomingMessage()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Status = ProcessingStatus.NotProcessed;
        }

        public int ID { get; set; }

        public string MsgContent { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public ProcessingStatus Status { get; set; }

        public string ErrorMsg { get; set; }
    }
}
