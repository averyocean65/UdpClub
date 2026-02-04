using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChatApp.Client;
using ChatApp.Packets;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.GUI {
	public partial class ClientWindow : Form {
		private bool _leftAlready = false;
		
		public ClientWindow() {
			InitializeComponent();
			
			RpcCallbacks.OnUserJoin += OnUserJoin;
			RpcCallbacks.OnUserLeave += OnUserLeave;
			ClientLogic.OnSyncClient += OnSyncClient;

			ClientLogic.Client.OnDisconnected += OnDisconnected;
			ClientLogic.Client.OnForceDisconnect += OnForceDisconnect;
			
			ClientCallbacks.ReceivedMessage += OnReceiveMessage;

			Closed += OnClosed;
			Load += OnLoaded;
		}

		private void OnForceDisconnect() {
			Close();
			Environment.Exit(0);
		}

		private void OnLoaded(object sender, EventArgs e) {
			if (!ClientLogic.Client.IsConnected) {
				OnDisconnected();
			}
		}

		private void OnDisconnected() {
			MessageBox.Show("Unable to communicate with server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Close();
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);

			if (e.KeyCode == Keys.Return) {
				sendButton_Click(null, null);
			}
		}

		private void OnClosed(object sender, EventArgs e) {
			ClientLogic.Client.OnDisconnected -= OnDisconnected;
			LeaveChatroom();
		}
		
		private void LeaveChatroom() {
			if (_leftAlready) {
				return;
			}
			RpcManager.BroadcastRpc(ClientLogic.Client, nameof(RpcCallbacks.UserLeave), ClientLogic.Username);
			_leftAlready = true;
		}

		private void OnUserJoin(string obj) {
			memberList.Items.Add(obj);
			messageList.Items.Add($"{obj} has joined the chat.");
		}
		
		private void OnUserLeave(string obj) {
			memberList.Items.Remove(obj);
			messageList.Items.Add($"{obj} has left the chat.");
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
		
		private void OnReceiveMessage(string user, string message) {
			messageList.Items.Add($"[{user}]: {message}");
		}
		
		private void sendButton_Click(object sender, EventArgs e) {
			string trimmed = messageField.Text.Trim();
			if (trimmed.Length < 1) {
				return;
			}
			
			MessagePacket messagePacket = new MessagePacket(ClientLogic.Username, messageField.Text);
			PackageHandler.SendPackage(ClientLogic.Client, null, messagePacket);
			
			// emulate receiving message
			OnReceiveMessage(messagePacket.Username, messagePacket.Message);
			
			// clear input field
			messageField.Text = string.Empty;
		}

		private void leaveButton_Click(object sender, EventArgs e) {
			LeaveChatroom();
			Close();
		}
	}
}