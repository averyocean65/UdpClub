using System;
using System.Windows.Forms;
using ChatApp.Packets;
using UdpClub.Packages;

using static UdpClub.Utils.DebugUtils;

namespace ChatApp.Client {
    public static class ClientCallbacks {
        public static Action<string, string> ReceivedMessage;
        
        public static void OnPackageParsed(BasePackage package) {
            DebugPrintln($"ClientLogic handling package with ID {package.Id}");
            if (package.IsPackageType(typeof(AuthReturnPacket))) {
                AuthReturnPacket authReturn = (AuthReturnPacket)package;
                if (!authReturn.Success) {
                    MessageBox.Show("Server didn't permit authorization.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                
                Console.WriteLine("Connection authorized!");
            }

            if (package.IsPackageType(typeof(MessagePacket))) {
                MessagePacket msg = (MessagePacket)package;
                ReceivedMessage.Invoke(msg.Username, msg.Message);
            }
        }
    }
}