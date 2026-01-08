using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UdpClub.Utils;

namespace UdpClub.Packages {
	public abstract class BasePackage {
		public byte Id { get; protected set; }
		protected byte[] UnhandledData;

		protected BasePackage() { }
		
		protected BasePackage(byte[] data) {
			if (data.Length < 1) {
				throw new ArgumentException(nameof(data));
			}

			Id = data[0];
			UnhandledData = ArrayUtils.Subarray(data, 1, data.Length - 1);
		}

		protected virtual byte GetRequiredId(Type type) {
			return PackageMap.Packages
				.FirstOrDefault(x => x.Value == type)
				.Key;
		}
		protected bool IsIdValid(Type type) {
			return GetRequiredId(type) == Id;
		}

		public abstract byte[] ToBytes();
	}
}