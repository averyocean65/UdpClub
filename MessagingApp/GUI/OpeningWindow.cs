using System;
using System.Windows.Forms;
using ChatApp.Client;
using UdpClub;

namespace ChatAp.GUI {
	public partial class OpeningWindow : Form {
		private LogicHandler _handler;
		private UdpBase _client;
		
		public OpeningWindow() {
			InitializeComponent();
		}

		private int GetPort() {
			bool parseSuccess = int.TryParse(portField.Text, out int parsed);
			if (!parseSuccess || parsed < 1) {
				MessageBox.Show("Port must be a positive number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}

			return parsed;
		}

		private void clientButton_Click(object sender, EventArgs e) {
			int port = GetPort();
			if (port < 0) {
				return;
			}
			
			_client = new UdpClientApp(hostnameField.Text, port);
			_handler = new ClientLogic(_client);
			
			// TODO: Get username + spawn client window
			
		}

		private void serverButton_Click(object sender, EventArgs e) {
			int port = GetPort();
			if (port < 0) {
				return;
			}
			
			_client = new UdpServerApp(hostnameField.Text, port);
			_handler = new ClientLogic(_client);
			
			_handler.Init();
			_handler.RunLoop();
			
			// TODO: spawn server window + redirect console logs
		}
	}
}