using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingToolkit.Messenger.Model
{
    public class Employee
    {
        public Employee()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            PhotoImageType = "image/png";    // Default to PNG
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmployeeID { get; set; }

        public  string EmployeeName { get; set; }

        public byte[] EmployeePhoto { get; set; }

        public string PhotoImageType { get; set; }
               
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
