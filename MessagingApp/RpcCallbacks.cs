using System;
using UdpClub.RPCs;

using static UdpClub.Utils.DebugUtils;

namespace ChatApp {
	public static class RpcCallbacks {
		public static Action<string> OnUserJoin;
		public static Action<string> OnUserLeave;
		
		[Rpc(nameof(UserJoin))]
		public static void UserJoin(string username) {
			DebugPrintln("OnUserJoin RPC called!");
			if (OnUserJoin == null) {
				DebugPrintln("OnUserJoin is null");
				return;
			}
			
			DebugPrintln($"User {username} joined!");
			OnUserJoin.Invoke(username);
		}
        
		[Rpc(nameof(UserLeave))]
		public static void UserLeave(string username) {
			if (OnUserLeave == null) {
				return;
			}
			OnUserLeave.Invoke(username);
		}
	}
}