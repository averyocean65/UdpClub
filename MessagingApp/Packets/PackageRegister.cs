using UdpClub.Packages;

namespace ChatApp.Packets {
    public static class PackageRegister {
        public static void RegisterPackets() {
            PackageMap.RegisterPacket(typeof(AuthPacket));
            PackageMap.RegisterPacket(typeof(AuthReturnPacket));
            PackageMap.RegisterPacket(typeof(MessagePacket));
        }
    }
}