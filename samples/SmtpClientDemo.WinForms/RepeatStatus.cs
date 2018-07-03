using System;
using System.Windows.Forms;

namespace SmtpClientDemo.WinForms
{
	public partial class RepeatStatus : Form
	{
		public event EventHandler<EventArgs> StopClicked
		{
			add
			{
				lock (m_EventLock)
				{
					m_StopClickedEvent += value;
				}
			}
			remove
			{
				lock (m_EventLock)
				{
					m_StopClickedEvent -= value;
				}
			}
		}

		private event EventHandler<EventArgs> m_StopClickedEvent;

		private readonly object m_EventLock = new object();

		/// <summary>
		/// Raises the StopClicked event synchronously.
		/// </summary>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void RaiseStopClicked(EventArgs args)
		{
			m_StopClickedEvent?.Invoke(this, args);
		}


		public RepeatStatus()
		{
			InitializeComponent();

			m_MessageCount = 0;
		}

		public void MessageSent()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(MessageSent));
				return;
			}

			m_MessageCount++;
			labelMessageCount.Text = m_MessageCount.ToString();
		}

		private int m_MessageCount;

		private void buttonStopOnClick(object sender, EventArgs e)
		{
			RaiseStopClicked(new EventArgs());
			Close();
		}
	}
}
