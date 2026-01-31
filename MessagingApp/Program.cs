using System;
using System.Windows.Forms;
using ChatApp.GUI;
using ChatApp.Packets;

namespace ChatApp {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			PackageRegister.RegisterPackets();
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new OpeningWindow());
		}
	}
}