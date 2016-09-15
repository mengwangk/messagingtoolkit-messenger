using MessagingToolkit.Messenger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using MessagingToolkit.Messenger.Properties;
using MessagingToolkit.Messenger.Helper;

namespace MessagingToolkit.Messenger.Database
{
    public class MessengerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MessengerContext>
    {
        /// <summary>
        /// Set up the database with data.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MessengerContext context)
        {
            // Get the sample photo
            string currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            byte[] samplePhoto = null;
            string sampleFileFullPath = currentPath + Path.DirectorySeparatorChar + "sample_photo.png";
            if (File.Exists(sampleFileFullPath))
            {
                Image image = Image.FromFile(sampleFileFullPath);
                samplePhoto = ImageHelper.ConvertImageToByteArray(image, ImageFormat.Png);
            }
            // Add sample employees
            var employees = new List<Employee>
            {
                new Employee{EmployeeID="001", EmployeeName="Alexander", EmployeePhoto = samplePhoto, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                new Employee{EmployeeID="002", EmployeeName="Alice", EmployeePhoto = samplePhoto, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                new Employee{EmployeeID="003", EmployeeName="Bob", EmployeePhoto = samplePhoto, DateCreated = DateTime.Now, DateModified = DateTime.Now },
            };
            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();


            // Add device
            string defaultGatewayConfig = "{\"BaudRate\": 115200,  \"DataBits\": 8, \"Handshake\": 0, \"PortName\": \"COM8\"," +
                                          "\"Parity\": 0, \"StopBits\": 1, \"Pin\": \"\", \"DisablePinCheck\": true," +
                                          "\"LicenseKey\": \"1234567890\", \"ProviderMMSC\": \"\", \"ProviderAPN\": \"\"," +
                                          "\"ProviderWAPGateway\": \"\", \"ProviderAPNAccount\": \"\", \"ProviderAPNPassword\": \"\"}";
            var gateways = new List<Gateway>
            {
                new Gateway {GatewayName = GlobalConstants.DefaultGatewayName, AutoStart = true, GatewayConfig = defaultGatewayConfig, DateCreated = DateTime.Now, DateModified = DateTime.Now }
            };
            gateways.ForEach(s => context.Gateways.Add(s));
            context.SaveChanges();

        }
    }
}
