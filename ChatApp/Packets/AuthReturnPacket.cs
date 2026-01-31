using System;
using System.Collections.Generic;
using System.Net;
using UdpClub.Packages;
using UdpClub.Utils;

namespace ChatApp.Packets {
    public class AuthReturnPacket : BasePackage {
        public bool Success;

        public AuthReturnPacket(byte[] data, IPEndPoint ep) : base(data, ep) {
            if (!IsPackageType(typeof(AuthReturnPacket))) {
                throw new ArgumentException("Invalid ID");
            }
            
            if (UnhandledData.Length < 1) {
                throw new ArgumentException("UnhandledData < 1");
            }

            Success = ByteUtils.ByteToBool(UnhandledData[0]);
        }

        public AuthReturnPacket(bool success) {
            Id = PackageMap.GetPackageId(typeof(AuthReturnPacket));
            Success = success;
        }
        
        public override byte[] ToBytes() {
            return new[] { Id, ByteUtils.BoolToByte(Success) };
        }
    }
}