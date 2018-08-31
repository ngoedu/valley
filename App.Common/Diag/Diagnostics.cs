/*
 * Created by SharpDevelop.
 * User: gbpc
 * Date: 2018/8/4
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace App.Common.Debug
{
	/// <summary>
	/// Description of Diagnostics.
	/// </summary>
	public class Diagnostics
	{
		static Diagnostics() {
			TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
			System.Diagnostics.Debug.Listeners.Add(tr1);
			TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText(CodeBase.GetCodePath()+@"\PadDebugDump.log"));
			System.Diagnostics.Debug.Listeners.Add(tr2);
		}
		
		public static void Debug(int level, string message) {
			System.Diagnostics.Debug.WriteLine(message);
			System.Diagnostics.Debug.Flush();
		}
		
		public static void Debug( string message) {
			Debug(1, message);
		}
	}
}
