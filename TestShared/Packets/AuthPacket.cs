using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Packages;

namespace TestShared.Packets {
	public sealed class AuthPacket : BasePackage {
		public string Username { get; private set; }
		
		public AuthPacket(byte[] data, IPEndPoint ep) : base(data, ep) {
			if (!IsIdValid(typeof(AuthPacket))) {
				throw new ArgumentException("ID from data bytes is invalid!");
			}

			Username = Encoding.Default.GetString(UnhandledData);
		}

		public AuthPacket(string username) {
			Id = PackageMap.GetPackageId(typeof(AuthPacket));
			Username = username;
		}
		
		public override byte[] ToBytes() {
			byte[] converted = Encoding.Default.GetBytes(Username);
			List<byte> data = new List<byte>() { Id };
			data.AddRange(converted);
			return data.ToArray();
		}
	}
}