namespace UdpClub.Utils {
	public static class ByteUtils {
		public static bool ByteToBool(byte input) {
			return input == 0x00;
		}

		public static byte BoolToByte(bool input) {
			return (byte)(input ? 0x00 : 0x01);
		}
	}
}