namespace SmtpClientDemo.WinForms
{
	public class SmtpMessage
	{
		public SmtpMessage(
			string @from,
			string to,
			string subject,
			string messageBody,
			string cc = "",
			string bcc = "")
		{
			From = @from;
			To = to;
			Cc = cc;
			Bcc = bcc;
			Subject = subject;
			MessageBody = messageBody;
		}

		public string From { get; }
		public string To { get; }
		public string Cc { get; }
		public string Bcc { get; }
		public string Subject { get; }
		public string MessageBody { get; }
	}
}