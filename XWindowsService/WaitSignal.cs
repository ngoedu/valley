/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/4
 * Time: 10:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace XWindowsService
{
	/// <summary>
	/// Description of WaitSignal.
	/// </summary>
	public class WaitSignal
	{
		readonly ManualResetEvent signalInternal = new ManualResetEvent(false);
		public Object AttechedObject {private set; get;}
		public bool IsErrorOccured{private set; get;}
		
		private string errorString;
		private string matchingString;
		private bool isWaiting = false;
		
		
		public WaitSignal()
		{
			IsErrorOccured = false;
		}
		
		
		public void PushObject(Object obj) {
			this.AttechedObject = obj;
		}
		
		public void WaitOne() {
			if (isWaiting)
				throw new InvalidProgramException("signal is in invalid status.");
			this.isWaiting = true;
			this.signalInternal.WaitOne();
		}
		
		public WaitSignal WaitOneWhen(string mstr, string error) {
			this.matchingString = mstr;
			this.errorString = error;
			WaitOne();
			return this;
		}
		
		public void Set() {
			this.signalInternal.Set();
			this.isWaiting = false;
		}
		
		public void SetWhen(Object str) {
			if (str == null || !this.isWaiting)
				return;
			if (str.ToString().Contains(this.matchingString)) {
		    	Set();
			} else if (str.ToString().Contains(this.errorString)) {
				Set();
				this.AttechedObject = str.ToString();
				IsErrorOccured = true;
			}
		}
		
		public void Reset() {
			this.signalInternal.Reset();
		}
		
		
	}
}
