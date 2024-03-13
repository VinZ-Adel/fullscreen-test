using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace fullscreen_test
{
	public class Win32
	{
		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint a);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttatchTo, bool fAttatch);

		[DllImport("user32.dll")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);

		public const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
		public const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;

		public const uint LSFW_UNLOCK = 2;
		public const uint ASFW_ANY = unchecked((uint)-1);
		public const int SW_RESTORE = 9;

		[DllImport("user32.dll")]
		public static extern bool LockSetForegroundWindow(uint uLockCode);

		[DllImport("user32.dll")]
		public static extern bool AllowSetForegroundWindow(uint dwProcessId);

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(nint hWnd);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(nint hWnd, int nCmdShow);
	}
}
