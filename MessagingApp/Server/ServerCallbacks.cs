using System;
using System.Linq;
using System.Windows.Forms;
using ChatApp.Client;
using ChatApp.GUI;
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
                    
                    // execute RPC on server (this is different from a user leaving, because this is called on the server)
                    RpcCallbacks.OnUserJoin.Invoke(auth.Username);

                    // notify others of new user!
                    RpcManager.BroadcastRpcToClients(ServerLogic.Client, ServerLogic.Users.Values,
                        nameof(RpcCallbacks.UserJoin), auth.Username, runOnServer: true);
                    
                    // make sure the new client knows the other members
                    if (ServerLogic.Users.Count > 1) {
                        RpcManager.BroadcastRpc(ServerLogic.Client, auth.Sender, nameof(ClientLogic.SyncClient),
                            ServerLogic.Users.Keys.ToArray(), runOnServer: false);
                    }
                }
                
                AuthReturnPacket authReturn = new AuthReturnPacket(canLogIn);
                
                DebugPrintln($"Sending package to: {auth.Sender}");
                PackageHandler.SendPackage(ServerLogic.Client, auth.Sender, authReturn);
            } else if (package.IsPackageType(typeof(MessagePacket))) {
                // Redirect package
                DebugPrintln("Redirecting message package...");
                PackageHandler.SendPackageToAllExcept(ServerLogic.Client, ServerLogic.Users.Values, package.Sender, package);

                if (ServerLogic.OnReceiveMessage != null) {
                    DebugPrintln("Invoking ServerLogic.OnReceiveMessage");
                    MessagePacket messagePacket = (MessagePacket)package;
                    ServerLogic.OnReceiveMessage.Invoke(messagePacket.Username, messagePacket.Message);
                }
            }
        }
    }
}