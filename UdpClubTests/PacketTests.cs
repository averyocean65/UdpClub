using NUnit.Framework;
using UdpClub;
using UdpClub.Packages;

namespace UdpClubTests {
	[TestFixture]
	public class PacketTests {
		[Test]
		public void CreatePackage() {
			TestPacket test = new TestPacket(new byte[] { 0x01, 0xFF, 0xAB, 0x0D, 0x00 });
			Assert.AreEqual(test.UnhandledData, new byte[] { 0xFF, 0xAB, 0x0D, 0x00 });
		}
	}
}