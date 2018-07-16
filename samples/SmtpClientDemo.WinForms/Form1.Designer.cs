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
			this.buttonListServerCapabilities = new System.Windows.Forms.Button();
			this.groupBoxRepeat = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textBoxBatchSize = new System.Windows.Forms.NumericUpDown();
			this.buttonRepeatedSend = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxRepeatSeconds = new System.Windows.Forms.NumericUpDown();
			this.trackBarLogSetting = new System.Windows.Forms.TrackBar();
			this.label14 = new System.Windows.Forms.Label();
			this.buttonSavePassword = new System.Windows.Forms.Button();
			this.groupBoxRepeat.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textBoxBatchSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxRepeatSeconds)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLogSetting)).BeginInit();
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
			this.textBoxFrom.TabIndex = 0;
			// 
			// textBoxTo
			// 
			this.textBoxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTo.Location = new System.Drawing.Point(76, 32);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(459, 20);
			this.textBoxTo.TabIndex = 1;
			// 
			// textBoxCC
			// 
			this.textBoxCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCC.Location = new System.Drawing.Point(76, 58);
			this.textBoxCC.Name = "textBoxCC";
			this.textBoxCC.Size = new System.Drawing.Size(459, 20);
			this.textBoxCC.TabIndex = 2;
			// 
			// textBoxBCC
			// 
			this.textBoxBCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBCC.Location = new System.Drawing.Point(76, 84);
			this.textBoxBCC.Name = "textBoxBCC";
			this.textBoxBCC.Size = new System.Drawing.Size(459, 20);
			this.textBoxBCC.TabIndex = 3;
			// 
			// textBoxSubject
			// 
			this.textBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSubject.Location = new System.Drawing.Point(76, 110);
			this.textBoxSubject.Name = "textBoxSubject";
			this.textBoxSubject.Size = new System.Drawing.Size(459, 20);
			this.textBoxSubject.TabIndex = 4;
			// 
			// textBoxServer
			// 
			this.textBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxServer.Location = new System.Drawing.Point(76, 233);
			this.textBoxServer.Name = "textBoxServer";
			this.textBoxServer.Size = new System.Drawing.Size(230, 20);
			this.textBoxServer.TabIndex = 6;
			this.textBoxServer.Leave += new System.EventHandler(this.TextBoxServerOnLeave);
			// 
			// textBoxPort
			// 
			this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPort.Location = new System.Drawing.Point(388, 233);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(147, 20);
			this.textBoxPort.TabIndex = 7;
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
			this.comboBoxAuth.TabIndex = 9;
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
			this.textBoxUser.TabIndex = 10;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPassword.Location = new System.Drawing.Point(388, 287);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(121, 20);
			this.textBoxPassword.TabIndex = 11;
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
			this.textBoxMessage.TabIndex = 5;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClose.Location = new System.Drawing.Point(460, 498);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 14;
			this.buttonClose.Text = "C&lose";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseOnClick);
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
			this.textBoxLog.Size = new System.Drawing.Size(459, 89);
			this.textBoxLog.TabIndex = 14;
			this.textBoxLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxLogOnMouseDoubleClick);
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
			this.buttonCopy.Location = new System.Drawing.Point(76, 408);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(117, 23);
			this.buttonCopy.TabIndex = 15;
			this.buttonCopy.TabStop = false;
			this.buttonCopy.Text = "&Copy to Clipboard";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.ButtonCopyOnClick);
			// 
			// buttonSend
			// 
			this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSend.Location = new System.Drawing.Point(379, 498);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 13;
			this.buttonSend.Text = "&Send";
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
			this.comboBoxSSL.TabIndex = 8;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(0, 441);
			this.progressBar1.MarqueeAnimationSpeed = 0;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(555, 10);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 31;
			// 
			// buttonListServerCapabilities
			// 
			this.buttonListServerCapabilities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonListServerCapabilities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonListServerCapabilities.Location = new System.Drawing.Point(388, 408);
			this.buttonListServerCapabilities.Name = "buttonListServerCapabilities";
			this.buttonListServerCapabilities.Size = new System.Drawing.Size(147, 23);
			this.buttonListServerCapabilities.TabIndex = 35;
			this.buttonListServerCapabilities.TabStop = false;
			this.buttonListServerCapabilities.Text = "List Server Capabilities";
			this.buttonListServerCapabilities.UseVisualStyleBackColor = true;
			this.buttonListServerCapabilities.Click += new System.EventHandler(this.ButtonListServerCapabilitiesOnClick);
			// 
			// groupBoxRepeat
			// 
			this.groupBoxRepeat.Controls.Add(this.label13);
			this.groupBoxRepeat.Controls.Add(this.textBoxBatchSize);
			this.groupBoxRepeat.Controls.Add(this.buttonRepeatedSend);
			this.groupBoxRepeat.Controls.Add(this.label12);
			this.groupBoxRepeat.Controls.Add(this.textBoxRepeatSeconds);
			this.groupBoxRepeat.Location = new System.Drawing.Point(7, 457);
			this.groupBoxRepeat.Name = "groupBoxRepeat";
			this.groupBoxRepeat.Size = new System.Drawing.Size(262, 68);
			this.groupBoxRepeat.TabIndex = 38;
			this.groupBoxRepeat.TabStop = false;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(192, 38);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(55, 13);
			this.label13.TabIndex = 42;
			this.label13.Text = "batch size";
			// 
			// textBoxBatchSize
			// 
			this.textBoxBatchSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.textBoxBatchSize.Location = new System.Drawing.Point(121, 36);
			this.textBoxBatchSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.textBoxBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.textBoxBatchSize.Name = "textBoxBatchSize";
			this.textBoxBatchSize.Size = new System.Drawing.Size(65, 20);
			this.textBoxBatchSize.TabIndex = 2;
			this.textBoxBatchSize.TabStop = false;
			this.textBoxBatchSize.ThousandsSeparator = true;
			this.textBoxBatchSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// buttonRepeatedSend
			// 
			this.buttonRepeatedSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRepeatedSend.Location = new System.Drawing.Point(10, 23);
			this.buttonRepeatedSend.Name = "buttonRepeatedSend";
			this.buttonRepeatedSend.Size = new System.Drawing.Size(102, 23);
			this.buttonRepeatedSend.TabIndex = 0;
			this.buttonRepeatedSend.TabStop = false;
			this.buttonRepeatedSend.Text = "&Repeated Send";
			this.buttonRepeatedSend.UseVisualStyleBackColor = true;
			this.buttonRepeatedSend.Click += new System.EventHandler(this.ButtonRepeatedSendOnClick);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(192, 17);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(61, 13);
			this.label12.TabIndex = 39;
			this.label12.Text = "sec interval";
			// 
			// textBoxRepeatSeconds
			// 
			this.textBoxRepeatSeconds.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.textBoxRepeatSeconds.Location = new System.Drawing.Point(121, 13);
			this.textBoxRepeatSeconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.textBoxRepeatSeconds.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.textBoxRepeatSeconds.Name = "textBoxRepeatSeconds";
			this.textBoxRepeatSeconds.Size = new System.Drawing.Size(65, 20);
			this.textBoxRepeatSeconds.TabIndex = 1;
			this.textBoxRepeatSeconds.TabStop = false;
			this.textBoxRepeatSeconds.ThousandsSeparator = true;
			this.textBoxRepeatSeconds.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// trackBarLogSetting
			// 
			this.trackBarLogSetting.Location = new System.Drawing.Point(7, 344);
			this.trackBarLogSetting.Maximum = 1;
			this.trackBarLogSetting.Name = "trackBarLogSetting";
			this.trackBarLogSetting.Size = new System.Drawing.Size(63, 42);
			this.trackBarLogSetting.TabIndex = 39;
			this.trackBarLogSetting.Scroll += new System.EventHandler(this.TrackBarLogSettingOnScroll);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(7, 375);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(66, 13);
			this.label14.TabIndex = 40;
			this.label14.Text = "Screen - File";
			// 
			// buttonSavePassword
			// 
			this.buttonSavePassword.Image = global::SmtpClientDemo.WinForms.Properties.Resources.save16x16;
			this.buttonSavePassword.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonSavePassword.Location = new System.Drawing.Point(514, 286);
			this.buttonSavePassword.Name = "buttonSavePassword";
			this.buttonSavePassword.Size = new System.Drawing.Size(21, 22);
			this.buttonSavePassword.TabIndex = 41;
			this.buttonSavePassword.TabStop = false;
			this.buttonSavePassword.UseVisualStyleBackColor = true;
			this.buttonSavePassword.Click += new System.EventHandler(this.ButtonSavePasswordOnClick);
			// 
			// SmtpClientDemo
			// 
			this.AcceptButton = this.buttonSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 533);
			this.Controls.Add(this.buttonSavePassword);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.trackBarLogSetting);
			this.Controls.Add(this.groupBoxRepeat);
			this.Controls.Add(this.buttonListServerCapabilities);
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
			this.groupBoxRepeat.ResumeLayout(false);
			this.groupBoxRepeat.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textBoxBatchSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBoxRepeatSeconds)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLogSetting)).EndInit();
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
		private System.Windows.Forms.Button buttonListServerCapabilities;
		private System.Windows.Forms.GroupBox groupBoxRepeat;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown textBoxBatchSize;
		private System.Windows.Forms.Button buttonRepeatedSend;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown textBoxRepeatSeconds;
		private System.Windows.Forms.TrackBar trackBarLogSetting;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button buttonSavePassword;
	}
}

