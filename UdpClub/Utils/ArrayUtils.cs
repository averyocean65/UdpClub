using System.Linq;

namespace UdpClub.Utils {
	public static class ArrayUtils {
		public static T[] Subarray<T>(T[] array, int start, int length) {
			return array
				.Skip(start)
				.Take(length)
				.ToArray();
		}
	}
}