/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 20:38
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace App.Common.Hook
{
	/// <summary>
	/// Description of HookKeyController.
	/// </summary>
	public class HookKeyController
	{
		private Dictionary<string, IHookKeyCallback> REGISTORY;
		private KeyboardHook HOOKS = new KeyboardHook();
		
		private static readonly Lazy<HookKeyController> lazy =
	        new Lazy<HookKeyController>(() => new HookKeyController());
	    
	    public static HookKeyController Instance { get { return lazy.Value; } }
	
	    
		private HookKeyController()
		{
			REGISTORY = new Dictionary<string, IHookKeyCallback>();
		}
		
		public void Register(Keys key, IHookKeyCallback callback) {
			HOOKS.HookedKeys.Add(key);
			HOOKS.KeyDown += new KeyEventHandler(gkh_KeyDown);
			HOOKS.KeyUp += new KeyEventHandler(gkh_KeyUp);
			REGISTORY.Add(key.ToString(), callback);
		}
		
		void gkh_KeyUp(object sender, KeyEventArgs e) {
			e.Handled = true;
		}

		void gkh_KeyDown(object sender, KeyEventArgs e) {
			System.Diagnostics.Debug.WriteLine("Hook Key :"+e.KeyCode.ToString());
			IHookKeyCallback cb = REGISTORY[e.KeyCode.ToString()];
			if (cb!=null)
				cb.OnKey(e.KeyValue);
			e.Handled = true;
		}
	}
}
