using System;
using System.Windows.Forms;

namespace ChatApp.GUI {
	public partial class PromptWindow : Form {
		public Action<string> OnSubmitPressed;
		
		public string Title { get; private set; }
		public string Message { get; private set; }
		
		private void DestroySelf(string obj) {
			Close();
		}

		private void OnShown(object sender, EventArgs e) {
			Text = Title;
			messageLabel.Text = Message;
		}

		public PromptWindow(string title, string message) {
			InitializeComponent();
			OnSubmitPressed += DestroySelf;
			Shown += OnShown;
			
			Title = title;
			Message = message;
		}

		private void submitButton_Click(object sender, EventArgs e) {
			OnSubmitPressed.Invoke(inputField.Text);
		}
	}
}