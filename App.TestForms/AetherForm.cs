/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/4/19
 * 时间: 23:54
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NGO.Protocol.AEther;

namespace AppTestForms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class AetherForm : Form, IEndpointCallback
	{
		Endpoint client;
		private delegate void _SafeSetTextCall(string text);
		public AetherForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.Text += " Endpoint ID="+Endpoint.ID;
			client = new Endpoint(this);
			
		}

		#region ICallback implementation

		public void Connected()
		{
			this.lbStatus.BackColor = Color.Green;
		}

		public void DataSent(string info)
		{
			if (this.InvokeRequired) {
                _SafeSetTextCall call = delegate(string s) {
					this.lbSend.Text = s; 
				};
                this.Invoke(call, info);
            }
            else
                this.lbSend.Text = info;
		}

		public void MessageReceived(string message)
		{
			if (this.InvokeRequired) {
                _SafeSetTextCall call = delegate(string s) {
					this.richTextBox1.AppendText("\r\n"+s); 
				};
                this.Invoke(call, message);
            }
            else
            	this.richTextBox1.AppendText("\r\n"+message);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			client.SendToRemote(this.textBox1.Text, Int16.Parse(this.textBox2.Text));
		}
		void Button2Click(object sender, EventArgs e)
		{
			client.Connect("192.168.0.9", 60001);
		}
		void Button3Click(object sender, EventArgs e)
		{
			bool isDisconnected = client.Disconnect();
			this.lbStatus.BackColor = Color.Gray;
		}

		#endregion
	}
}
