#region Namespaces

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;

#endregion Namespaces

namespace SmtpClientDemo.WinForms
{
	public class SmtpClientSystemNetMail : ISmtpClient
	{
		#region Public Constructors

		static SmtpClientSystemNetMail()
		{
			ServicePointManager.SecurityProtocol =
				SecurityProtocolType.Ssl3 |
				SecurityProtocolType.Tls |
				SecurityProtocolType.Tls11 |
				SecurityProtocolType.Tls12;
		}

		public SmtpClientSystemNetMail()
		{
			m_Dirty = true;
		}

		private void CreateClient()
		{
			if (m_Client != null)
			{
				Disconnect();
			}

			m_Client = new System.Net.Mail.SmtpClient(Server, Port);
			m_Client.Timeout = DEFAULT_TIMEOUT;
			m_Client.DeliveryMethod = SmtpDeliveryMethod.Network;
			m_Client.EnableSsl = (SecureSocketOption != SecureSocketOptions.None) &&
								 (SecureSocketOption != SecureSocketOptions.Auto);

			if ((SecureSocketOption != SecureSocketOptions.None) &&
				(SecureSocketOption != SecureSocketOptions.Auto))
			{
				// To make SSL work we have to set this to "STARTTLS/<host>".
				// It was found on several forums like this 
				// https://stackoverflow.com/questions/20906077/gmail-error-the-smtp-server-requires-a-secure-connection-or-the-client-was-not
				// but I cannot find any official notes from Microsoft about this.
				m_Client.TargetName = "STARTTLS/" + Server;
			}

			if ((!string.IsNullOrEmpty(User)) &&
				(!string.IsNullOrEmpty(Password)))
			{
				m_Client.UseDefaultCredentials = false;
				m_Client.Credentials = new NetworkCredential(User, Password);
			}

			m_Dirty = false;
		}

		#endregion Public Constructors

		#region Public Properties

		public string Authentication { get; set; }

		public HashSet<string> AuthenticationMechanisms
		{
			get { return m_AuthenticationMechanisms; }
		}

		public int BatchSize
		{
			get { return 0; }
			set { }
		}

		public SmtpCapabilities Capabilities
		{
			get { return SmtpCapabilities.None; }
		}

		public bool IsConnected
		{
			get { return true; }
		}

		public ProtocolLogger Logger { get; set; }

		public uint MaxSize { get; }

		public string Password
		{
			get { return m_Password; }
			set
			{
				if (m_Password != value)
				{
					m_Password = value;
					m_Dirty = true;
				}
			}
		}

		public int Port
		{
			get { return m_Port; }
			set
			{
				if (m_Port != value)
				{
					m_Port = value;
					m_Dirty = true;
				}
			}
		}

		public SecureSocketOptions SecureSocketOption
		{
			get { return m_SecureSocketOption; }
			set
			{
				if (m_SecureSocketOption != value)
				{
					m_SecureSocketOption = value;
					m_Dirty = true;
				}
			}
		}

		public string Server
		{
			get { return m_Server; }
			set
			{
				if (m_Server != value)
				{
					m_Server = value;
					m_Dirty = true;
				}
			}
		}

		public string User
		{
			get { return m_User; }
			set
			{
				if (m_User != value)
				{
					m_User = value;
					m_Dirty = true;
				}
			}
		}

		#endregion Public Properties

		#region Public Methods

		public Task Authenticate()
		{
			return Task.FromResult(0);
		}

		public Task Connect()
		{
			if (m_Dirty)
			{
				CreateClient();
			}

			return Task.FromResult(0);
		}

		public void Disconnect()
		{
		}

		public bool HasCapability(SmtpCapabilities capability)
		{
			// The System.Net.Mail.SmtpClient doesn't know what capabilities are available, so just say yes
			return true;
		}

		public async Task<SendResult> TrySend(System.Net.Mail.MailMessage message)
		{
			SendResult result = new SendResult();

			await Connect();

			try
			{
				await Task.Run(() => m_Client.Send(message));
				result.Success = true;
			}
			catch (Exception exception)
			{
				result.Exception = exception;
				result.Success = false;
			}

			return result;
		}

		#endregion Public Methods

		#region Private Constants

		private const string AUTO = "Auto";

		private const int DEFAULT_TIMEOUT = 30000;
		private const string NONE = "None";

		// 5 seconds

		#endregion Private Constants

		#region Private Fields

		private static readonly HashSet<string> m_AuthenticationMechanisms = new HashSet<string>
		{
			NONE,
			AUTO,
			"SCRAM-SHA-256",
			"SCRAM-SHA-1",
			"CRAM-MD5",
			"DIGEST-MD5",
			"PLAIN",
			"LOGIN",
			"NTLM"
		};

		private System.Net.Mail.SmtpClient m_Client;
		private string m_Server;
		private int m_Port;
		private string m_Password;
		private string m_User;

		private bool m_Dirty;
		private SecureSocketOptions m_SecureSocketOption;

		#endregion Private Fields

	}
}
