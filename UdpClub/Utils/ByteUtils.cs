using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UdpClub.Utils {
	public static class ByteUtils {
		public static bool ByteToBool(byte input) {
			return input == 0x00;
		}

		public static byte BoolToByte(bool input) {
			return (byte)(input ? 0x00 : 0x01);
		}

		// From: https://stackoverflow.com/questions/33022660/how-to-convert-byte-array-to-any-type
		public static byte[] ToByteArray<T>(T obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			using(MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static T FromByteArray<T>(byte[] data)
		{
			if(data == null)
				return default(T);
			BinaryFormatter bf = new BinaryFormatter();
			using(MemoryStream ms = new MemoryStream(data))
			{
				object obj = bf.Deserialize(ms);
				return (T)obj;
			}
		}
	}
}