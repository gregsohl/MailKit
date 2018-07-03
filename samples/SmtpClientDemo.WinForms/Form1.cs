#region Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

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
		private string m_Port;

		private bool m_Cancel;
		private RepeatStatus m_RepeatStatus;

		#endregion Private Fields

		#region Private Methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			LoadSslComboBoxValues();
			LoadFormDefaultValuesFromConfig();
		}

		private async void LoadFormDefaultValuesFromConfig()
		{
			string from = ConfigurationManager.AppSettings[APP_SETTINGS_FROM];
			textBoxFrom.Text = from;

			string to = ConfigurationManager.AppSettings[APP_SETTINGS_TO];
			textBoxTo.Text = to;

			string cc = ConfigurationManager.AppSettings[APP_SETTINGS_CC];
			textBoxCC.Text = cc;

			string bcc = ConfigurationManager.AppSettings[APP_SETTINGS_BCC];
			textBoxBCC.Text = bcc;

			string subject = ConfigurationManager.AppSettings[APP_SETTINGS_SUBJECT];
			textBoxSubject.Text = subject;

			string message = ConfigurationManager.AppSettings[APP_SETTINGS_MESSAGE];
			textBoxMessage.Text = message;

			string server = ConfigurationManager.AppSettings[APP_SETTINGS_SERVER];
			textBoxServer.Text = server;
			m_Server = server;

			string port = ConfigurationManager.AppSettings[APP_SETTINGS_PORT];
			textBoxPort.Text = port;
			m_Port = port;

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
			}

			string user = ConfigurationManager.AppSettings[APP_SETTINGS_USER];
			textBoxUser.Text = user;

			string password = ConfigurationManager.AppSettings[APP_SETTINGS_PASSWORD];
			textBoxPassword.Text = password;

			await UpdateAuthComboBoxValues();
			string auth = ConfigurationManager.AppSettings[APP_SETTINGS_AUTH];
			
			comboBoxAuth.SelectedItem = auth;
			UpdateCredentialsVisibility();

			comboBoxSSL.SelectedValueChanged += ComboBoxSslOnSelectedValueChanged;
			comboBoxAuth.SelectedValueChanged += ComboBoxAuthOnSelectedValueChanged;
		}

		private void LoadAuthComboBoxValues(SmtpClient client = null)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					LoadAuthComboBoxValues(client);
				}));

				return;
			}

			List<string> authComboBoxData = new List<string> { NONE };

			int initialIndex = 0;

			if ((client != null) &&
				(client.IsConnected))
			{
				if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
				{
					if (client.AuthenticationMechanisms.Count > 0)
					{
						authComboBoxData.Add(AUTO);
						initialIndex = 1;
					}

					// Limit the list to the intersection of those supported by both the server and MailKit library
					foreach (string serverSupportedAuthenticationMechanism in client.AuthenticationMechanisms)
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
			Invoke(new Action(() =>
			{
				if (buttonClose.Focused)
				{
					buttonClose.PerformClick();
				}
			}));

			if (m_Server != textBoxServer.Text)
			{
				m_Server = textBoxServer.Text;
				await UpdateAuthComboBoxValues();
			}
		}

		private async void TextBoxPortOnLeave(object sender, EventArgs e)
		{
			Invoke(new Action(() =>
			{
				if (buttonClose.Focused)
				{
					buttonClose.PerformClick();
				}
			}));

			if (m_Port != textBoxPort.Text)
			{
				m_Port = textBoxPort.Text;
				await UpdateAuthComboBoxValues();
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
			bool show = (string)comboBoxAuth.SelectedItem != NONE;

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

			try
			{
				MimeMessage message = CreateMessage();

				var sendEmailTask = TrySendEmail(message);
				await sendEmailTask;

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

		private async Task<SendResult> TrySendEmail(
			MimeMessage message,
			SmtpClient client)
		{
			SendResult result = new SendResult();

			try
			{
				client.Send(message);
				result.Success = true;

			}
			catch (Exception exception)
			{
				result.Exception = exception;
				result.Success = false;
			}

			//finally
			//{
			//	logStream.Position = 0;

			//	using (StreamReader logStreamReader = new StreamReader(logStream, Encoding.UTF8, true, 8192, true))
			//	{
			//		textBoxLog.Text = logStreamReader.ReadToEnd();
			//	}
			//}

			return result;
		}

		private async Task<SendResult> TrySendEmail(
			MimeMessage message)
		{
			SendResult result = new SendResult();
			SmtpClient client = null;

			// MemoryStream disposed by ProtocolLogger
			MemoryStream logStream = new MemoryStream();

			using (ProtocolLogger logger = new ProtocolLogger(logStream))
			{
				try
				{
					client = await ConnectToSmtpServer(logger);

					await AuthenticateToSmtpServer(client);

					client.Send(message);
					result.Success = true;
				}
				catch (Exception ex)
				{
					result.Exception = ex;
					result.Success = false;
				}
				finally
				{
					logStream.Position = 0;

					using (StreamReader logStreamReader = new StreamReader(logStream, Encoding.UTF8, true, 8192, true))
					{
						textBoxLog.Text = logStreamReader.ReadToEnd();
					}

					if (client != null)
					{
						if (client.IsConnected)
						{
							client.Disconnect(true);
						}

						client.Dispose();
					}
				}
			}

			return result;
		}

		private async Task AuthenticateToSmtpServer(SmtpClient client)
		{
			string selectedAuth = GetAuthenticationMethod();

			if (selectedAuth != NONE)
			{
				string userName = GetUserName();
				string password = GetPassword();

				var credentials = new NetworkCredential(userName, password);

				if (selectedAuth != AUTO)
				{
					// Manually create an SaslMechanism with the selected authentication mechanism. 
					// NTLM is not supported for auto select.
					var saslMechanism = SaslMechanism.Create(selectedAuth, new Uri("smtp://localhost"), credentials);

					// client.Authenticate(saslMechanism);
					await client.AuthenticateAsync(saslMechanism);
				}
				else
				{
					// client.Authenticate(credentials);
					await client.AuthenticateAsync(credentials);
				}
			}
		}

		private void ButtonCloseOnClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private async Task<SmtpClient> ConnectToSmtpServer(ProtocolLogger logger = null)
		{
			SmtpClient client = logger == null ? new SmtpClient() : new SmtpClient(logger);

			if (string.IsNullOrWhiteSpace(textBoxServer.Text))
			{
				throw new ArgumentNullException("Server", "Parameter Server should be specified");
			}

			if (string.IsNullOrWhiteSpace(textBoxPort.Text))
			{
				throw new ArgumentNullException("Port", "Parameter Port should be specified");
			}

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

			var secureSocketOptions = GetSecureSocketOption();
			await client.ConnectAsync(textBoxServer.Text, portNumber, secureSocketOptions);

			return client;
		}

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
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(GetPassword));
			}

			return textBoxPassword.Text;
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

		private string GetUserName()
		{
			if (InvokeRequired)
			{
				return (string)Invoke(new Func<string>(GetUserName));
			}

			return textBoxUser.Text;
		}

		private MimeMessage CreateMessage()
		{
			MimeMessage message = new MimeMessage();

			if (string.IsNullOrWhiteSpace(textBoxFrom.Text))
			{
				throw new ArgumentNullException("From", "Parameter From should be specified");
			}

			message.From.Add(new MailboxAddress(textBoxFrom.Text));

			if (string.IsNullOrWhiteSpace(textBoxTo.Text))
			{
				throw new ArgumentNullException("To", "Parameter To should be specified");
			}

			message.To.Add(new MailboxAddress(textBoxTo.Text));

			if (!string.IsNullOrWhiteSpace(textBoxCC.Text))
			{
				message.Cc.Add(new MailboxAddress(textBoxCC.Text));
			}

			if (!string.IsNullOrWhiteSpace(textBoxBCC.Text))
			{
				message.Bcc.Add(new MailboxAddress(textBoxBCC.Text));
			}

			message.Subject = textBoxSubject.Text;

			message.Body = new TextPart("plain")
			{
				Text = textBoxMessage.Text
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
			Task<SmtpClient> createClientTask = null;

			UpdateFormState(false);

			try
			{
				if ((!string.IsNullOrWhiteSpace(textBoxServer.Text)) &&
					(!string.IsNullOrWhiteSpace(textBoxPort.Text)))
				{
					createClientTask = ConnectToSmtpServer();
					await createClientTask.ContinueWith(t => LoadAuthComboBoxValues(t.Result));
				}
			}
			catch
			{
				LoadAuthComboBoxValues();
			}
			finally
			{
				SmtpClient client = null;

				try
				{
					client = createClientTask.Result;
				}
				catch
				{
					// ignored
				}

				if (client != null)
				{
					if (client.IsConnected)
					{
						client.Disconnect(true);
					}

					client.Dispose();
				}

				UpdateFormState(true);
			}
		}

		private async void ButtonListServerCapabilitiesOnClick(object sender, EventArgs e)
		{
			UpdateFormState(false);

			try
			{
				SmtpClient client = await ConnectToSmtpServer();

				using (client)
				{
					string capabilities = ListCapabilities(client);

					textBoxLog.Text += capabilities;
				}
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

		private string ListCapabilities(SmtpClient client)
		{
			StringBuilder capabilities = new StringBuilder();

			if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
			{
				var mechanisms = string.Join(", ", client.AuthenticationMechanisms);
				capabilities.AppendLine(string.Format("SASL mechanisms: {0}", mechanisms));
			}

			if (client.Capabilities.HasFlag(SmtpCapabilities.Size))
			{
				capabilities.AppendLine(string.Format("Message size restriction: {0}.", client.MaxSize));
			}

			if (client.Capabilities.HasFlag(SmtpCapabilities.Dsn))
			{
				capabilities.AppendLine("Supports delivery-status notifications.");
			}

			if (client.Capabilities.HasFlag(SmtpCapabilities.EightBitMime))
			{
				capabilities.AppendLine("Supports Content-Transfer-Encoding: 8bit");
			}

			if (client.Capabilities.HasFlag(SmtpCapabilities.BinaryMime))
			{
				capabilities.AppendLine("Supports Content-Transfer-Encoding: binary");
			}

			if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
			{
				capabilities.AppendLine("Supports UTF-8 in message headers.");
			}

			return capabilities.ToString();
		}

		private async void ButtonRepeatedSendOnClick(object sender, EventArgs e)
		{
			UpdateFormState(false);

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
			SmtpClient client = null;

			int countOnConnection = 0;
			int countTotal = 0;

			try
			{
				client = await ConnectToSmtpServer();
				await AuthenticateToSmtpServer(client);

				MimeMessage message = CreateMessage();

				m_Cancel = false;

				while (!m_Cancel)
				{
					SendResult sendResult = await TrySendEmail(message, client);

					if (!sendResult.Success)
					{
						break;
					}

					countOnConnection++;
					countTotal++;

					m_RepeatStatus.MessageSent();

					if (countOnConnection >= 5)
					{
						client.Dispose();
						client = null;
						countOnConnection = 0;
					}

					Thread.Sleep(1000 * Convert.ToInt32(txtRepeatSeconds.Value));

					if ((client == null) ||
						(!client.IsConnected))
					{
						if (client != null)
						{
							client.Dispose();
						}

						client = await ConnectToSmtpServer();
						countOnConnection = 0;
					}
				}
			}
			finally
			{
				if (client != null)
				{
					client.Dispose();
				}
			}

			return true;
		}

		#endregion Private Methods

		private class SendResult
		{
			public bool Success { get; set; }
			public Exception Exception { get; set; }
		}
	}
}