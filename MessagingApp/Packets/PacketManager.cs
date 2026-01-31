using ChatApp.Packets;
using UdpClub.Packages;

namespace ChatApp {
    public class PackageRegister {
        public static void RegisterPackets() {
            PackageMap.RegisterPacket(typeof(AuthPacket));
            PackageMap.RegisterPacket(typeof(AuthReturnPacket));
        }
    }
}