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
			ClientLogic.OnUserLeave += OnUserLeave;
			ClientLogic.OnSyncClient += OnSyncClient;

			Closed += OnClosed;
		}

		private void OnClosed(object sender, EventArgs e) {
			LeaveChatroom();
		}
		
		private void LeaveChatroom() {
			RpcManager.BroadcastRpc(ClientLogic.Client, nameof(ClientLogic.UserLeave), ClientLogic.Username, true);
		}

		private void OnUserJoin(string obj) {
			memberList.Items.Add(obj);
		}
		
		private void OnUserLeave(string obj) {
			memberList.Items.Remove(obj);
		}
		
		private void OnSyncClient(string[] obj) {
			if (obj == null) {
				return;
			}

			if (obj.Length < 1) {
				return;
			}
			
			foreach(string username in obj)
			{
				if (username == ClientLogic.Username) {
					continue;
				}
				
				memberList.Items.Add(username);
			}
		}

		private void sendButton_Click(object sender, EventArgs e) {
			
		}

		private void leaveButton_Click(object sender, EventArgs e) {
			LeaveChatroom();
			Close();
		}
	}
}