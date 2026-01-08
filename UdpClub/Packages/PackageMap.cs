using System;
using System.Collections.Generic;
using System.Linq;

namespace UdpClub.Packages {
	public static class PackageMap {
		public static Dictionary<byte, Type> Packages = new Dictionary<byte, Type>();
		private static byte _latestIndex;
		
		public static void RegisterPacket(Type packet, bool throwExceptionIfPacketIsRegistered = false) {
			if (!packet.IsSubclassOf(typeof(BasePackage))) {
				throw new ArgumentException($"{nameof(packet)} must be a child of {nameof(BasePackage)}");
			}

			if (Packages.ContainsValue(packet) && throwExceptionIfPacketIsRegistered) {
				throw new ArgumentException($"{nameof(packet)} is already registered!");
			}

			if (_latestIndex == byte.MaxValue) {
				throw new ApplicationException(
					$"No available indices for new packet. Please make less than {byte.MaxValue} packets!");
			}
			
			Packages.Add(_latestIndex, packet);
			_latestIndex++;
		}

		public static Type GetPackageType(byte id) {
			return Packages
				.FirstOrDefault(x => x.Key == id)
				.Value;
		}
		
		public static byte GetPackageId(Type type) {
			return Packages
				.FirstOrDefault(x => x.Value == type)
				.Key;
		}
	}
}