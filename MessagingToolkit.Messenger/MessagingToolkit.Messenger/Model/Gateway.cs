using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger.Model
{
    public class Gateway
    {

        public int ID { get; set; }

        public string GatewayName { get; set; }

        public string GatewayConfig { get; set; }

        public bool AutoStart { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
