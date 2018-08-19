/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/7/15
 * Time: 20:38
 * 
 * 
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace App.Common.Hook
{
	/// <summary>
	/// Description of HookKeyController.
	/// </summary>
	public class HookKeyController
	{
		private Dictionary<int, HotKeyEntry> REGISTORY = new Dictionary<int, HotKeyEntry>();
		
		private static readonly Lazy<HookKeyController> lazy =
	        new Lazy<HookKeyController>(() => new HookKeyController());
	    
	    public static HookKeyController Instance { get { return lazy.Value; } }
	
	    
		private HookKeyController()
		{
			REGISTORY.Add(1, new HotKeyEntry(1, Keys.F1, null));
			REGISTORY.Add(2, new HotKeyEntry(2, Keys.F2, null));
			REGISTORY.Add(3, new HotKeyEntry(3, Keys.F3, null));
			REGISTORY.Add(4, new HotKeyEntry(4, Keys.F4, null));
			/*REGISTORY.Add(5, new HotKeyEntry(5, Keys.F5, null));
			REGISTORY.Add(6, new HotKeyEntry(6, Keys.F6, null));
			REGISTORY.Add(7, new HotKeyEntry(7, Keys.F7, null));
			REGISTORY.Add(8, new HotKeyEntry(8, Keys.F8, null));
			REGISTORY.Add(9, new HotKeyEntry(9, Keys.F9, null));
			REGISTORY.Add(10, new HotKeyEntry(10, Keys.F10, null));*/
		}
		
		public bool IsHotKeyTriggered(int keyId) {
			foreach(var e in REGISTORY) {
				if (e.Key == keyId)
					return true;
			}
			return false;
		}
		
		public void DispatchHotKeyEvent(int keyId) {
			var entry = REGISTORY[keyId];
			if (entry != null && entry.callback != null) {
				entry.callback.OnHotKey(keyId);
			}			
		}
		
		public bool RegisterCallback(int keyId, IHookKeyCallback cb) {
			var entry = REGISTORY[keyId];
			if (entry != null) {
				entry.callback = cb;
				return true;
			}
			return false;
		}

		public void RegisterHotKey(IntPtr handle)
		{
			foreach(var entry in REGISTORY) {
				SystemHotKey.RegHotKey(handle, entry.Value.HotKeyID, SystemHotKey.KeyModifiers.None, entry.Value.key);     
			}    
		}
		
		public void UnRegisterHotKey(IntPtr handle)
		{
			foreach(var entry in REGISTORY) {
				SystemHotKey.UnRegHotKey(handle, entry.Value.HotKeyID);
			}    
		}
	}
	
	class HotKeyEntry {
		public int HotKeyID {set; get;}
		public Keys key {set; get; }
		public IHookKeyCallback callback {set; get;}
		
		public HotKeyEntry (int kid, Keys k, IHookKeyCallback cb) {
			this.HotKeyID = kid;
			this.key = k;
			this.callback = cb;
		}
	}
}
