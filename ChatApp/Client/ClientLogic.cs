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
            
            Username = Program.PromptUser("Please input your username");
            Client.Connect();
        }

        private void SendAuthPacket() {
            AuthPacket auth = new AuthPacket(Username);
            PackageHandler.SendPackage(Client, null, auth);
        }

        public void RunLoop() {
            string message = "";
            while (true) {
                message = Program.PromptUser("");
            }
        }

        [Rpc(nameof(WelcomeUser))]
        public static void WelcomeUser(string username) {
            Console.WriteLine($"{username} has entered the chat.");
        }
    }
}