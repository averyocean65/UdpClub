using System;
using NUnit.Framework;
using UdpClub.Utils;

namespace UdpClubTests {
	[TestFixture]
	public class ArrayTests {
		[Test]
		public void TestSubarray() {
			int[] baseArray = new int[] { 21, 5, 92, 10, 159 };
			
			int[] subExpected1 = new int[] { 21, 5, 92 };
			int[] subExpected2 = new int[] { 10, 159 };
			int[] subExpected3 = new int[] { 5 };

			int[] subResult1 = ArrayUtils.Subarray(baseArray, 0, 3);
			int[] subResult2 = ArrayUtils.Subarray(baseArray, 3, 2);
			int[] subResult3 = ArrayUtils.Subarray(baseArray, 1, 1);

			Assert.AreEqual(subExpected1, subResult1);
			Assert.AreEqual(subExpected2, subResult2);
			Assert.AreEqual(subExpected3, subResult3);
		}
	}
}