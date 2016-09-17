using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger.Model
{
    public class Gateway
    {
        public Gateway()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GatewayID { get; set; }

        public string GatewayConfig { get; set; }

        public string GatewayPhoneNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
