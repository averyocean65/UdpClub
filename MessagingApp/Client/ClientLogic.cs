using System;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.Client {
    public class ClientLogic : LogicHandler {
        public static string Username { get; private set; }
        public static UdpBase Client { get; private set; }

        public static Action<string> OnUserJoin;
        public static Action<string> OnUserLeave;
        public static Action<string[]> OnSyncClient;
        
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

        [Rpc(nameof(SyncClient))]
        public static void SyncClient(string[] prevUsers) {
            OnSyncClient.Invoke(prevUsers);
        }
    }
}