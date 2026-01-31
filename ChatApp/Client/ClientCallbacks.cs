using System;
using ChatApp.Packets;
using UdpClub.Packages;

using static UdpClub.Utils.DebugUtils;

namespace ChatApp.Client {
    public static class ClientCallbacks {
        public static void OnPackageParsed(BasePackage package) {
            DebugPrintln($"ClientLogic handling package with ID {package.Id}");
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