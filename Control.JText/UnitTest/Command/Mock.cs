/*
 * 由SharpDevelop创建。
 * 用户： Bob (XuYong Hou) houxuyong@hotmail.com
 * 日期: 2018/3/28
 * 时间: 12:54
 * 
 * 
 */
using NGO.Pad.JText.Controller;
using NGO.Pad.JText.UI;

namespace NGO.Pad.JText.UnitTest.Command
{
	/// <summary>
	///  Mock TextController.
	/// </summary>
	public class Mock
	{
		public static TextController MockTextController() {
			Context context = new Context();
			context.SetFlicker(new MockFlicker());
			var tc = new TextController(context);
			//a defaut line feed will be appened in any new document
			return tc;
		}
	}
	
	public class MockFlicker : IFlicker {
		public void SetLocation(int x, int y)
		{
			throw new System.NotImplementedException();
		}
		public void SetBackgound(System.Drawing.Color color)
		{
			throw new System.NotImplementedException();
		}
	}
}
