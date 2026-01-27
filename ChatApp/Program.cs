using System;
using System.Runtime.InteropServices;
using ChatApp.Client;
using ChatApp.Server;
using UdpClub;
using UdpClub.Packages;
using UdpClub.RPCs;

namespace ChatApp {
    internal class Program {
        private const string Hostname = "127.0.0.1";
        private const int Port = 8000;
        
        private static UdpBase _client;

        public static string PromptUser(string prompt) {
            if (prompt.Length > 0) {
                Console.WriteLine(prompt);
            }

            Console.Write("> ");
            return Console.ReadLine();
        }
        
        public static void Main(string[] args) {
            PackageRegister.RegisterPackets();
            
            void InputError() {
                Console.Error.WriteLine("Please write either 'c' or 's' to select a client or server!");
                Environment.Exit(1);
            }
            
            string selection = "";
            
            if (args.Length < 1) {
                Console.WriteLine("Hint: you can also run the app with -c or -s to bypass this dialog!");
                selection = PromptUser("Would you like to run the app as a client or server? (c or s)").ToLower();
            } else {
                selection = args[0];
            }
            
            switch (selection) {
                case "client":
                case "c":
                    RunClient();
                    break;
                case "server":
                case "s":
                    RunServer();
                    break;
                default:
                    InputError();
                    break;
            }
        }

        private static void RunServer() {
            Console.WriteLine("Running as server...");
            _client = new UdpServerApp(Hostname, Port);
            
            ServerLogic logic = new ServerLogic(_client);
            logic.Init();
            logic.RunLoop();
        }
        
        private static void RunClient() {
            Console.WriteLine("Running as client...");
            _client = new UdpClientApp(Hostname, Port);

            ClientLogic logic = new ClientLogic(_client);
            logic.Init();
            logic.RunLoop();
        }
    }
}