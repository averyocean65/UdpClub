using System;
using System.Threading;
using TestShared;
using TestShared.Packets;
using UdpClub;
using UdpClub.Packages;

namespace TestServer {
	internal class Program {
		public static void Main(string[] args) {
			PackageManager.RegisterPackets();
			
			PackageHandler.OnMessageReceived += MessageReceivedCallback;
			PackageHandler.OnPackageParsed += PackageParsedCallback;
			
			UdpBase baseClient = new UdpServerApp("127.0.0.1", 8201);
			baseClient.Connect();
			
			while (true) { }
		}

		private static void MessageReceivedCallback(byte[] obj) {
			Console.WriteLine($"Incoming message: {BitConverter.ToString(obj)}");
		}

		private static void PackageParsedCallback(BasePackage obj) {
			if (obj is MessagePacket message) {
				Console.WriteLine($"Message packet: {message.Username}: {message.Message}");
			}
		}
	}
}