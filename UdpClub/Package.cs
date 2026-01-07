using System;

namespace UdpClub {
	public class Package {
		public byte Id;
		protected byte[] UnhandledData;

		public Package(byte[] data) {
			if (data.Length < 1) {
				throw new ArgumentException(nameof(data));
				return;
			}
			
			
		}
	}
}