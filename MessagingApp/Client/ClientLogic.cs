using System;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.Client {
    public class ClientLogic : LogicHandler {
        public readonly string Username;
        public readonly UdpBase Client;

        public static Action<string> OnUserJoin;
        public static Action<string> OnUserLeave;
        
        public ClientLogic(UdpBase client, string username) {
            Client = client;
            Username = username;
        }

        public void Init() {
            PackageHandler.OnPackageParsed += ClientCallbacks.OnPackageParsed;
            Client.OnConnected += SendAuthPacket;
            Client.Connect();
        }

        private void SendAuthPacket() {
            AuthPacket auth = new AuthPacket(Username);
            PackageHandler.SendPackage(Client, null, auth);
        }

        public void RunLoop() { }

        [Rpc(nameof(UserJoin))]
        public static void UserJoin(string username) {
            OnUserJoin.Invoke(username);
        }
        
        [Rpc(nameof(UserLeave))]
        public static void UserLeave(string username) {
            OnUserLeave.Invoke(username);
        }
    }
}