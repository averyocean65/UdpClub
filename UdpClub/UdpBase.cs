using System;
using System.Net.Sockets;

namespace UdpClub {
	public class UdpBase {
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
		public UdpBase(string hostname, int port, bool isServer) {
			InnerClient = new UdpClient(hostname, port);
			IsServer = isServer;
		}
		
		~UdpBase() {
			InnerClient.Dispose();
		}

		/// <summary>
		/// Connects to a specified server.
		/// </summary>
		/// <returns>A boolean stating whether the connection attempt was successful or not.</returns>
		public bool Connect() {
			if (InnerClient.Client.Connected || IsServer) {
				// respond with true, because connection isn't necessary.
				return true;
			}

			try {
				InnerClient.Connect(Hostname, Port);
			}
			catch(Exception ex) {
				Console.Error.WriteLine();
				Console.Error.WriteLine(ex);
				return false;
			}
			
			// dummy text for testing
			Console.WriteLine("yay! we're connected!");
			return true;
		}

		public void Disconnect() {
			if (!InnerClient.Client.Connected) {
				return;
			}
			InnerClient.Close();
		}
	}
}