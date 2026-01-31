using System.Collections.Generic;
using System.Net;
using UdpClub.Packages;

using static ChatApp.Packets.PacketConstants;

namespace ChatApp.Packets {
    public sealed class AuthPacket : BasePackage {
        public string Username { get; private set; }
        
        public AuthPacket(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (!IsPackageType(typeof(AuthPacket))) {
                throw new System.InvalidCastException("IDs don't match!");
            }

            Username = MessageEncoding.GetString(UnhandledData);
        }

        public AuthPacket(string username) {
            Id = PackageMap.GetPackageId(typeof(AuthPacket));
            Username = username;
        }
        
        public override byte[] ToBytes() {
            List<byte> bytes = new List<byte>() { Id };
            bytes.AddRange(MessageEncoding.GetBytes(Username));
            return bytes.ToArray();
        }
    }
}