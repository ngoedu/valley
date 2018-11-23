/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/11/23
 * Time: 18:27
 * 
 * 
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Control.JBrowser.Browser
{
	/// <summary>
	/// Description of JNav2WebBrowser.
	/// </summary>
	public partial class JNav2WebBrowser : UserControl
	{
		private readonly ChromiumWebBrowser browser;
		
		public JNav2WebBrowser()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			browser = new ChromiumWebBrowser("about:blank")
            {
                Dock = DockStyle.Fill,
            };
            panelBrowser.Controls.Add(browser);
            
             browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
		}

		void JNav2WebBrowserSizeChanged(object sender, EventArgs e)
		{
			this.goBackButton.Left = 2;
			this.goBackButton.Top = 2;
			
			this.forwardButton.Left = this.goBackButton.Left + this.goBackButton.Width + 2;
			this.forwardButton.Top = 2;
			
			this.urlTextBox.Left = this.forwardButton.Left + this.forwardButton.Width;
			this.urlTextBox.Top = 1;
			this.urlTextBox.Width = this.Width - 100;
			
			this.goBotton.Left = this.urlTextBox.Left+this.urlTextBox.Width + 2;
			this.goBotton.Top = 2;
			
			this.statusLabel.Top = this.Height - statusLabel.Height;
			this.statusLabel.Left = 0;
			this.statusLabel.Width = this.Width;
			
			this.panelBrowser.Top = this.urlTextBox.Top + this.urlTextBox.Height + 1;
			this.panelBrowser.Left = 0;
			this.panelBrowser.Width = this.Width;
			this.panelBrowser.Height = this.Height - urlTextBox.Height - statusLabel.Height - 2;
			this.panelBrowser.BackColor = Color.Red;
			
			this.goBotton.Click += new System.EventHandler(this.GoButtonClick);
            this.urlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UrlTextBoxKeyUp);
            this.forwardButton.Click += new System.EventHandler(this.ForwardButtonClick);
            this.goBackButton.Click += new System.EventHandler(this.BackButtonClick);
            
            this.goBackButton.EnabledChanged += new System.EventHandler(this.GoBackButtonEnabledChanged);
			this.forwardButton.EnabledChanged += new System.EventHandler(this.GoForwardButtonEnabledChanged);
			
            
		}
		
		void GoBackButtonEnabledChanged(object sender, EventArgs e)
		{
			if (this.goBackButton.Enabled)
				this.goBackButton.Image = global::Control.JBrowser.Resource1.nav_left_green;
			else
				this.goBackButton.Image = global::Control.JBrowser.Resource1.nav_left_green_disable;	
		}
		
		void GoForwardButtonEnabledChanged(object sender, EventArgs e)
		{
			if (this.forwardButton.Enabled)
				this.forwardButton.Image = global::Control.JBrowser.Resource1.nav_right_green;
			else
				this.forwardButton.Image = global::Control.JBrowser.Resource1.nav_right_green_disable;
		}
		
		private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }
		
		private void GoButtonClick(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            browser.Forward();
        }
        
		
		private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            //DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }
        
        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => this.goBackButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
			//goButton.Text = isLoading ? "停止" : "Go";
           	this.goBotton.Image = isLoading ?  global::Control.JBrowser.Resource1.nav_plain_red :   global::Control.JBrowser.Resource1.nav_plain_green;
        }
        
        public void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }
        
        public void RefreshPage(string pageUrl) {
        	if (!string.IsNullOrEmpty(pageUrl))
        		LoadUrl(pageUrl);
			else
				LoadUrl(urlTextBox.Text);
        }
        
        public void LoadHtml(string content) {
        	browser.LoadHtml(content, "about:blank");
        }
        
        public string ShowDevTools(Panel panel) {
			/**/var windowInfo = new WindowInfo();
			var brow = browser.GetBrowser().GetHost();
			var rect = panel.ClientRectangle;
			windowInfo.SetAsChild(panel.Handle,rect.Left, rect.Top, rect.Right, rect.Bottom);
			brow.ShowDevTools(windowInfo);
			
			//cefBrowser.ShowDevTools();
			return "chrome-devtools://devtools/inspector.html";
		}
	}
}
