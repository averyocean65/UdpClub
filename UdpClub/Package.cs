using System;
using UdpClub.Utils;

namespace UdpClub {
	public class Package {
		public byte Id;
		public byte[] UnhandledData { get; protected set; }

		public Package(byte[] data) {
			if (data.Length < 1) {
				throw new ArgumentException(nameof(data));
			}

			Id = data[0];
			UnhandledData = ArrayUtils.Subarray(data, 1, data.Length - 1);
			
			Console.WriteLine($"Unhandled data: {BitConverter.ToString(UnhandledData)}");
		}
	}
}