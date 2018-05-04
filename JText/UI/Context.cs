/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/27
 * 时间: 20:20
 * 
 * 
 */

using System.Windows.Forms;

namespace NGO.Pad.JText.UI
{
	/// <summary>
	/// this behaives as a mediator between fliker and keyboard callback.
	/// </summary>
	public class Context
	{
		private IFlicker flicker;
		private IKeyCallback keyCallback;
		private UserControl main;
		
		public Context() {
		}
		
		public Context(IFlicker f, IKeyCallback kc) {
			this.flicker = f;
			this.keyCallback = kc;
		}
		
		public void SetFlicker(IFlicker f) {
			this.flicker = f;
		}
		
		public void SetKeyCallback(IKeyCallback kc) {
			this.keyCallback = kc;
		}
		
		public IFlicker GetFlicker() {
			return this.flicker;
		}
		
		public IKeyCallback GetKeyCallback() {
			return this.keyCallback;
		}
		
		public void SetMainControl(UserControl ctl) {
			this.main = ctl;
		}
		
		public UserControl GetMainControl() {
			return this.main;
		}
	}
}
