using System.ComponentModel;

namespace ChatApp.GUI {
	partial class ClientWindow {
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
			this.messageList = new System.Windows.Forms.ListBox();
			this.memberList = new System.Windows.Forms.ListBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.leaveButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.messageField = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// messageList
			// 
			this.messageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.messageList.FormattingEnabled = true;
			this.messageList.Location = new System.Drawing.Point(12, 25);
			this.messageList.Name = "messageList";
			this.messageList.Size = new System.Drawing.Size(414, 212);
			this.messageList.TabIndex = 0;
			// 
			// memberList
			// 
			this.memberList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
			this.memberList.FormattingEnabled = true;
			this.memberList.Location = new System.Drawing.Point(432, 25);
			this.memberList.Name = "memberList";
			this.memberList.Size = new System.Drawing.Size(97, 212);
			this.memberList.TabIndex = 1;
			// 
			// sendButton
			// 
			this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sendButton.Location = new System.Drawing.Point(359, 243);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(67, 30);
			this.sendButton.TabIndex = 2;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// leaveButton
			// 
			this.leaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.leaveButton.Location = new System.Drawing.Point(432, 243);
			this.leaveButton.Name = "leaveButton";
			this.leaveButton.Size = new System.Drawing.Size(97, 30);
			this.leaveButton.TabIndex = 3;
			this.leaveButton.Text = "Leave";
			this.leaveButton.UseVisualStyleBackColor = true;
			this.leaveButton.Click += new System.EventHandler(this.leaveButton_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(414, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Messages";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(432, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Users";
			// 
			// messageField
			// 
			this.messageField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.messageField.Location = new System.Drawing.Point(12, 249);
			this.messageField.Name = "messageField";
			this.messageField.Size = new System.Drawing.Size(341, 20);
			this.messageField.TabIndex = 6;
			// 
			// ClientWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 285);
			this.Controls.Add(this.messageField);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.leaveButton);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.memberList);
			this.Controls.Add(this.messageList);
			this.KeyPreview = true;
			this.Name = "ClientWindow";
			this.Text = "ClientWindow";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.ListBox memberList;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Button leaveButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox messageField;

		private System.Windows.Forms.ListBox messageList;

		#endregion
	}
}