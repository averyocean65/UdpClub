using System;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.Client {
    public class ClientLogic : LogicHandler {
        public static string Username { get; private set; }
        public static UdpBase Client { get; private set; }
        
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

        

        [Rpc(nameof(SyncClient))]
        public static void SyncClient(string[] prevUsers) {
            if (OnSyncClient == null) {
                return;
            }
            
            OnSyncClient.Invoke(prevUsers);
        }
    }
}