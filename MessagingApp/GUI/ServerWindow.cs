using System;
using System.Net;
using System.Timers;
using System.Windows.Forms;
using ChatApp.Server;
using UdpClub.RPCs;

namespace ChatApp.GUI {
	public partial class ServerWindow : Form {
		public ServerWindow() {
			InitializeComponent();

			RpcCallbacks.OnUserJoin += OnUserJoin;
			RpcCallbacks.OnUserLeave += OnUserLeave;

			ServerLogic.OnReceiveMessage += OnReceiveMessage;
		}

		private void OnReceiveMessage(string user, string message) {
			messageList.Items.Add($"[{user}]: {message}");
		}

		private void OnUserJoin(string user) {
			userList.Items.Add(user);
		}
		
		private void OnUserLeave(string user) {
			userList.Items.Remove(user);
		}

		private void kickUserButton_Click(object sender, EventArgs e) {
			if (!(userList.SelectedItem is string selectedUser)) {
				MessageBox.Show("Please select a user from the user list!", "No selection.", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			DialogResult result = MessageBox.Show($"Are you sure you want to kick {selectedUser}?", "Confirm selection.",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				if (!ServerLogic.Users.ContainsKey(selectedUser)) {
					MessageBox.Show("Selected user is no longer in server!", "Error", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return;
				}

				RpcManager.BroadcastRpcToClients(ServerLogic.Client, ServerLogic.Users.Values,
					nameof(RpcCallbacks.OnUserLeave), selectedUser);
			}
		}
	}
}