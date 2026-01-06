using System.Net;

namespace UdpClub {
	public class UdpClientApp : UdpBase {
		public UdpClientApp(string hostname, int port) : base(hostname, port, false) { }
	}
}