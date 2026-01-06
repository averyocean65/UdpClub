using System;
using System.Threading;
using UdpClub;

namespace TestClient {
	internal class Program {
		public static void Main(string[] args) {
			UdpClientApp client = new UdpClientApp("127.0.0.1", 8201);
			client.Connect();

			while (true) {
				Console.WriteLine("Async Test");
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
		}
	}
}