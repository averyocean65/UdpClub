using System.ComponentModel;

namespace ChatApp.GUI {
	partial class ServerWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.userList = new System.Windows.Forms.ListBox();
			this.messageList = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.kickUserButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// userList
			// 
			this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
			this.userList.FormattingEnabled = true;
			this.userList.Location = new System.Drawing.Point(350, 25);
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(200, 264);
			this.userList.TabIndex = 0;
			// 
			// messageList
			// 
			this.messageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.messageList.FormattingEnabled = true;
			this.messageList.Location = new System.Drawing.Point(12, 25);
			this.messageList.Name = "messageList";
			this.messageList.Size = new System.Drawing.Size(332, 303);
			this.messageList.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(237, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Messages";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(350, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Users";
			// 
			// kickUserButton
			// 
			this.kickUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.kickUserButton.Location = new System.Drawing.Point(350, 297);
			this.kickUserButton.Name = "kickUserButton";
			this.kickUserButton.Size = new System.Drawing.Size(200, 31);
			this.kickUserButton.TabIndex = 4;
			this.kickUserButton.Text = "Kick User";
			this.kickUserButton.UseVisualStyleBackColor = true;
			this.kickUserButton.Click += new System.EventHandler(this.kickUserButton_Click);
			// 
			// ServerWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 340);
			this.Controls.Add(this.kickUserButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.messageList);
			this.Controls.Add(this.userList);
			this.Name = "ServerWindow";
			this.Text = "Chat Server";
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Button kickUserButton;

		private System.Windows.Forms.ListBox userList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.ListBox messageList;

		#endregion
	}
}