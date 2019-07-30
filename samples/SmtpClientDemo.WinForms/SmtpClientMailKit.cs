#region Namespaces

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;

#endregion Namespaces

namespace SmtpClientDemo.WinForms
{
	public class SmtpClientMailKit : IDisposable, ISmtpClient
	{
		private readonly RemoteCertificateValidationCallback m_RemoteCertificateValidationCallback;

		#region Public Constructors

		public SmtpClientMailKit(RemoteCertificateValidationCallback remoteCertificateValidationCallback)
		{
			m_RemoteCertificateValidationCallback = remoteCertificateValidationCallback;
			m_CheckCertificateRevocation = true;

			m_Client = new SmtpClient
			{
				ServerCertificateValidationCallback = remoteCertificateValidationCallback,
				CheckCertificateRevocation =  m_CheckCertificateRevocation
			};
		}

		public void Dispose()
		{
			if (m_Client != null)
			{
				m_Client.Dispose();
				m_Client = null;
			}
		}

		#endregion Public Constructors

		#region Public Properties

		public string Authentication { get; set; }

		public HashSet<string> AuthenticationMechanisms
		{
			get
			{
				if (IsConnected)
				{
					return m_Client.AuthenticationMechanisms;
				}

				return new HashSet<string>();
			}
		}

		public SmtpCapabilities Capabilities
		{
			get
			{
				if (m_Client != null)
				{
					return m_Client.Capabilities;
				}

				return SmtpCapabilities.None;
			}
		}

		public bool CheckCertificateRevocation
		{
			get { return m_CheckCertificateRevocation; }
			set { m_CheckCertificateRevocation = value; }
		}

		public int BatchSize { get; set; }

		public bool IsConnected
		{
			get
			{
				return ((m_Client != null) && (m_Client.IsConnected));
			}
		}

		public ProtocolLogger Logger { get; set; }

		public uint MaxSize
		{
			get
			{
				if (m_Client != null)
				{
					return m_Client.MaxSize;
				}

				return 0;
			}
		}

		public string Password { get; set; }
		public int Port { get; set; }
		public SecureSocketOptions SecureSocketOption { get; set; }
		public string Server { get; set; }
		public string User { get; set; }

		#endregion Public Properties

		#region Public Methods

		public async Task Authenticate()
		{
			if (Authentication != NONE)
			{
				await Connect();

				if (m_Client.IsAuthenticated)
				{
					return;
				}

				var credentials = new NetworkCredential(User, Password);

				if (Authentication != AUTO)
				{
					// Manually create an SaslMechanism with the selected authentication mechanism. 
					// NTLM is not supported for auto select.
					var saslMechanism = SaslMechanism.Create(Authentication, new Uri("smtp://localhost"), credentials);

					// client.Authenticate(saslMechanism);
					await m_Client.AuthenticateAsync(saslMechanism);
				}
				else
				{
					// client.Authenticate(credentials);
					await m_Client.AuthenticateAsync(credentials);
				}
			}
		}

		public async Task Connect()
		{
			if (IsConnected)
			{
				return;
			}

			m_Client = (Logger == null) ? new SmtpClient() : new SmtpClient(Logger);
			m_Client.ServerCertificateValidationCallback = m_RemoteCertificateValidationCallback;
			m_Client.CheckCertificateRevocation = CheckCertificateRevocation;

			if (string.IsNullOrWhiteSpace(Server))
			{
				throw new ArgumentNullException("Server", "Parameter Server should be specified");
			}

			if ((Port < 1) ||
				(Port > 65535))
			{
				throw new ArgumentNullException("Port", "Parameter Port should be specified");
			}

			await m_Client.ConnectAsync(Server, Port, SecureSocketOption);

			m_MessagesInCurrentBatch = 0;
		}

		public void Disconnect()
		{
			if (m_Client != null)
			{
				m_Client.Disconnect(true);
				m_MessagesInCurrentBatch = 0;
			}
		}

		public bool HasCapability(SmtpCapabilities capability)
		{
			return Capabilities.HasFlag(capability);
		}

		public async Task<SendResult> TrySend(System.Net.Mail.MailMessage message)
		{
			await Connect();
			await Authenticate();

			SendResult result = new SendResult();

			try
			{
				MimeMessage mimeMessage = MimeMessage.CreateFromMailMessage(message);

				await m_Client.SendAsync(mimeMessage);
				result.Success = true;
				m_MessagesInCurrentBatch++;

				if ((BatchSize > 0) &&
					(m_MessagesInCurrentBatch >= BatchSize))
				{
					Disconnect();
				}
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

		private const string NONE = "None";

		#endregion Private Constants

		#region Private Fields

		private SmtpClient m_Client;
		private int m_MessagesInCurrentBatch;
		private bool m_CheckCertificateRevocation;

		#endregion Private Fields
	}
}