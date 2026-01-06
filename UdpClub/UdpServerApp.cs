using System.Net;

namespace UdpClub {
	public class UdpServerApp : UdpBase {
		public UdpServerApp(string hostname, int port) : base(hostname, port, true) { }
	}
}