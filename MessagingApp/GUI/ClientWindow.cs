using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ChatApp.Client;
using ChatApp.Packets;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.GUI {
	public partial class ClientWindow : Form {
		private bool _leftAlready = false;
		private bool _joinedAlready = false;

		private readonly object _memberListLock = new object();
		
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
		}

		private void OnLoaded(object sender, EventArgs e) {
			if (!ClientLogic.Client.IsConnected) {
				OnDisconnected();
			}

			Text = $"Chat Client - {ClientLogic.Username}";
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

		private void AddUserLocally(string user) {
			lock(_memberListLock) {
				if (memberList.Items.Contains(user)) {
					return;
				}
				
				// somehow a timeout causes this stuff to not spawn duplicates, so....
				Thread.Sleep(50);
				memberList.Items.Add(user);
			}
		}

		private void OnUserJoin(string user) {
			if (string.Equals(user, ClientLogic.Username, StringComparison.Ordinal) && _joinedAlready) {
				return;
			}

			_joinedAlready = true;
			AddUserLocally(user);
			
			// filter member list just to be sure
			
			messageList.Items.Add($"{user} has joined the chat.");
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
				if (string.Equals(username, ClientLogic.Username, StringComparison.Ordinal) ||
				    memberList.Items.Contains(obj)) {
					continue;
				}

				AddUserLocally(username);
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