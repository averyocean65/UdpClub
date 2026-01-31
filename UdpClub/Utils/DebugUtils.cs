using System;
using System.IO;

namespace UdpClub.Utils {
    public static class DebugUtils {
	    public static bool PauseConsoleWriting = false;
	    
	    public static void DebugPrintln(string message) {
#if DEBUG
		    if (PauseConsoleWriting) return;
		    Console.WriteLine(message);
#endif		    
        }
        
        public static void DebugPrint(string message) {
#if DEBUG
	        if (PauseConsoleWriting) return;
	        Console.Write(message);
#endif
        }
    }
}