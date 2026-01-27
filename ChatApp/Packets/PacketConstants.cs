using System.Text;

namespace ChatApp.Packets {
    public static class PacketConstants {
        public const char Separator = ':';
        public static readonly Encoding MessageEncoding = Encoding.UTF8;
    }
}