using System;
using System.Threading;
using UdpClub;

namespace TestServer {
	internal class Program {
		public static void Main(string[] args) {
			UdpBase baseClient = new UdpServerApp("127.0.0.1", 8201);
			baseClient.Connect();
			
			while (true) {
				Console.WriteLine("Async Test");
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
		}
	}
}