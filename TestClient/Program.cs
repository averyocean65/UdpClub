using System;
using System.Threading;
using TestShared;
using TestShared.Packets;
using UdpClub;
using UdpClub.Packages;

namespace TestClient {
	internal class Program {
		private static UdpClientApp _client;
		
		public static void Main(string[] args) {
			PackageManager.RegisterPackets();
			
			PackageHandler.OnPackageParsed += PackageParsedCallback;
			
			_client = new UdpClientApp("127.0.0.1", 8201);
			_client.OnConnected += ConnectedCallback;
			_client.Connect();
			
			while(true) { }
		}

		private static void ConnectedCallback() {
			MessagePacket messagePacket = new MessagePacket("test", "hello there! :3");
			PackageHandler.SendPackage(_client, null, messagePacket);
		}
		
		private static void PackageParsedCallback(BasePackage obj) {
			if (obj is MessagePacket message) {
				Console.WriteLine($"Message packet: {message.Username}: {message.Message}");
			}
		}
	}
}