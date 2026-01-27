using System;

namespace UdpClub.Utils {
    public static class DebugUtils {
        public static void DebugPrintln(string message) {
#if DEBUG
            Console.WriteLine(message);
#endif
        }
        
        public static void DebugPrint(string message) {
#if DEBUG
	        Console.Write(message);
#endif
        }
    }
}