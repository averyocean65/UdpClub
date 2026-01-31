using System;
using System.Net;
using UdpClub.Packages;
using UdpClub.Utils;

namespace ChatApp.Packets {
    public class AuthReturnPacket : GenericPackage<AuthReturnPacket> {
        public bool Success;
        
        public AuthReturnPacket(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (UnhandledData.Length < 1) {
                throw new ArgumentException("UnhandledData < 1");
            }

            Success = ByteUtils.ByteToBool(UnhandledData[0]);
        }

        public AuthReturnPacket(bool success) {
            Id = GetRequiredId();
            Success = success;
        }
        
        public override byte[] ToBytes() {
            return new[] { Id, ByteUtils.BoolToByte(Success) };
        }
    }
}