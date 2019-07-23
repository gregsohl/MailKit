#region Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MailKit;

using MailKit.Net.Smtp;
using MailKit.Security;

#endregion Namespaces

namespace SmtpClientDemo.WinForms
{
	public partial class SmtpClientDemo : Form
	{
		#region Constructors

		public SmtpClientDemo()
		{
			InitializeComponent();
		}

		#endregion Constructors

		#region Protected Methods

		protected override void OnLoad(
			EventArgs e)
		{
			base.OnLoad(e);

			m_Client = GetClient();

			if (m_Client is SmtpClientSystemNetMail)
			{
				Text = ".NET SMTP Client Demo - WinForms";
				buttonListServerCapabilities.Enabled = false;
				buttonCertificateTest.Enabled = false;
			}

			m_LogStream = new MemoryStream();
			m_Client.Logger = new ProtocolLogger(m_LogStream);

			LoadSslComboBoxValues();
			LoadFormDefaultValuesFromConfig();
		}

		private ISmtpClient GetClient()
		{
			string clientType = ConfigurationManager.AppSettings[APP_SETTINGS_CLIENT_TYPE].ToUpper();

			switch (clientType)
			{
				case APP_SETTINGS_CLIENT_TYPE_DOTNET:
					return new SmtpClientSystemNetMail();

				case APP_SETTINGS_CLIENT_TYPE_MAILKIT:
				default:
					return new SmtpClientMailKit(ServerCertificateValidationCallback);
			}
		}

		#endregion Protected Methods

		#region Private Constants

		private const string APP_SETTINGS_CLIENT_TYPE = "clientType";
		private const string APP_SETTINGS_CLIENT_TYPE_DOTNET = "DOTNET";
		private const string APP_SETTINGS_CLIENT_TYPE_MAILKIT = "MAILKIT";

		private const string APP_SETTINGS_FROM = "from";
		private const string APP_SETTINGS_TO = "to";
		private const string APP_SETTINGS_CC = "cc";
		private const string APP_SETTINGS_BCC = "bcc";
		private const string APP_SETTINGS_SUBJECT = "subject";
		private const string APP_SETTINGS_MESSAGE = "message";
		private const string APP_SETTINGS_SERVER = "server";
		private const string APP_SETTINGS_PORT = "port";
		private const string APP_SETTINGS_SSL = "ssl";
		private const string APP_SETTINGS_USER = "user";
		private const string APP_SETTINGS_PASSWORD = "password";
		private const string APP_SETTINGS_AUTH = "auth";

		private const string NONE = "None";
		private const string AUTO = "Auto";

		private const string SECURE_SOCKET_OPTION_SSL_ON_CONNECT = "SSL on Connect";
		private const string SECURE_SOCKET_OPTION_START_TLS = "StartTLS";
		private const string SECURE_SOCKET_OPTION_START_TLS_WHEN_AVAILABLE = "StartTLS when available";

		#endregion Private Constants

		#region Private Fields

		private string m_Server;
		private int m_Port;
		private SecureSocketOptions m_SecureSocketOptions;

		private string m_AuthorizationMechanism;
		private string m_User;
		private string m_Password;

		private string m_From;
		private string m_To;
		private string m_Cc;
		private string m_Bcc;
		private string m_Subject;
		private string m_MessageBody;

		private bool m_Cancel;
		private RepeatStatus m_RepeatStatus;

		private ISmtpClient m_Client;
		private Stream m_LogStream;

		#endregion Private Fields

		#region Private Methods

