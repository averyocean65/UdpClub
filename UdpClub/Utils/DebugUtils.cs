using System;

namespace UdpClub.Utils {
    public static class DebugUtils {
        public static void DebugPrint(string message) {
#if DEBUG
            Console.WriteLine(message);
#endif
        }
    }
}