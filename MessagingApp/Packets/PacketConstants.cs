using System.Text;

namespace ChatApp.Packets {
    public static class PacketConstants {
        public const byte Separator = 0xFF;
        public static readonly Encoding MessageEncoding = Encoding.UTF8;
    }
}