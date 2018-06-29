namespace SmtpClientDemo.WinForms
{
	partial class SmtpClientDemo
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.textBoxCC = new System.Windows.Forms.TextBox();
			this.textBoxBCC = new System.Windows.Forms.TextBox();
			this.textBoxSubject = new System.Windows.Forms.TextBox();
			this.textBoxServer = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.comboBoxAuth = new System.Windows.Forms.ComboBox();
			this.labelPassword = new System.Windows.Forms.Label();
			this.labelUser = new System.Windows.Forms.Label();
			this.textBoxUser = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.comboBoxSSL = new System.Windows.Forms.ComboBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "To:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "CC:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "BCC:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 113);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Subject:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 139);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Message:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 236);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Server:";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(326, 236);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(29, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "Port:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 262);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(55, 13);
			this.label10.TabIndex = 9;
			this.label10.Text = "SSL/TLS:";
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(326, 262);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(32, 13);
			this.label11.TabIndex = 10;
			this.label11.Text = "Auth:";
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFrom.Location = new System.Drawing.Point(76, 6);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(459, 20);
			this.textBoxFrom.TabIndex = 1;
			// 
			// textBoxTo
			// 
			this.textBoxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTo.Location = new System.Drawing.Point(76, 32);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(459, 20);
			this.textBoxTo.TabIndex = 2;
			// 
			// textBoxCC
			// 
			this.textBoxCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCC.Location = new System.Drawing.Point(76, 58);
			this.textBoxCC.Name = "textBoxCC";
			this.textBoxCC.Size = new System.Drawing.Size(459, 20);
			this.textBoxCC.TabIndex = 3;
			// 
			// textBoxBCC
			// 
			this.textBoxBCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBCC.Location = new System.Drawing.Point(76, 84);
			this.textBoxBCC.Name = "textBoxBCC";
			this.textBoxBCC.Size = new System.Drawing.Size(459, 20);
			this.textBoxBCC.TabIndex = 4;
			// 
			// textBoxSubject
			// 
			this.textBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSubject.Location = new System.Drawing.Point(76, 110);
			this.textBoxSubject.Name = "textBoxSubject";
			this.textBoxSubject.Size = new System.Drawing.Size(459, 20);
			this.textBoxSubject.TabIndex = 5;
			// 
			// textBoxServer
			// 
			this.textBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServer.Location = new System.Drawing.Point(76, 233);
			this.textBoxServer.Name = "textBoxServer";
			this.textBoxServer.Size = new System.Drawing.Size(230, 20);
			this.textBoxServer.TabIndex = 7;
			this.textBoxServer.Leave += new System.EventHandler(this.TextBoxServerOnLeave);
			// 
			// textBoxPort
			// 
			this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPort.Location = new System.Drawing.Point(388, 233);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(147, 20);
			this.textBoxPort.TabIndex = 8;
			this.textBoxPort.Leave += new System.EventHandler(this.TextBoxPortOnLeave);
			// 
			// comboBoxAuth
			// 
			this.comboBoxAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAuth.FormattingEnabled = true;
			this.comboBoxAuth.ItemHeight = 13;
			this.comboBoxAuth.Location = new System.Drawing.Point(388, 259);
			this.comboBoxAuth.Name = "comboBoxAuth";
			this.comboBoxAuth.Size = new System.Drawing.Size(147, 21);
			this.comboBoxAuth.TabIndex = 10;
			// 
			// labelPassword
			// 
			this.labelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelPassword.AutoSize = true;
			this.labelPassword.Location = new System.Drawing.Point(326, 289);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(56, 13);
			this.labelPassword.TabIndex = 23;
			this.labelPassword.Text = "Password:";
			// 
			// labelUser
			// 
			this.labelUser.AutoSize = true;
			this.labelUser.Location = new System.Drawing.Point(12, 289);
			this.labelUser.Name = "labelUser";
			this.labelUser.Size = new System.Drawing.Size(32, 13);
			this.labelUser.TabIndex = 22;
			this.labelUser.Text = "User:";
			// 
			// textBoxUser
			// 
			this.textBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUser.Location = new System.Drawing.Point(76, 286);
			this.textBoxUser.Name = "textBoxUser";
			this.textBoxUser.Size = new System.Drawing.Size(230, 20);
			this.textBoxUser.TabIndex = 11;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPassword.Location = new System.Drawing.Point(388, 287);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(147, 20);
			this.textBoxPassword.TabIndex = 12;
			this.textBoxPassword.UseSystemPasswordChar = true;
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMessage.Location = new System.Drawing.Point(76, 136);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(459, 91);
			this.textBoxMessage.TabIndex = 6;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClose.Location = new System.Drawing.Point(460, 457);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 17;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.BtnCloseOnClick);
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Location = new System.Drawing.Point(76, 312);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxLog.Size = new System.Drawing.Size(459, 76);
			this.textBoxLog.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 315);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(28, 13);
			this.label7.TabIndex = 30;
			this.label7.Text = "Log:";
			// 
			// buttonCopy
			// 
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCopy.Location = new System.Drawing.Point(76, 394);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(117, 23);
			this.buttonCopy.TabIndex = 15;
			this.buttonCopy.Text = "Copy to Clipboard";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.ButtonCopyOnClick);
			// 
			// buttonSend
			// 
			this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSend.Location = new System.Drawing.Point(379, 457);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 16;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.ButtonSendOnClick);
			// 
			// comboBoxSSL
			// 
			this.comboBoxSSL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSSL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSSL.FormattingEnabled = true;
			this.comboBoxSSL.ItemHeight = 13;
			this.comboBoxSSL.Location = new System.Drawing.Point(76, 260);
			this.comboBoxSSL.Name = "comboBoxSSL";
			this.comboBoxSSL.Size = new System.Drawing.Size(230, 21);
			this.comboBoxSSL.TabIndex = 9;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(-3, 439);
			this.progressBar1.MarqueeAnimationSpeed = 0;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(554, 10);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 31;
			// 
			// SmtpClientDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 487);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.comboBoxSSL);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.buttonCopy);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxLog);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.textBoxUser);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.labelUser);
			this.Controls.Add(this.comboBoxAuth);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.textBoxServer);
			this.Controls.Add(this.textBoxSubject);
			this.Controls.Add(this.textBoxBCC);
			this.Controls.Add(this.textBoxCC);
			this.Controls.Add(this.textBoxTo);
			this.Controls.Add(this.textBoxFrom);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MinimumSize = new System.Drawing.Size(563, 526);
			this.Name = "SmtpClientDemo";
			this.Text = "MailKit SMTP Client Demo - WinForms";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxFrom;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.TextBox textBoxCC;
		private System.Windows.Forms.TextBox textBoxBCC;
		private System.Windows.Forms.TextBox textBoxSubject;
		private System.Windows.Forms.TextBox textBoxServer;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.ComboBox comboBoxAuth;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Label labelUser;
		private System.Windows.Forms.TextBox textBoxUser;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button buttonCopy;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.ComboBox comboBoxSSL;
		private System.Windows.Forms.ProgressBar progressBar1;
	}
}

