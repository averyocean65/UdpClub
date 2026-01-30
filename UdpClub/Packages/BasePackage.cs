using System;
using System.Linq;
using System.Net;
using UdpClub.Utils;

namespace UdpClub.Packages {
	public abstract class BasePackage {
		public byte Id { get; protected set; }
		protected byte[] UnhandledData;
		
		public IPEndPoint Sender { get; protected set; }

		protected BasePackage() { }
		
		protected BasePackage(byte[] data, IPEndPoint ep) {
			if (data.Length < 1) {
				throw new ArgumentException(nameof(data));
			}

			Sender = ep;
			Id = data[0];
			UnhandledData = ArrayUtils.Subarray(data, 1, data.Length - 1);
		}

		protected virtual byte GetRequiredId(Type type) {
			return PackageMap.Packages
				.FirstOrDefault(x => x.Value == type)
				.Key;
		}
		
		public bool IsPackageType(Type type) {
			return GetRequiredId(type) == Id;
		}

		public abstract byte[] ToBytes();
	}
}