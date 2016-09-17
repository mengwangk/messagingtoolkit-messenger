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
            string sampleFileFullPath = currentPath + Path.DirectorySeparatorChar + "sample_photo.jpg";
            if (File.Exists(sampleFileFullPath))
            {
                //Image image = Image.FromFile(sampleFileFullPath);
                //samplePhoto = ImageHelper.ConvertImageToByteArray(image, ImageFormat.Png);
                samplePhoto = ImageHelper.ImageFileToByteArray(sampleFileFullPath);
            }
            // Add sample employees
            var employees = new List<Employee>
            {
                new Employee{EmployeeID="001", EmployeeName="Alexander", EmployeePhoto = samplePhoto },
                new Employee{EmployeeID="002", EmployeeName="Alice", EmployeePhoto = samplePhoto },
                new Employee{EmployeeID="003", EmployeeName="Bob", EmployeePhoto = samplePhoto },
            };
            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();


            // ****** CHANGE THIS for your device *****************
            string defaultGatewayConfig = "{\"DeviceName\": \"Huawei\", \"BaudRate\": 115200,  \"DataBits\": 8, \"Handshake\": 0, \"PortName\": \"COM6\"," +
                                          "\"Parity\": 0, \"StopBits\": 1, \"Pin\": \"\", \"DisablePinCheck\": true," +
                                          "\"LicenseKey\": \"1234567890\", \"ProviderMMSC\": \"http://mms.digi.com.my/servlets/mms\", \"ProviderAPN\": \"digimms\"," +
                                          "\"ProviderWAPGateway\": \"203.92.128.160\", \"ProviderAPNAccount\": \"mms\", \"ProviderAPNPassword\": \"mms\"}";
            string gatewayPhoneNumber = "0192292309";   // Phone number for your SIM card

            var gateways = new List<Gateway>
            {
                new Gateway {GatewayID = GlobalConstants.DefaultGatewayID, GatewayPhoneNumber = gatewayPhoneNumber, GatewayConfig = defaultGatewayConfig }
            };
            gateways.ForEach(s => context.Gateways.Add(s));
            context.SaveChanges();


            // Add sample message for testing
            /*
            string sampleMsg = "{\r\n  \"PhoneNumber\": \"0126868739\",\r\n  \"ReceivedDate\": \"2016-09-17T09:19:22+08:00\",\r\n  \"Timezone\": \"+8:00\",\r\n  \"Content\": \"001\",\r\n  \"MessageType\": 0,\r\n  \"TotalPiece\": 1,\r\n  \"CurrentPiece\": 0,\r\n  \"DeliveryStatus\": 0,\r\n  \"DestinationReceivedDate\": \"0001-01-01T00:00:00\",\r\n  \"ValidityTimestamp\": \"0001-01-01T00:00:00\",\r\n  \"Index\": 17,\r\n  \"MessageStatusType\": 0,\r\n  \"ReferenceNo\": 0,\r\n  \"SourcePort\": -1,\r\n  \"DestinationPort\": -1,\r\n  \"GatewayId\": \"Default\",\r\n  \"TotalPieceReceived\": 1,\r\n  \"Status\": 0,\r\n  \"RawMessage\": \"\",\r\n  \"Indexes\": [\r\n    17\r\n  ], \r\n  \"ServiceCentreAddress\": \"60160783001\",\r\n  \"ServiceCentreAddressType\": 2\r\n}";
            var msgs = new List<IncomingMessage>
            {
                 new IncomingMessage() { MsgContent = sampleMsg }
            };
            msgs.ForEach(s => context.IncomingMesages.Add(s));
            context.SaveChanges();
            */

        }
    }
}
