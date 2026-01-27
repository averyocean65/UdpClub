using System;
using ChatApp.Packets;
using UdpClub.Packages;

namespace ChatApp.Server {
    public static class ServerCallbacks {
        public static void OnPackageParsed(BasePackage package) {
            if (package is AuthPacket auth) {
                
            }
        }
    }
}