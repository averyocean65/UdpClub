namespace ChatApp.GUI {
	partial class OpeningWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.label1 = new System.Windows.Forms.Label();
			this.clientButton = new System.Windows.Forms.Button();
			this.serverButton = new System.Windows.Forms.Button();
			this.hostnameField = new System.Windows.Forms.TextBox();
			this.portField = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(288, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Would you like to run as a server or client?";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// clientButton
			// 
			this.clientButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
			this.clientButton.Location = new System.Drawing.Point(12, 91);
			this.clientButton.Name = "clientButton";
			this.clientButton.Size = new System.Drawing.Size(138, 27);
			this.clientButton.TabIndex = 1;
			this.clientButton.Text = "&Client";
			this.clientButton.UseVisualStyleBackColor = true;
			this.clientButton.Click += new System.EventHandler(this.clientButton_Click);
			// 
			// serverButton
			// 
			this.serverButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
			this.serverButton.Location = new System.Drawing.Point(156, 91);
			this.serverButton.Name = "serverButton";
			this.serverButton.Size = new System.Drawing.Size(144, 27);
			this.serverButton.TabIndex = 2;
			this.serverButton.Text = "&Server";
			this.serverButton.UseVisualStyleBackColor = true;
			this.serverButton.Click += new System.EventHandler(this.serverButton_Click);
			// 
			// hostnameField
			// 
			this.hostnameField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hostnameField.Location = new System.Drawing.Point(92, 39);
			this.hostnameField.Name = "hostnameField";
			this.hostnameField.Size = new System.Drawing.Size(208, 20);
			this.hostnameField.TabIndex = 3;
			this.hostnameField.Text = "localhost";
			// 
			// portField
			// 
			this.portField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.portField.Location = new System.Drawing.Point(92, 65);
			this.portField.Name = "portField";
			this.portField.Size = new System.Drawing.Size(208, 20);
			this.portField.TabIndex = 4;
			this.portField.Text = "8000";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Hostname";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(12, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 19);
			this.label3.TabIndex = 6;
			this.label3.Text = "Port";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// OpeningWindow
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(312, 130);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.portField);
			this.Controls.Add(this.hostnameField);
			this.Controls.Add(this.serverButton);
			this.Controls.Add(this.clientButton);
			this.Controls.Add(this.label1);
			this.MaximumSize = new System.Drawing.Size(328, 169);
			this.MinimumSize = new System.Drawing.Size(328, 169);
			this.Name = "OpeningWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UdpClub Demo";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Label label3;

		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.TextBox hostnameField;
		private System.Windows.Forms.TextBox portField;

		private System.Windows.Forms.Button serverButton;

		private System.Windows.Forms.Button clientButton;

		private System.Windows.Forms.Label label1;

		#endregion
	}
}