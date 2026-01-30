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
        }
        
        public void Init() {
            PackageHandler.OnPackageParsed += ServerCallbacks.OnPackageParsed;
            Client.Connect();
        }

        public void RunLoop() {
            while (true) { }
        }
    }
}