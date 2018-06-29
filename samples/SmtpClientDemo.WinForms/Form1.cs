#region Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
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
			}

			List<string> authComboBoxData = new List<string> { NONE };

			if ((client != null) &&
				(client.IsConnected))
			{
				if (client.AuthenticationMechanisms.Count > 0)
				{
					authComboBoxData.Add(AUTO);
				}

				authComboBoxData.AddRange(client.AuthenticationMechanisms);
			}

			comboBoxAuth.DataSource = new BindingSource(authComboBoxData, null);
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

				//Invoke(new Action(() =>
				//{
				//	if (buttonSend.Focused)
				//	{
				//		buttonSend.PerformClick();
				//	}
				//	else if (buttonCopy.Focused)
				//	{
				//		buttonCopy.PerformClick();
				//	}
				//}));
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

				//Invoke(new Action(() =>
				//{
				//	if (buttonSend.Focused)
				//	{
				//		buttonSend.PerformClick();
				//	}
				//	else if (buttonCopy.Focused)
				//	{
				//		buttonCopy.PerformClick();
				//	}
				//}));
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

		private async void ButtonSendOnClick(object sender, EventArgs e)
		{
			Exception exception = null;
			SmtpClient client = null;

			using (MemoryStream logStream = new MemoryStream())
			{
				using (ProtocolLogger logger = new ProtocolLogger(logStream))
				{
					try
					{
						UpdateFormState(false);

						client = await ConnectToSmtpServer(logger);

						string selectedAuth = (string)comboBoxAuth.SelectedValue;

						if (selectedAuth != NONE)
						{
							if (selectedAuth != AUTO)
							{
								client.AuthenticationMechanisms.RemoveWhere(m => m != selectedAuth);
							}

							client.Authenticate(textBoxUser.Text, textBoxPassword.Text);
						}

						MimeMessage message = CreateMessage();
						client.Send(message);
					}
					catch (Exception ex)
					{
						exception = ex;
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

						UpdateFormState(true);

						if (exception == null)
						{
							MessageBox.Show("Email has been sent successfully", "Sent successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						else
						{
							MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void BtnCloseOnClick(object sender, EventArgs e)
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

			SecureSocketOptions secureSocketOptions = (SecureSocketOptions) comboBoxSSL.SelectedValue;
			await client.ConnectAsync(textBoxServer.Text, portNumber, secureSocketOptions);

			return client;
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
			}

			Enabled = enabled;
			progressBar1.Style = enabled ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
			progressBar1.MarqueeAnimationSpeed = enabled ? 0 : 10;
		}

		private async Task UpdateAuthComboBoxValues()
		{
			Task<SmtpClient> task = null;

			try
			{
				UpdateFormState(false);

				task = ConnectToSmtpServer();
				await task.ContinueWith(t => LoadAuthComboBoxValues(t.Result));
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
					client = task.Result;
				}
				catch
				{
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

		#endregion Private Methods
	}
}