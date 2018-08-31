/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/8/29
 * Time: 22:17
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace App.Common.Win32
{
	/// <summary>
	/// Description of Win32Api.
	/// </summary>
	public class Win32Api
	{
		#region form dll
		public const int SW_HIDE =              0;
		public const int SW_SHOWNORMAL   =    1;
		public const int SW_NORMAL         =  1;
		public const int SW_SHOWMINIMIZED  =  2;
		public const int SW_SHOWMAXIMIZED  =  3;
		public const int SW_MAXIMIZE       =  3;
		public const int SW_SHOWNOACTIVATE =  4;
		public const int SW_SHOW            = 5;
		public const int SW_MINIMIZE       =  6;
		public const int SW_SHOWMINNOACTIVE = 7;
		public const int SW_SHOWNA         = 8;
		public const int SW_RESTORE        =  9;
		public const int SW_SHOWDEFAULT    =  10;
		public const int SW_FORCEMINIMIZE   = 11;
		public const int SW_MAX            =  11;
			
		public const short SWP_NOMOVE = 0X2;
		public const short SWP_NOSIZE = 1;
		public const short SWP_NOZORDER = 0X4;
		public const int SWP_SHOWWINDOW = 0x0040;
		public const int SWP_FRAMECHANGED = 0x0020;
		public const int SWP_NOOWNERZORDER = 0x0200;
		
		//assorted constants needed
		public static uint MF_BYPOSITION = 0x400;
		public static uint MF_REMOVE = 0x1000;
		public static int GWL_STYLE = -16;
		public static int WS_CHILD = 0x40000000; //child window
		public static int WS_BORDER = 0x00800000; //window with border
		public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
		public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar 
		public static int WS_SYSMENU = 0x00080000; //window menu  
		public static int WS_THICKFRAME = 0x00040000;
		
			
		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);
		[DllImport("user32.dll")]
		public static extern int ShowWindow(int hwnd, int cmdShow);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		
		[DllImport("USER32.DLL")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		
		//Gets window attributes
		[DllImport("USER32.DLL")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
		
		[DllImport("user32.dll")]
		public static extern IntPtr GetMenu(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		public static extern int GetMenuItemCount(IntPtr hMenu);
		
		[DllImport("user32.dll")]
		public static extern bool DrawMenuBar(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
		
		[DllImport("user32.dll", EntryPoint = "FindWindow")]
		public static extern IntPtr FindWindowEx( IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow );

		#endregion form dll
		
		
		
		public delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

		[DllImport("user32")]
	    [return: MarshalAs(UnmanagedType.Bool)]
	    public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);
	
	    public static List<IntPtr> GetAllChildHandles(IntPtr _MainHandle)
	    {
	        List<IntPtr> childHandles = new List<IntPtr>();
	
	        GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
	        IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);
	
	        try
	        {
	            EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
	            EnumChildWindows(_MainHandle, childProc, pointerChildHandlesList);
	        }
	        finally
	        {
	            gcChildhandlesList.Free();
	        }
	
	        return childHandles;
	    }
	    
	    private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
	    {
	        GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);
	
	        if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
	        {
	            return false;
	        }
	
	        List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
	        childHandles.Add(hWnd);
	
	        return true;
	    }
	}
}
