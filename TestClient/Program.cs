using UdpClub;

namespace TestClient {
	internal class Program {
		public static void Main(string[] args) {
			UdpBase baseClient = new UdpBase("127.0.0.1", 8000, false);
			baseClient.Connect();
		}
	}
}