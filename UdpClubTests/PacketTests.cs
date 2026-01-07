using NUnit.Framework;
using UdpClub;

namespace UdpClubTests {
	[TestFixture]
	public class PacketTests {
		[Test]
		public void CreatePackage() {
			Package test = new Package(new byte[] { 0x01, 0xFF, 0xAB, 0x0D, 0x00 });
			Assert.AreEqual(test.UnhandledData, new byte[] { 0xFF, 0xAB, 0x0D, 0x00 });
		}
	}
}