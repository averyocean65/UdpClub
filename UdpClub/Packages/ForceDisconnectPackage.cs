using System.Net;

namespace UdpClub.Packages {
	// i wanna find a more secure way to do that
	public class ForceDisconnectPackage : GenericPackage<ForceDisconnectPackage> {
		public ForceDisconnectPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
			
		}

		public ForceDisconnectPackage() {
			
		}
		
		public override byte[] ToBytes() {
			return new byte[] { Id };
		}
	}
}