using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace fullscreen_test
{
	public partial class Form1 : Form
	{
		List<KeyValuePair<Process, IntPtr>> GamesAndMenuWithHandles = [];
		bool KeepFocus = true;
		public Form1()
		{
			InitializeComponent();
			//this.FormBorderStyle = FormBorderStyle.None;
			//this.WindowState = FormWindowState.Maximized;
			GamesAndMenuWithHandles.Add(new(Process.GetCurrentProcess(), this.Handle)); // Adds this to process list, should be in position [0]
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string game = @"Blackout.exe"; //game .exe name
			string path = Path.GetDirectoryName(Application.ExecutablePath) ?? @"C:\\";
			label1.Text = path;
			try
			{
				Process temp = Process.Start(path + @"\" + game);       // starts the indicated game and adds it to process list
				GamesAndMenuWithHandles.Add(new(temp, temp.MainWindowHandle));  // ^
			}
			catch { label1.Text = "Error"; }
		}

		private void KeyPressChecker(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape)
				this.WindowState = FormWindowState.Minimized;
			if (e.KeyChar == (char)Keys.Tab)
				KeepFocus = !KeepFocus;
		}

		[DllImport("user32.dll")]
		internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("kernel32.dll")]
		static extern int GetProcessId(IntPtr handle);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out nint a);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(int idAttach, int idAttatchTo, bool fAttatch);

		[DllImport("user32.dll")]
		public static extern bool BringWindowToTop(IntPtr hWnd);


		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		const UInt32 SWP_NOSIZE = 0x0001;
		const UInt32 SWP_NOMOVE = 0x0002;
		const UInt32 SWP_SHOWWINDOW = 0x0040;

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (KeepFocus)
			{
				List<KeyValuePair<Process, IntPtr>> remove = [];
				foreach (KeyValuePair<Process, IntPtr> p in GamesAndMenuWithHandles)
				{
					if (p.Key.HasExited)
						remove.Add(p);
				}
				foreach (KeyValuePair<Process, IntPtr> p in remove)
					GamesAndMenuWithHandles.Remove(p);

				IntPtr bluh = GetForegroundWindow();
				IntPtr test = this.Handle;

				if (GamesAndMenuWithHandles.Any(c => c.Value == bluh)) { label1.Text = "same"; }
				else if (GamesAndMenuWithHandles.Any(c => c.Key.MainWindowHandle == bluh)) { label1.Text = "SAME"; }
				else
					label1.Text = "not";


				Process[] processesrunning = Process.GetProcesses();
				foreach (Process process in processesrunning)
				{
					if (process.MainWindowHandle == bluh)
					{
						AttachThreadInput(Process.GetCurrentProcess().Id, process.Id, true);
						IntPtr hWnd = GamesAndMenuWithHandles[^1].Key.MainWindowHandle; // THIS WORkS (.Key.MainWindowHandle), .Value DOES NOT
						BringWindowToTop(hWnd);
						AttachThreadInput(Process.GetCurrentProcess().Id, process.Id, false);
					}
				}

				//nint parentid;
				//nint pid;
				//pid = GetWindowThreadProcessId(GetForegroundWindow(), out parentid);
				//Process currentFocus = Process.GetProcessById((int)pid);
				//if (currentFocus != GamesAndMenuWithHandles[^1].Key) //should be !GamesAndMenu.Contains(currentFocus) in real one
				//{
				//	AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)pid, true);
				//	IntPtr hWnd = GamesAndMenuWithHandles[^1].Value;
				//	BringWindowToTop(hWnd);
				//	//ShowWindow(hWnd, 3);
				//	//SetWindowPos(hWnd, -1, 0, 0, 0, 0, 0x0002 | 0x0001); // sets as topmost
				//	AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)pid, false);
				//}
			}
			else
			{
				foreach (KeyValuePair<Process, IntPtr> pro in GamesAndMenuWithHandles)
					SetWindowPos(pro.Value, -2, 0, 0, 0, 0, 0x0002 | 0x0001);
			} // removes topmost
		}

		private void topmost(object sender, EventArgs e) // still does not work
		{
			nint parentid;
			nint pid;
			IntPtr bluh = GetForegroundWindow();
			IntPtr test = this.Handle;

			if (bluh == test) { label1.Text = "same"; }


			Process[] processesrunning = Process.GetProcesses();
			foreach (Process process in processesrunning)
			{
				if (process.MainWindowHandle == bluh)
				{
					AttachThreadInput(Process.GetCurrentProcess().Id, process.Id, true);
					IntPtr hWnd = GamesAndMenuWithHandles[^1].Value;
					BringWindowToTop(hWnd);
					AttachThreadInput(Process.GetCurrentProcess().Id, process.Id, false);
				}
			}
			//try
			//{
			//	pid = GetWindowThreadProcessId(bluh, out parentid);

			//	if (pid != 0)
			//	{
			//		AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)pid, true);
			//	}
			//	else
			//	{
			//		AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)parentid, true);
			//	}

			//	IntPtr hWnd = GamesAndMenuWithHandles[^1].Value;
			//	BringWindowToTop(hWnd);
			//	//bool test = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
			//	if (pid != 0 )
			//		AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)pid, false);
			//	else
			//		AttachThreadInput(GamesAndMenuWithHandles[0].Key.Id, (int)parentid, false);
			//}
			//catch { label1.Text = "Id get error"; }
		}

		// Import user32.dll (containing the function we need) and define
		// the method corresponding to the native function.
		[DllImport("user32.dll")]
		private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

		private void button3_Click(object sender, EventArgs e)
		{
			// Invoke the function as a regular managed method.
			MessageBox(IntPtr.Zero, "Command-line message box", "Attention!", 0);
		}

		private void topmost_simple(object sender, EventArgs e)
		{
			SetWindowPos(GamesAndMenuWithHandles[^1].Value, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			foreach (KeyValuePair<Process, IntPtr> pro in GamesAndMenuWithHandles)
				SetWindowPos(pro.Value, -2, 0, 0, 0, 0, 0x0002 | 0x0001);
		}
	}
}
