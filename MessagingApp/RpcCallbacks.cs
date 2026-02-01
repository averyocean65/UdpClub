using System;
using UdpClub.RPCs;

namespace ChatApp {
	public static class RpcCallbacks {
		public static Action<string> OnUserJoin;
		public static Action<string> OnUserLeave;
		
		[Rpc(nameof(UserJoin))]
		public static void UserJoin(string username) {
			if (OnUserJoin == null) {
				return;
			}
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