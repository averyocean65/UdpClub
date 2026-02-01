using System;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;

using static UdpClub.Utils.DebugUtils;

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
				try {
					object obj = bf.Deserialize(ms);
					return (T)obj;
				}
				catch (Exception ex) {
					Console.Error.WriteLine(ex.Message);
					return default(T);
				}
			}
		}

		private static byte[] Deflation(byte[] data, CompressionMode mode, CompressionLevel level) {
			MemoryStream input = new MemoryStream(data);
			MemoryStream output = new MemoryStream();

			if (mode == CompressionMode.Compress) {
				using (DeflateStream deflateStream = new DeflateStream(output, level)) {
					deflateStream.Write(data, 0, data.Length);
				}
			}
			else {
				using (DeflateStream deflateStream = new DeflateStream(input, mode)) {
					deflateStream.CopyTo(output);
				}	
			}

			return output.ToArray();
		}

		public static byte[] Compress(this byte[] data) {
			byte[] compressed = Deflation(data, CompressionMode.Compress, CompressionLevel.Fastest);
			DebugPrintln($"Uncompressed size: {data.Length}; Compressed size: {compressed.Length}");
			return compressed;
		}
		
		public static byte[] Decompress(this byte[] data) {
			return Deflation(data, CompressionMode.Decompress, CompressionLevel.Fastest);
		}
	}
}