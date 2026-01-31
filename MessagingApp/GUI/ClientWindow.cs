using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChatApp.Client;
using UdpClub.RPCs;

namespace ChatApp.GUI {
	public partial class ClientWindow : Form {
		public ClientWindow() {
			InitializeComponent();

			ClientLogic.OnUserJoin += OnUserJoin;
			ClientLogic.OnUserLeave += OnUserJoin;

			Closed += OnClosed;
		}

		private void OnClosed(object sender, EventArgs e) {
			LeaveChatroom();
		}
		
		private void LeaveChatroom() {
			// TODO: implement leaving logic
		}

		private void OnUserJoin(string obj) {
			memberList.Items.Add(obj);
		}
		
		private void OnUserLeave(string obj) {
			memberList.Items.Remove(obj);
		}

		private void sendButton_Click(object sender, EventArgs e) {
			
		}

		private void leaveButton_Click(object sender, EventArgs e) {
			throw new System.NotImplementedException();
		}
	}
}