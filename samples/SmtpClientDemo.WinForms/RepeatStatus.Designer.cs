namespace SmtpClientDemo.WinForms
{
	partial class RepeatStatus
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
			this.labelMessageCount = new System.Windows.Forms.Label();
			this.buttonStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Messages Sent:";
			// 
			// labelMessageCount
			// 
			this.labelMessageCount.AutoSize = true;
			this.labelMessageCount.Location = new System.Drawing.Point(92, 12);
			this.labelMessageCount.Name = "labelMessageCount";
			this.labelMessageCount.Size = new System.Drawing.Size(13, 13);
			this.labelMessageCount.TabIndex = 1;
			this.labelMessageCount.Text = "0";
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(70, 50);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "S&top";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStopOnClick);
			// 
			// RepeatStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(214, 79);
			this.ControlBox = false;
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.labelMessageCount);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "RepeatStatus";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Repeated Send - Status";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelMessageCount;
		private System.Windows.Forms.Button buttonStop;
	}
}