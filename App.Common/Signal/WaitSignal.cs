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

namespace App.Common.Signal
{
	/// <summary>
	/// Description of WaitSignal.
	/// </summary>
	public class WaitSignal
	{
		readonly ManualResetEvent signalInternal = new ManualResetEvent(false);
		public Object AttechedObject {private set; get;}
		
		public WaitSignal()
		{
		}
		
		public void PushObject(Object obj) {
			this.AttechedObject = obj;
		}
		
		public void WaitOne() {
			this.signalInternal.WaitOne();
		}
		
		public void Set() {
			this.signalInternal.Set();
		}
		
		public void Reset() {
			this.signalInternal.Reset();
		}
		
		
	}
}
