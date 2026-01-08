using System;
using System.Collections.Generic;
using System.Net;
using UdpClub.Packages;
using UdpClub.Utils;

namespace TestShared.Packets {
	public sealed class AuthReturnPacket : BasePackage {
		public bool Successful { get; private set; }

		public AuthReturnPacket(byte[] data, IPEndPoint ep) : base(data, ep) {
			if (!IsIdValid(typeof(AuthReturnPacket))) {
				throw new ArgumentException("Invalid ID in data bytes!");
			}

			if (UnhandledData.Length < 1) {
				throw new ArgumentException("Data requires truth value!");
			}

			Successful = ByteUtils.ByteToBool(data[1]);
		}

		public AuthReturnPacket(bool successful) {
			Id = PackageMap.GetPackageId(typeof(AuthReturnPacket)); 
			Successful = successful;
		}
		
		public override byte[] ToBytes() {
			return new byte[] { Id, ByteUtils.BoolToByte(Successful) };
		}
	}
}