using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using TestShared;
using TestShared.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace TestServer {
	internal class Program {
		private static Dictionary<string, IPEndPoint> _connectedUsers = new Dictionary<string, IPEndPoint>();
		private static UdpBase _client;
		
		public static void Main(string[] args) {
			PackageManager.RegisterPackets();
			
			PackageHandler.OnMessageReceived += MessageReceivedCallback;
			PackageHandler.OnPackageParsed += PackageParsedCallback;
			
			_client = new UdpServerApp("127.0.0.1", 8201);
			_client.Connect();
			
			while (true) { }
		}

		private static void MessageReceivedCallback(byte[] obj, IPEndPoint ep) {
			Console.WriteLine($"Incoming message: {BitConverter.ToString(obj)}");
		}

		private static void PackageParsedCallback(BasePackage obj) {
			if (obj is MessagePacket message) {
				Console.WriteLine($"Message packet from {message.Username}: {message.Message}");
			}

			if (obj is AuthPacket auth) {
				if (_connectedUsers.ContainsKey(auth.Username)) {
					Console.Error.WriteLine($"User \"{auth.Username}\" already connected!");
					
					AuthReturnPacket rejection = new AuthReturnPacket(false);
					PackageHandler.SendPackage(_client, auth.Sender, rejection);
					
					return;
				}
				
				Console.WriteLine($"New user: {auth.Username}");
				_connectedUsers.Add(auth.Username, auth.Sender);
				
				AuthReturnPacket accept = new AuthReturnPacket(true);
				PackageHandler.SendPackage(_client, auth.Sender, accept);
			}
		}
	}
}