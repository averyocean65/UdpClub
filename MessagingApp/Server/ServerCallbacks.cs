using System;
using ChatApp.Client;
using ChatApp.Packets;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;
using static UdpClub.Utils.DebugUtils;

namespace ChatApp.Server {
    public static class ServerCallbacks {
        public static void OnPackageParsed(BasePackage package) {
            DebugPrintln($"Server parsing package: {package.Id}");
            
            if (package.IsPackageType(typeof(AuthPacket))) {
                AuthPacket auth = (AuthPacket)package;
                DebugPrintln($"Handling auth packet: {auth.Username}");

                bool canLogIn = !ServerLogic.Users.ContainsKey(auth.Username);
                if (canLogIn) {
                    Console.WriteLine($"New user: {auth.Username}");
                    ServerLogic.Users.Add(auth.Username, auth.Sender);

                    RpcManager.BroadcastRpcToClients(ServerLogic.Client, ServerLogic.Users.Values,
                        nameof(ClientLogic.UserJoin), auth.Username);
                }
                
                AuthReturnPacket authReturn = new AuthReturnPacket(canLogIn);
                
                DebugPrintln($"Sending package to: {auth.Sender}");
                PackageHandler.SendPackage(ServerLogic.Client, auth.Sender, authReturn);
            }
        }
    }
}