using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp.GUI {
	public partial class PromptWindow : Form {
		public Action<string> OnSubmitPressed;
		
		private void DestroySelf(string obj) {
			Close();
		}

		public PromptWindow() {
			InitializeComponent();
			OnSubmitPressed += DestroySelf;
		}

		public PromptWindow(string title, string message) : base() {
			Text = title;
			messageLabel.Text = message;
		}

		private void submitButton_Click(object sender, EventArgs e) {
			OnSubmitPressed.Invoke(inputField.Text);
		}
	}
}