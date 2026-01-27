using System;
using UdpClub.Packages;

namespace ChatApp.Client {
    public static class ClientCallbacks {
        public static void OnPackageParsed(BasePackage package) {
            Console.WriteLine($"package to client (id): {package.Id}");
        }
    }
}