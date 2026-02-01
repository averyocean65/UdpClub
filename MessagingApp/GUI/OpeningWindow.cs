using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChatApp.Client;
using ChatApp.Packets;
using ChatApp.Server;
using UdpClub;

namespace ChatApp.GUI {
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
			
			PromptWindow usernamePrompt = new PromptWindow("Input Username", "Please input your username...");
			usernamePrompt.OnSubmitPressed += ClientCanInit;
			usernamePrompt.Show();
		}

		private void ClientCanInit(string username) {
			if (Regex.IsMatch(username, "\\W") || string.IsNullOrEmpty(username)) {
				MessageBox.Show("Please input a valid username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			
			_handler = new ClientLogic(_client, username);
			_handler.Init();
			_handler.RunLoop();

			// spawn client window
			ClientWindow window = new ClientWindow();
			window.Closed += CloseApp;
			
			window.Show();
			
			Hide();
		}

		private void CloseApp(object sender, EventArgs e) {
			Close();
			Environment.Exit(0);
		}

		private void serverButton_Click(object sender, EventArgs e) {
			int port = GetPort();
			if (port < 0) {
				return;
			}
			
			_client = new UdpServerApp(hostnameField.Text, port);
			_handler = new ServerLogic(_client);
			
			_handler.Init();
			_handler.RunLoop();

			// spawn server window
			ServerWindow window = new ServerWindow();
			window.Closed += CloseApp;
			
			window.Show();
			
			Hide();
		}
	}
}