		private async void LoadFormDefaultValuesFromConfig()
		{
			m_From = ConfigurationManager.AppSettings[APP_SETTINGS_FROM];
			textBoxFrom.Text = m_From;

			m_To = ConfigurationManager.AppSettings[APP_SETTINGS_TO];
			textBoxTo.Text = m_To;

			m_Cc = ConfigurationManager.AppSettings[APP_SETTINGS_CC];
			textBoxCC.Text = m_Cc;

			m_Bcc = ConfigurationManager.AppSettings[APP_SETTINGS_BCC];
			textBoxBCC.Text = m_Bcc;

			m_Subject = ConfigurationManager.AppSettings[APP_SETTINGS_SUBJECT];
			textBoxSubject.Text = m_Subject;

			m_MessageBody = ConfigurationManager.AppSettings[APP_SETTINGS_MESSAGE];
			textBoxMessage.Text = m_MessageBody;

			m_Server = ConfigurationManager.AppSettings[APP_SETTINGS_SERVER];
			textBoxServer.Text = m_Server;

			string port = ConfigurationManager.AppSettings[APP_SETTINGS_PORT];
			textBoxPort.Text = port;
			Int32.TryParse(port, out m_Port);

			string sslConfiguration = ConfigurationManager.AppSettings[APP_SETTINGS_SSL];

			if (!string.IsNullOrEmpty(sslConfiguration))
			{
				SecureSocketOptions valueToSelect = SecureSocketOptions.Auto;
				comboBoxSSL.SelectedValue = valueToSelect;

				foreach (KeyValuePair<string, SecureSocketOptions> item in comboBoxSSL.Items)
				{
					if (item.Value.ToString() == sslConfiguration)
					{
						comboBoxSSL.SelectedValue = item.Value;
						break;
					}
				}

				m_SecureSocketOptions = (SecureSocketOptions)comboBoxSSL.SelectedValue;
			}

			m_User = ConfigurationManager.AppSettings[APP_SETTINGS_USER];
			textBoxUser.Text = m_User;

			string password = ConfigurationManager.AppSettings[APP_SETTINGS_PASSWORD];
			m_Password = Encryption.DpapiDecrypt(password);

			textBoxPassword.Text = m_Password;

			await UpdateAuthComboBoxValues();
			m_AuthorizationMechanism = ConfigurationManager.AppSettings[APP_SETTINGS_AUTH];
			
			comboBoxAuth.SelectedItem = m_AuthorizationMechanism;
			UpdateCredentialsVisibility();

			comboBoxSSL.SelectedValueChanged += ComboBoxSslOnSelectedValueChanged;
			comboBoxAuth.SelectedValueChanged += ComboBoxAuthOnSelectedValueChanged;
		}

		private void LoadAuthComboBoxValues()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(LoadAuthComboBoxValues));

