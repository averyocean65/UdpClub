using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ChatApp.Server;
using UdpClub.RPCs;

using static UdpClub.Utils.DebugUtils;

namespace ChatApp.GUI {
	public partial class ServerWindow : Form {
		public ServerWindow() {
			InitializeComponent();

			RpcCallbacks.OnUserJoin += OnUserJoin;
			RpcCallbacks.OnUserLeave += OnUserLeave;
			
			ServerLogic.Client.OnKickInitiated += OnClientGotKicked;
			ServerLogic.OnReceiveMessage += OnReceiveMessage;
		}

		private void OnClientGotKicked(IPEndPoint ip) {
			DebugPrintln($"Kicking {ip.Address}");
			if (!ServerLogic.Users.ContainsValue(ip)) {
				return;
			}

			string username = ServerLogic.Users.FirstOrDefault(x => Equals(x.Value, ip)).Key;
			RpcManager.BroadcastRpcToClients(ServerLogic.Client, ServerLogic.Users.Values,
				nameof(RpcCallbacks.UserLeave), username);
		}

		private void OnReceiveMessage(string user, string message) {
			messageList.Items.Add($"[{user}]: {message}");
		}

		private void OnUserJoin(string user) {
			messageList.Items.Add($"-- {user} JOINED --");
			userList.Items.Add(user);
		}
		
		private void OnUserLeave(string user) {
			messageList.Items.Add($"-- {user} LEFT --");
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

				IPEndPoint ip = ServerLogic.Users[selectedUser];
				messageList.Items.Add($"- {selectedUser} GOT KICKED -");
				userList.Items.Remove(selectedUser);
				ServerLogic.Client?.Kick(ip);
			}
		}
	}
}