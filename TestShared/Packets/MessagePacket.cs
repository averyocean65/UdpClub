using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UdpClub.Packages;

namespace TestShared.Packets {
	public sealed class MessagePacket : BasePackage {
		private const char Separator = ':';

		public string Username { get; private set; } = string.Empty;
		public string Message { get; private set; } = string.Empty;
		
		public MessagePacket(byte[] data, IPEndPoint ep) : base(data, ep) {
			if (!IsIdValid(typeof(MessagePacket))) {
				throw new ArgumentException("ID from byte data is not valid!");
			}
			
			string rawMessageContent = Encoding.Default.GetString(UnhandledData);

			int separationIndex = rawMessageContent.IndexOf(Separator);
			if (separationIndex < 0) {
				Username = rawMessageContent;
				return;
			}

			Username = rawMessageContent.Substring(0, separationIndex);
			Message = rawMessageContent.Substring(separationIndex + 1);
		}

		public MessagePacket(string username, string message) {
			Id = GetRequiredId(typeof(MessagePacket));
			Username = username;
			Message = message;
		}
		
		public override byte[] ToBytes() {
			string concat = Username + Separator + Message;
			byte[] concatBytes = Encoding.Default.GetBytes(concat);

			List<byte> outputList = new List<byte> { Id };
			outputList.AddRange(concatBytes);
			return outputList.ToArray();
		}
	}
}