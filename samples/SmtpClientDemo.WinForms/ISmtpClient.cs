#region Namespaces

using System.Collections.Generic;
using System.Net.Security;
using System.Threading.Tasks;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;

#endregion Namespaces

namespace SmtpClientDemo.WinForms
{
	public interface ISmtpClient
	{
		#region Public Properties

		string Authentication { get; set; }

		HashSet<string> AuthenticationMechanisms { get; }

		int BatchSize { get; set; }

		SmtpCapabilities Capabilities { get; }

		bool IsConnected { get; }

		ProtocolLogger Logger { get; set; }

		uint MaxSize { get; }

		string Password { get; set; }

		int Port { get; set; }

		SecureSocketOptions SecureSocketOption { get; set; }

		string Server { get; set; }

		string User { get; set; }

		#endregion Public Properties

		#region Public Methods

		Task Authenticate();

		Task Connect();

		void Disconnect();

		bool HasCapability(SmtpCapabilities capability);

		Task<SendResult> TrySend(System.Net.Mail.MailMessage message);

		#endregion Public Methods
	}
}