using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using UdpClub.Packages;
using UdpClub.RPCs;
using static UdpClub.Utils.DebugUtils;

namespace UdpClub {
	public class UdpServerApp : UdpBase {
		public static List<IPEndPoint> UsersToKick { get; private set; }

		public Action<IPEndPoint> OnKickInitiated;
		public Action<IPEndPoint> OnKickExecuted;

		public UdpServerApp(string hostname, int port) : base(Assembly.GetCallingAssembly(), hostname, port, true) {
			UsersToKick = new List<IPEndPoint>();
		}

		public void Kick(IPEndPoint ip) {
			if (!RegisteredIPs.Contains(ip)) {
				throw new ArgumentException($"IP is not present in {nameof(RegisteredIPs)}");
			}

			OnKickInitiated?.Invoke(ip);
			UsersToKick.Add(ip);
		}

		protected override bool RunIpChecks(IPEndPoint ep, byte[] data) {
			if (UsersToKick.Contains(ep)) {
				DebugPrintln($"Kicking: {ep.Address}");
				
				PackageHandler.SendPackage(this, ep, new ForceDisconnectPackage());
				UsersToKick.Remove(ep);

				OnKickInitiated?.Invoke(ep);
				return false;
			}

			return true;
		}
	}
}