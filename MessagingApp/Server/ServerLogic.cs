using System.Collections.Generic;
using System.Net;
using ChatApp.Client;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;

namespace ChatApp.Server {
    public class ServerLogic : LogicHandler {
        public static Dictionary<string, IPEndPoint> Users;
        public static UdpBase Client;
        
        public ServerLogic(UdpBase client) {
            Users = new Dictionary<string, IPEndPoint>();
            Client = client;

            ClientLogic.OnUserLeave += HandleUserLeave;
        }

        private void HandleUserLeave(string obj) {
            Users.Remove(obj);
        }

        public void Init() {
            PackageHandler.OnPackageParsed += ServerCallbacks.OnPackageParsed;
            Client.Connect();
        }

        public void RunLoop() { }
    }
}