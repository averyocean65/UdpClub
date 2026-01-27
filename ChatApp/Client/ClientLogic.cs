using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;

namespace ChatApp.Client {
    public class ClientLogic : LogicHandler {
        public string Username;
        private UdpBase _client;
        
        public ClientLogic(UdpBase client) {
            _client = client;
        }
        
        public void Init() {
            PackageHandler.OnPackageParsed += ClientCallbacks.OnPackageParsed;
            _client.OnConnected += SendAuthPacket;
            Username = Program.PromptUser("Please input your username");

            _client.Connect();
        }

        private void SendAuthPacket() {
            AuthPacket auth = new AuthPacket(Username);
            PackageHandler.SendPackage(_client, null, auth);
        }

        public void RunLoop() {
            string message = "";
            while (true) {
                message = Program.PromptUser("");
            }
        }
    }
}