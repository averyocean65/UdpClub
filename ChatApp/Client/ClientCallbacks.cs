using System;
using ChatApp.Packets;
using UdpClub.Packages;

namespace ChatApp.Client {
    public static class ClientCallbacks {
        public static void OnPackageParsed(BasePackage package) {
            if (package.IsPackageType(typeof(AuthReturnPacket))) {
                AuthReturnPacket authReturn = (AuthReturnPacket)package;
                if (!authReturn.Success) {
                    Console.Error.WriteLine("Authentication rejected by server.");
                    Environment.Exit(0);
                }
                
                Console.WriteLine("Connection authorized!");
            }
        }
    }
}