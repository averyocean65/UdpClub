using System;
using System.Collections.Generic;
using System.Net;
using UdpClub.Packages;
using UdpClub.Utils;

using static UdpClub.Utils.DebugUtils;

namespace ChatApp.Packets {
	public class MessagePacket : GenericPackage<MessagePacket> {
		public string Username;
		public string Message;
		
		public MessagePacket(byte[] data, IPEndPoint ep) : base(data, ep) {
			int separatorIndex = Array.IndexOf(UnhandledData, PacketConstants.Separator);
			if (separatorIndex < 1) {
				throw new ArgumentException("No separator in UnhandledData!");
			}

			Username = PacketConstants.MessageEncoding.GetString(
				UnhandledData.Subarray(0, separatorIndex)
			);
			
			DebugPrint($"Username: {Username}, ");
			
			Message = PacketConstants.MessageEncoding.GetString(
				UnhandledData.Subarray(separatorIndex + 1)
			);
			
			DebugPrintln($"Message: {Message}");
		}

		public MessagePacket(string username, string message) {
			Username = username;
			Message = message;
		}
		
		public override byte[] ToBytes() {
			List<byte> data = new List<byte>() { Id };
			data.AddRange(PacketConstants.MessageEncoding.GetBytes(Username));
			data.Add(PacketConstants.Separator);
			data.AddRange(PacketConstants.MessageEncoding.GetBytes(Message));

			return data.ToArray();
		}
	}
}