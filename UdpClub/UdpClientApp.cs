using System.Net;
using System.Reflection;

namespace UdpClub {
	public class UdpClientApp : UdpBase {
		public UdpClientApp(string hostname, int port) : base(Assembly.GetCallingAssembly(), hostname, port, false) { }
	}
}