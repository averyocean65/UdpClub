using System;
using System.Reflection;
using System.Threading;
using TestShared;
using TestShared.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace TestClient {
	internal class Program {
		private static UdpClientApp _client;
		private static string _requestedUsername = string.Empty;
		
		public static void Main(string[] args) {
			PackageManager.RegisterPackets();
			PackageHandler.OnPackageParsed += PackageParsedCallback;

			RPCManager.ExecuteRpc += attribute =>
				RPCManager.InvokeRpcInAssembly(Assembly.GetExecutingAssembly(), attribute);
			RPCManager.ExecuteRpc += (a) => { Console.WriteLine("Invoking RPC!"); };
			
			Console.WriteLine("Input username:");
			_requestedUsername = Console.ReadLine();
			
			_client = new UdpClientApp("127.0.0.1", 8201);
			_client.OnConnected += ConnectedCallback;
			_client.Connect();
			
			while(true) { }
		}

		[RPC(nameof(MyRPC))]
		public static void MyRPC() {
			Console.WriteLine("RPC called!");
		}

		private static void ConnectedCallback() {
			AuthPacket authPacket = new AuthPacket(_requestedUsername);
			PackageHandler.SendPackage(_client, null, authPacket);
		}
		
		private static void PackageParsedCallback(BasePackage obj) {
			if (obj is MessagePacket message) {
				Console.WriteLine($"Message packet from {message.Username}: {message.Message}");
			}
			
			if (obj is AuthReturnPacket success) {
				if (success.Successful) {
					Console.WriteLine("authenticated with the server");
					return;
				}
				
				Console.WriteLine("connection denied!");
				Environment.Exit(1);
			}
		}
	}
}