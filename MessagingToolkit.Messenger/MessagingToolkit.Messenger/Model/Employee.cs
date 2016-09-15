using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger.Model
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmployeeID { get; set; }

        public  string EmployeeName { get; set; }

        public byte[] EmployeePhoto { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
