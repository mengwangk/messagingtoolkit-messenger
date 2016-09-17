using MessagingToolkit.Messenger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger.Configurator
{
    public partial class frmConfigurator : Form
    {
        public frmConfigurator()
        {
            InitializeComponent();
        }

        private void frmConfigurator_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenerateModemConfiguration_Click(object sender, EventArgs e)
        {
            DeviceInfo deviceInfo = new DeviceInfo();
            deviceInfo.DeviceName = "Huawei";
            deviceInfo.BaudRate = 115200;
            deviceInfo.DataBits = 8;
            deviceInfo.PortName = "COM8";
            deviceInfo.Handshake = 0;
            deviceInfo.Parity = 0;
            deviceInfo.Pin = "";
            deviceInfo.StopBits = 1;
            deviceInfo.DisablePinCheck = true;
            deviceInfo.LicenseKey = "1234567890";
            deviceInfo.ProviderAPN = "";
            deviceInfo.ProviderAPNAccount = "";
            deviceInfo.ProviderAPNPassword = "";
            deviceInfo.ProviderMMSC = "";
            deviceInfo.ProviderMMSC = "";
            deviceInfo.ProviderWAPGateway = "";

            txtOutput.Text = JsonConvert.SerializeObject(deviceInfo, Formatting.Indented);
        }
    }
}
