using System;
using System.Net;

namespace UdpClub.Packages {
	public abstract class GenericPackage<T> : BasePackage
	where T : BasePackage {
		protected GenericPackage() {
			Id = GetRequiredId();
		}
		
		protected GenericPackage(byte[] data, IPEndPoint ep) : base(data, ep) {
			if (!IsPackageType()) {
				throw new ArgumentException($"ID ({Id}) does not match the package {nameof(T)}");
			}
		}
		
		protected byte GetRequiredId() {
			return GetRequiredId(typeof(T));
		}

		public bool IsPackageType() {
			return Id == GetRequiredId();
		}
	}
}