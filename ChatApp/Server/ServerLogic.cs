using System.Collections.Generic;
using System.Net;
using ChatApp.Client;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;

namespace ChatApp.Server {
    public class ServerLogic : LogicHandler {
        public static Dictionary<string, IPEndPoint> users;
        private UdpBase _client;
        
        public ServerLogic(UdpBase client) {
            _client = client;
        }
        
        public void Init() {
            PackageHandler.OnPackageParsed += ServerCallbacks.OnPackageParsed;
            _client.Connect();
        }

        public void RunLoop() {
            while (true) { }
        }
    }
}