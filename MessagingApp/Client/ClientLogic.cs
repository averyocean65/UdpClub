using System;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp.Client {
    public class ClientLogic : LogicHandler {
        public string Username;
        public readonly UdpBase Client;
        
        public ClientLogic(UdpBase client) {
            Client = client;
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

        public void RunLoop() {
            while (true) {
                
            }
        }

        [Rpc(nameof(WelcomeUser))]
        public static void WelcomeUser(string username) {
            Console.WriteLine($"{username} has entered the chat.");
        }
    }
}