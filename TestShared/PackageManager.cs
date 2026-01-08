using TestShared.Packets;
using UdpClub.Packages;

namespace TestShared {
	public class PackageManager {
		public static void RegisterPackets() {
			PackageMap.RegisterPacket(typeof(AuthPacket));
			PackageMap.RegisterPacket(typeof(AuthReturnPacket));
			PackageMap.RegisterPacket(typeof(MessagePacket));
		}
	}
}