				return;
			}

			List<string> authComboBoxData = new List<string> { NONE };

			int initialIndex = 0;

			if (m_Client.IsConnected)
			{
				if (m_Client.HasCapability(SmtpCapabilities.Authentication))
				{
					if (m_Client.AuthenticationMechanisms.Count > 0)
					{
						authComboBoxData.Add(AUTO);
						initialIndex = 1;
					}

					// Limit the list to the intersection of those supported by both the server and MailKit library
					foreach (string serverSupportedAuthenticationMechanism in m_Client.AuthenticationMechanisms)
					{
						if (SaslMechanism.IsSupported(serverSupportedAuthenticationMechanism))
						{
							authComboBoxData.Add(serverSupportedAuthenticationMechanism);
						}
					}
				}
			}

			comboBoxAuth.DataSource = new BindingSource(authComboBoxData, null);
			comboBoxAuth.SelectedIndex = initialIndex;
		}

		private void LoadSslComboBoxValues()
		{
			Dictionary<string, SecureSocketOptions> sslComboBoxData = new Dictionary<string, SecureSocketOptions>();

			sslComboBoxData.Add(NONE, SecureSocketOptions.None);
			sslComboBoxData.Add(AUTO, SecureSocketOptions.Auto);
			sslComboBoxData.Add(SECURE_SOCKET_OPTION_SSL_ON_CONNECT, SecureSocketOptions.SslOnConnect);
			sslComboBoxData.Add(SECURE_SOCKET_OPTION_START_TLS, SecureSocketOptions.StartTls);
			sslComboBoxData.Add(SECURE_SOCKET_OPTION_START_TLS_WHEN_AVAILABLE, SecureSocketOptions.StartTlsWhenAvailable);

			comboBoxSSL.DataSource = new BindingSource(sslComboBoxData, null);
			comboBoxSSL.DisplayMember = "Key";
			comboBoxSSL.ValueMember = "Value";
		}

		private async void TextBoxServerOnLeave(object sender, EventArgs e)
		{
			if (m_Server != textBoxServer.Text)
			{
				m_Server = textBoxServer.Text;
				await UpdateAuthComboBoxValues();
			}
		}

		private async void TextBoxPortOnLeave(object sender, EventArgs e)
		{
			if (m_Port.ToString() != textBoxPort.Text)
			{
				if (Int32.TryParse(textBoxPort.Text, out m_Port))
				{
					await UpdateAuthComboBoxValues();
				}
			}
		}

		private async void ComboBoxSslOnSelectedValueChanged(object sender, EventArgs e)
		{
			await UpdateAuthComboBoxValues();
		}

		private void ComboBoxAuthOnSelectedValueChanged(object sender, EventArgs e)
		{
			UpdateCredentialsVisibility();
		}

		private void UpdateCredentialsVisibility()
		{
			m_AuthorizationMechanism = GetAuthenticationMethod();

			bool show = m_AuthorizationMechanism != NONE;

			labelUser.Visible = show;
			textBoxUser.Visible = show;
			labelPassword.Visible = show;
			textBoxPassword.Visible = show;
		}

		private void ButtonClearLogOnClick(object sender, EventArgs e)
		{
			textBoxLog.Clear();
		}

		private void ButtonCopyOnClick(object sender, EventArgs e)
		{
			Clipboard.SetText(textBoxLog.Text);
		}

		private async void ButtonSendOnClick(
			object sender,
			EventArgs e)
		{
			UpdateFormState(false);
			ReadDataFromControls();

			try
			{
				// MimeMessage message = CreateMessage();
				System.Net.Mail.MailMessage message = CreateMessage();

				LogMessage($"{DateTime.Now} - BEGIN SEND");

				var sendEmailTask = m_Client.TrySend(message);
				await sendEmailTask;
				m_Client.Disconnect();

				PutLogInTextbox();

				if (sendEmailTask.Result.Success)
				{
					MessageBox.Show(
						"Email has been sent successfully",
						"Sent successfully",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					string resultMessage;

					if (sendEmailTask.Result.Exception != null)
					{
						resultMessage = sendEmailTask.Result.Exception.Message;
					}
					else
					{
						resultMessage = "Unknown failure sending email.";
					}

					MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("A failure occurred sending the message.\r\n\r\n" + ex.Message);
			}
			finally
			{
				LogMessage($"{DateTime.Now} - END SEND");
				UpdateFormState(true);
			}
		}

		private void ButtonCloseOnClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private string GetAuthenticationMethod()
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(GetAuthenticationMethod));
			}

			return (string)comboBoxAuth.SelectedValue;
		}

		private int GetPortNumber()
		{
			int portNumber;

			if (!int.TryParse(textBoxPort.Text, out portNumber))
			{
				throw new FormatException("Parameter Port should be integer");
			}

			if (portNumber < 1)
			{
				throw new FormatException("Parameter Port should more than 0");
			}

			if (portNumber > 65535)
			{
				throw new FormatException("Parameter Port should less than 65536");
			}

			return portNumber;
		}

		private SecureSocketOptions GetSecureSocketOption()
		{
			if (InvokeRequired)
			{
				return (SecureSocketOptions)Invoke(new Func<SecureSocketOptions>(GetSecureSocketOption));
			}

			SecureSocketOptions secureSocketOptions = (SecureSocketOptions)comboBoxSSL.SelectedValue;
			return secureSocketOptions;
		}

		private string GetInputField(TextBox inputField)
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<String>(() => GetInputField(inputField)));
			}

			return inputField.Text;
		}

		private void ReadDataFromControls()
		{
			m_Server = GetInputField(textBoxServer);
			m_Port = GetPortNumber();
			m_SecureSocketOptions = GetSecureSocketOption();

			m_User = GetInputField(textBoxUser);
			m_Password = GetInputField(textBoxPassword);
			m_AuthorizationMechanism = GetAuthenticationMethod();

			m_From = GetInputField(textBoxFrom);
			m_To = GetInputField(textBoxTo);
			m_Cc = GetInputField(textBoxCC);
			m_Bcc = GetInputField(textBoxBCC);
			m_Subject = GetInputField(textBoxSubject);
			m_MessageBody = GetInputField(textBoxMessage);

			m_Client.Server = m_Server;
			m_Client.Port = m_Port;
			m_Client.SecureSocketOption = m_SecureSocketOptions;

			m_Client.User = m_User;
			m_Client.Password = m_Password;
			m_Client.Authentication = m_AuthorizationMechanism;
		}

		private System.Net.Mail.MailMessage CreateMessage(string messageSuffix = "")
		{
			//MimeMessage message = new MimeMessage();

			if (string.IsNullOrWhiteSpace(m_From))
			{
				throw new ArgumentNullException("From", "Parameter From should be specified");
			}

			// message.From.Add(new MailboxAddress(m_From));

			if (string.IsNullOrWhiteSpace(m_To))
			{
				throw new ArgumentNullException("To", "Parameter To should be specified");
			}

			// message.To.Add(new MailboxAddress(m_To));
			System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(m_From, m_To, m_Subject, m_MessageBody + messageSuffix);

			if (!string.IsNullOrWhiteSpace(m_Cc))
			{
				//message.Cc.Add(new MailboxAddress(m_Cc));
				message.CC.Add(new System.Net.Mail.MailAddress(m_Cc));
			}

			if (!string.IsNullOrWhiteSpace(m_Bcc))
			{
				// message.Bcc.Add(new MailboxAddress(m_Bcc));
				message.Bcc.Add(new System.Net.Mail.MailAddress(m_Bcc));
			}

			//message.Subject = m_Subject;

			//message.Body = new TextPart("plain")
			//{
			//	Text = m_MessageBody + messageSuffix
			//};

			return message;
		}

		private void UpdateFormState(bool enabled)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					UpdateFormState(enabled);
				}));

				return;
			}

			Enabled = enabled;
			progressBar1.Style = enabled ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
			progressBar1.MarqueeAnimationSpeed = enabled ? 0 : 10;
		}

		private async Task UpdateAuthComboBoxValues()
		{
			UpdateFormState(false);

			try
			{
				if ((!string.IsNullOrWhiteSpace(textBoxServer.Text)) &&
					(!string.IsNullOrWhiteSpace(textBoxPort.Text)))
				{
					ReadDataFromControls();

					Task createClientTask = m_Client.Connect();
					await createClientTask.ContinueWith(t => LoadAuthComboBoxValues());
					m_Client.Disconnect();
				}
			}
			catch
			{
				LoadAuthComboBoxValues();
			}
			finally
			{
				UpdateFormState(true);
			}
		}

		private async void ButtonListServerCapabilitiesOnClick(object sender, EventArgs e)
		{
			UpdateFormState(false);

			ReadDataFromControls();

			try
			{
				await m_Client.Connect();

				string capabilities = ListCapabilities();

				textBoxLog.Text += capabilities;

				m_Client.Disconnect();
			}
			catch
			{
				// ignored
			}
			finally
			{
				UpdateFormState(true);
			}
		}

		private string ListCapabilities()
		{
			StringBuilder capabilities = new StringBuilder();

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
			{
				var mechanisms = string.Join(", ", m_Client.AuthenticationMechanisms);
				capabilities.AppendLine(string.Format("SASL mechanisms: {0}", mechanisms));
			}

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.Size))
			{
				capabilities.AppendLine(string.Format("Message size restriction: {0}.", m_Client.MaxSize));
			}

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.Dsn))
			{
				capabilities.AppendLine("Supports delivery-status notifications.");
			}

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.EightBitMime))
			{
				capabilities.AppendLine("Supports Content-Transfer-Encoding: 8bit");
			}

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.BinaryMime))
			{
				capabilities.AppendLine("Supports Content-Transfer-Encoding: binary");
			}

			if (m_Client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
			{
				capabilities.AppendLine("Supports UTF-8 in message headers.");
			}

			return capabilities.ToString();
		}

		private async void ButtonRepeatedSendOnClick(object sender, EventArgs e)
		{
			UpdateFormState(false);

			ReadDataFromControls();

			m_RepeatStatus = new RepeatStatus();
			m_RepeatStatus.StopClicked += StatusFormOnStopClicked;
			m_RepeatStatus.Show(this);

			try
			{
				await Task.Run(RepeatSend);
			}
			finally
			{
				m_RepeatStatus.Dispose();
				m_RepeatStatus = null;

				UpdateFormState(true);
			}
		}

		private void StatusFormOnStopClicked(object sender, EventArgs e)
		{
			m_Cancel = true;
		}

		private async Task<bool> RepeatSend()
		{
			int interval = Convert.ToInt32(textBoxRepeatSeconds.Value);
			int batchSize = Convert.ToInt32(textBoxBatchSize.Value);

			int countTotal = 0;

			m_Client.BatchSize = batchSize;

			try
			{
				m_Cancel = false;

				while (!m_Cancel)
				{
					// MimeMessage message = CreateMessage(" #" + (countTotal + 1));
					System.Net.Mail.MailMessage message = CreateMessage(" #" + (countTotal + 1));

					SendResult sendResult = await m_Client.TrySend(message);

					if (!sendResult.Success)
					{
						break;
					}

					countTotal++;

					m_RepeatStatus.MessageSent();

					Sleep(interval);
				}
			}
			finally
			{
				m_Client.Disconnect();
			}

			return true;
		}

		private void Sleep(int sleepSeconds)
		{
			// Do the sleep in small chunks to support quicker cancellation.
			for (int count = 0; count < sleepSeconds * 2; count++)
			{
				Thread.Sleep(500);
				if (m_Cancel)
				{
					break;
				}
			}
		}

		private void TrackBarLogSettingOnScroll(object sender, EventArgs e)
		{
			if (trackBarLogSetting.Value == 0)
			{
				textBoxLog.Text = string.Empty;

				if (m_Client.Logger != null)
				{
					m_Client.Logger.Dispose();
					m_LogStream = null;
				}

				m_LogStream = new MemoryStream();
				m_Client.Logger = new ProtocolLogger(m_LogStream);
			}
			else
			{
				using (SaveFileDialog dialog = new SaveFileDialog())
				{
					dialog.Title = "SMTP Log File";
					dialog.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
					dialog.FilterIndex = 1;
					dialog.OverwritePrompt = false;

					if (dialog.ShowDialog(this) == DialogResult.OK)
					{
						textBoxLog.Text = "< log to file: " + dialog.FileName + " >";

						if (m_Client.Logger != null)
						{
							m_Client.Logger.Dispose();
							m_LogStream = null;
						}

						m_LogStream = File.Open(dialog.FileName, FileMode.Append, FileAccess.Write, FileShare.Read);
						m_Client.Logger = new ProtocolLogger(m_LogStream);
					}
					else
					{
						// User didn't specify a log file. Set logging back to screen.
						trackBarLogSetting.Value = 0;
					}
				}
			}
		}

		private void TextBoxLogOnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (m_LogStream != null)
			{
				m_LogStream = new MemoryStream();
				m_Client.Logger = new ProtocolLogger(m_LogStream);
				textBoxLog.Clear();
			}
		}

		private void TextBoxLogKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				textBoxLog.SelectAll();
			}
		}

		private void ButtonSavePasswordOnClick(object sender, EventArgs e)
		{
			string encryptedPassword = Encryption.DpapiEncrypt(textBoxPassword.Text);
			AddOrUpdateAppSettings("Password", encryptedPassword);
		}

		public static void AddOrUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}
		}

		private async void ButtonCertificateTestOnClick(object sender, EventArgs e)
		{
			UpdateFormState(false);
			ReadDataFromControls();

			try
			{
				LogMessage($"{DateTime.Now} - BEGIN CERTIFICATE TEST");

				var connectTask = m_Client.Connect();

				await connectTask;

				m_Client.Disconnect();

				PutLogInTextbox();
			}
			catch (SslHandshakeException exception)
			{
				LogMessage(exception.Message);
				if (exception.InnerException != null)
				{
					LogMessage(exception.InnerException.Message);
				}
			}
			finally
			{
				LogMessage($"{DateTime.Now} - END CERTIFICATE TEST");
				UpdateFormState(true);
			}
		}

		private bool ServerCertificateValidationCallback(
			object sender,
			X509Certificate certificate,
			X509Chain chain,
			SslPolicyErrors sslpolicyerrors)
		{
			if ((checkBoxDisplayCertificate.Checked) &&
				(certificate is X509Certificate2))
			{
				X509Certificate2UI.DisplayCertificate((X509Certificate2) certificate);
			}

			if (checkBoxLogCertificates.Checked)
			{
				int index = 0;
				foreach (X509ChainElement chainElement in chain.ChainElements)
				{
					index++;
					DumpCertificate(chainElement.Certificate, $"Certificate {index}");
				}
			}

			if (sslpolicyerrors != SslPolicyErrors.None)
			{
				LogMessage("Certificate has Errors: " + sslpolicyerrors);
			}

			LogMessage();

			return true;
		}

		private void DumpCertificate(
			X509Certificate2 certificate,
			string label)
		{
			using (StreamWriter streamWriter = GetLogStreamWriter())
			{
				streamWriter.WriteLine();
				streamWriter.WriteLine("Certificate Chain Index '{0}'", label);
				streamWriter.WriteLine("Friendly Name: {0}", certificate.FriendlyName);
				streamWriter.WriteLine("Issued To: {0}", certificate.GetNameInfo(X509NameType.SimpleName, false));
				streamWriter.WriteLine("Subject: {0}", certificate.SubjectName);
				streamWriter.WriteLine("Serial number: {0}", certificate.SerialNumber);
				streamWriter.WriteLine("Thumbprint: {0}", certificate.Thumbprint);
				streamWriter.WriteLine("Issuer: {0}", certificate.IssuerName.Name);
				streamWriter.WriteLine("Valid from: {0:d}", certificate.NotBefore);
				streamWriter.WriteLine("Valid to: {0:d}", certificate.NotAfter);
				streamWriter.WriteLine("Extensions");

				foreach (X509Extension extension in certificate.Extensions)
				{
					streamWriter.WriteLine("\t" + extension.Oid.FriendlyName + "(" + extension.Oid.Value + "):");

					if (extension.Oid.FriendlyName == "Key Usage")
					{
						X509KeyUsageExtension ext = (X509KeyUsageExtension) extension;
						streamWriter.WriteLine("\t\tKey Usage: {0}", ext.KeyUsages);
					}

					if (extension.Oid.FriendlyName == "Basic Constraints")
					{
						X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension) extension;
						streamWriter.WriteLine(
							"\t\tBasic Constraints - CertificateAuthority: {0}",
							ext.CertificateAuthority);
						streamWriter.WriteLine(
							"\t\tBasic Constraints - HasPathLengthConstraint: {0}",
							ext.HasPathLengthConstraint);
						streamWriter.WriteLine(
							"\t\tBasic Constraints - PathLengthConstraint: {0}",
							ext.PathLengthConstraint);
					}

					if (extension.Oid.FriendlyName == "Subject Key Identifier")
					{
						X509SubjectKeyIdentifierExtension ext = (X509SubjectKeyIdentifierExtension) extension;
						streamWriter.WriteLine("\t\tSubject Key Identifier: {0}", ext.SubjectKeyIdentifier);
					}

					if (extension.Oid.FriendlyName == "Enhanced Key Usage")
					{
						X509EnhancedKeyUsageExtension ext = (X509EnhancedKeyUsageExtension) extension;
						OidCollection oids = ext.EnhancedKeyUsages;

						foreach (Oid oid in oids)
						{
							streamWriter.WriteLine("\t\tEnhanced Key Usage: {0}", oid.FriendlyName + "(" + oid.Value + ")");
						}
					}
				}
			}
		}

		private void LogMessage(string message = "")
		{
			using (StreamWriter streamWriter = GetLogStreamWriter())
			{
				streamWriter.WriteLine(message);
			}
		}

		private StreamWriter GetLogStreamWriter()
		{
			StreamWriter streamWriter = new StreamWriter(m_LogStream, Encoding.UTF8, 1024, true);
			return streamWriter;
		}

		private void PutLogInTextbox()
		{
			// If logging to on-screen, grab the buffer and put it in the log text box
			if ((trackBarLogSetting.Value == 0) &&
				(m_LogStream != null))
			{
				m_LogStream.Position = 0;

				using (StreamReader logStreamReader = new StreamReader(
					m_LogStream,
					Encoding.UTF8,
					true,
					8192,
					true))
				{
					textBoxLog.Text = logStreamReader.ReadToEnd();
					textBoxLog.SelectionStart = textBoxLog.TextLength;
					textBoxLog.ScrollToCaret();
				}

				m_LogStream.Position = m_LogStream.Length;
			}
		}

		#endregion Private Methods

	}
}