#region Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MimeKit;
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

		#region Private Constants

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
		private const string SECURE_SOCKET_OPTION_START_TLS = "Start TLS";
		private const string SECURE_SOCKET_OPTION_START_TLS_WHEN_AVAILABLE = "Start TLS when available";

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

		private SmtpClientMailKit m_Client;

		#endregion Private Fields

		#region Private Methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			m_Client = new SmtpClientMailKit();

			LoadSslComboBoxValues();
			LoadFormDefaultValuesFromConfig();
		}

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

			string ssl = ConfigurationManager.AppSettings[APP_SETTINGS_SSL];

			if (!string.IsNullOrEmpty(ssl))
			{
				SecureSocketOptions valueToSelect = SecureSocketOptions.Auto;

				foreach (KeyValuePair<string, SecureSocketOptions> item in comboBoxSSL.Items)
				{
					if (item.Key == ssl)
					{
						comboBoxSSL.SelectedValue = valueToSelect;
						break;
					}
				}

				m_SecureSocketOptions = (SecureSocketOptions)comboBoxSSL.SelectedValue;
			}

			m_User = ConfigurationManager.AppSettings[APP_SETTINGS_USER];
			textBoxUser.Text = m_User;

			m_Password = ConfigurationManager.AppSettings[APP_SETTINGS_PASSWORD];
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
				if (m_Client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
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
				MimeMessage message = CreateMessage();

				var sendEmailTask = m_Client.TrySend(message);
				await sendEmailTask;
				m_Client.EndSend();

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
			finally
			{
				UpdateFormState(true);
			}
		}

		//private async Task<SendResult> TrySendEmail(
		//	MimeMessage message,
		//	SmtpClient client)
		//{
		//	SendResult result = new SendResult();

		//	try
		//	{
		//		await client.SendAsync(message);
		//		result.Success = true;

		//	}
		//	catch (Exception exception)
		//	{
		//		result.Exception = exception;
		//		result.Success = false;
		//	}

		//	//finally
		//	//{
		//	//	logStream.Position = 0;

		//	//	using (StreamReader logStreamReader = new StreamReader(logStream, Encoding.UTF8, true, 8192, true))
		//	//	{
		//	//		textBoxLog.Text = logStreamReader.ReadToEnd();
		//	//	}
		//	//}

		//	return result;
		//}

		//private async Task<SendResult> TrySendEmail(
		//	MimeMessage message)
		//{
		//	SendResult result = new SendResult();
		//	SmtpClient client = null;

		//	// MemoryStream disposed by ProtocolLogger
		//	MemoryStream logStream = new MemoryStream();

		//	using (ProtocolLogger logger = new ProtocolLogger(logStream))
		//	{
		//		try
		//		{
		//			client = await ConnectToSmtpServer(GetServer(), GetPortNumber(), GetSecureSocketOption(), logger);

		//			await AuthenticateToSmtpServer(client, GetAuthenticationMethod(), GetUserName(), GetPassword());

		//			await client.SendAsync(message);
		//			result.Success = true;
		//		}
		//		catch (Exception ex)
		//		{
		//			result.Exception = ex;
		//			result.Success = false;
		//		}
		//		finally
		//		{
		//			logStream.Position = 0;

		//			using (StreamReader logStreamReader = new StreamReader(logStream, Encoding.UTF8, true, 8192, true))
		//			{
		//				textBoxLog.Text = logStreamReader.ReadToEnd();
		//			}

		//			if (client != null)
		//			{
		//				if (client.IsConnected)
		//				{
		//					client.Disconnect(true);
		//				}

		//				client.Dispose();
		//			}
		//		}
		//	}

		//	return result;
		//}

		//private async Task AuthenticateToSmtpServer(SmtpClient client,
		//	string authenticationMethod,
		//	string userName,
		//	string password)
		//{
		//	if (authenticationMethod != NONE)
		//	{
		//		var credentials = new NetworkCredential(userName, password);

		//		if (authenticationMethod != AUTO)
		//		{
		//			// Manually create an SaslMechanism with the selected authentication mechanism. 
		//			// NTLM is not supported for auto select.
		//			var saslMechanism = SaslMechanism.Create(authenticationMethod, new Uri("smtp://localhost"), credentials);

		//			// client.Authenticate(saslMechanism);
		//			await client.AuthenticateAsync(saslMechanism);
		//		}
		//		else
		//		{
		//			// client.Authenticate(credentials);
		//			await client.AuthenticateAsync(credentials);
		//		}
		//	}
		//}

		private void ButtonCloseOnClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		//private async Task<SmtpClient> ConnectToSmtpServer(
		//	string server,
		//	int portNumber,
		//	SecureSocketOptions secureSocketOption,
		//	ProtocolLogger logger = null)
		//{
		//	SmtpClient client = logger == null ? new SmtpClient() : new SmtpClient(logger);

		//	if (string.IsNullOrWhiteSpace(server))
		//	{
		//		throw new ArgumentNullException("Server", "Parameter Server should be specified");
		//	}

		//	if (string.IsNullOrWhiteSpace(textBoxPort.Text))
		//	{
		//		throw new ArgumentNullException("Port", "Parameter Port should be specified");
		//	}

		//	await client.ConnectAsync(server, portNumber, secureSocketOption);

		//	return client;
		//}

		private string GetAuthenticationMethod()
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(GetAuthenticationMethod));
			}

			return (string)comboBoxAuth.SelectedValue;
		}

		private string GetPassword()
		{
			return GetInputField(textBoxPassword);
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

		private string GetServer()
		{
			return GetInputField(textBoxServer);
		}

		private string GetUserName()
		{
			return GetInputField(textBoxUser);
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
			m_Server = GetServer();
			m_Port = GetPortNumber();
			m_SecureSocketOptions = GetSecureSocketOption();

			m_User = GetUserName();
			m_Password = GetPassword();
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

		private MimeMessage CreateMessage(string messageSuffix = "")
		{
			MimeMessage message = new MimeMessage();

			if (string.IsNullOrWhiteSpace(m_From))
			{
				throw new ArgumentNullException("From", "Parameter From should be specified");
			}

			message.From.Add(new MailboxAddress(m_From));

			if (string.IsNullOrWhiteSpace(m_To))
			{
				throw new ArgumentNullException("To", "Parameter To should be specified");
			}

			message.To.Add(new MailboxAddress(m_To));

			if (!string.IsNullOrWhiteSpace(m_Cc))
			{
				message.Cc.Add(new MailboxAddress(m_Cc));
			}

			if (!string.IsNullOrWhiteSpace(m_Bcc))
			{
				message.Bcc.Add(new MailboxAddress(m_Bcc));
			}

			message.Subject = m_Subject;

			message.Body = new TextPart("plain")
			{
				Text = m_MessageBody + messageSuffix
			};

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
			m_RepeatStatus.StopClicked += StatusForm_StopClicked;
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

		private void StatusForm_StopClicked(object sender, EventArgs e)
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
					MimeMessage message = CreateMessage(" #" + (countTotal + 1));

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
				m_Client.EndSend();
			}

			return true;
		}

		private void Sleep(int sleepSeconds)
		{
			for (int count = 0; count < sleepSeconds; count++)
			{
				Thread.Sleep(1000);
				if (m_Cancel)
				{
					break;
				}
			}
		}

		#endregion Private Methods
	}
}