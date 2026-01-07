using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UdpClub {
	public abstract class UdpBase {
		/// <summary>
		/// The bare knuckles UDP client.
		/// </summary>
		protected UdpClient InnerClient;
		
		/// <summary>
		/// The hostname / IP address of the connection.
		/// </summary>
		public string Hostname { get; private set; }
		
		/// <summary>
		/// The port of the connection.
		/// </summary>
		public int Port { get; private set; }
		
		/// <summary>
		/// Decides whether to treat certain operations like a server or like a client.
		/// </summary>
		public bool IsServer { get; private set; }
		
		/// <summary>
		/// Creates a new instance of class UdpBase.
		/// </summary>
		/// <param name="hostname">The address for the connection. Assigned to <see cref="Hostname"/></param>
		/// <param name="port">The port that the client will be listening on / sending to. Assigned to <see cref="Port"/></param>
		/// <param name="isServer">Whether to internally treat the instance as a server or client. Assigned to <see cref="IsServer"/></param>
		protected UdpBase(string hostname, int port, bool isServer) {
			Hostname = hostname;
			Port = port;
			IsServer = isServer;

			if (IsServer) {
				// server-specific constructor
				InnerClient = new UdpClient(Port);
				return;
			}
			
			InnerClient = new UdpClient();
		}
		
		~UdpBase() {
			Disconnect();
			InnerClient.Dispose();
		}

		/// <summary>
		/// Connects to a specified server.
		/// </summary>
		/// <returns>A boolean stating whether the connection attempt was successful or not.</returns>
		public virtual bool Connect() {
			if (InnerClient.Client.Connected) {
				Console.Error.WriteLine("You're already connected!");
				return false;
			}
			
			if (IsServer) {
				Task.Run(NetworkLoop);
				return true;
			}

			try {
				InnerClient.Connect(Hostname, Port);
				InnerClient.Send(new byte[] { 0xFE, 0xDC, 0xBA, 0xFF }, 4);
				Task.Run(NetworkLoop);
			}
			catch(Exception ex) {
				Console.Error.WriteLine(ex);
				return false;
			}
			
			return true;
		}

		/// <summary>
		/// Handles running different functions such as <see cref="HandlePackage"/>
		/// </summary>
		private void NetworkLoop() {
			IPEndPoint ep = null;
			while (InnerClient.Client.Connected || IsServer) {
				Console.WriteLine("Waiting for new packages...");
				HandlePackage(ref ep);
			}
			
			Console.WriteLine("Connected ended!");
		}

		protected virtual void HandlePackage(ref IPEndPoint endPoint) {
			byte[] received = InnerClient.Receive(ref endPoint);
			Console.WriteLine($"Bytes from {endPoint.Address}: {BitConverter.ToString(received)}");
			
			// Send package back
			if (IsServer) {
				InnerClient.Send(new byte[] { 0x00, 0xAA, 0xBB, 0xFF }, 4, endPoint);
			}
			else {
				InnerClient.Send(new byte[] { 0x00, 0xAA, 0xBB, 0xFF }, 4);
			}
			Console.WriteLine("Sent package back!");
		}

		public virtual void Disconnect() {
			if (!InnerClient.Client.Connected) {
				return;
			}
			InnerClient.Close();
		}
	}
}