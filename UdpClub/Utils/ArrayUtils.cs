using System.Linq;

namespace UdpClub.Utils {
	public static class ArrayUtils {
		public static T[] Subarray<T>(this T[] array, int start, int length) {
			return array
				.Skip(start)
				.Take(length)
				.ToArray();
		}

		public static T[] Subarray<T>(this T[] array, int start) {
			int length = array.Length - start;
			return Subarray(array, start, length);
		}
	}
}