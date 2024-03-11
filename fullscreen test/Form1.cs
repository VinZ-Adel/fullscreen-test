using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace fullscreen_test
{
	public partial class Form1 : Form
	{
		List<Process> GamesAndMenu = [];
		bool KeepFocus = true;
		public Form1()
		{
			InitializeComponent();
			//this.FormBorderStyle = FormBorderStyle.None;
			//this.WindowState = FormWindowState.Maximized;
			GamesAndMenu.Add(Process.GetCurrentProcess()); // Adds this to process list, should be in position [0]
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string game = @"Blackout.exe"; //game .exe name
			string path = Path.GetDirectoryName(Application.ExecutablePath) ?? @"C:\\";
			label1.Text = path;
			try
			{
				GamesAndMenu.Add(Process.Start(path + @"\" + game)); // starts the indicated game and adds it to process list
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
		static extern nint GetForegroundWindow();

		[DllImport("kernel32.dll")]
		static extern int GetProcessId(IntPtr handle);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (KeepFocus)
			{
				List<Process> remove = [];
				foreach (Process p in GamesAndMenu)
				{
					if (p.HasExited)
						remove.Add(p);
				}
				foreach (Process p in remove.Intersect(GamesAndMenu))
					GamesAndMenu.Remove(p);

				uint pid;
				GetWindowThreadProcessId(GetForegroundWindow(), out pid);
				Process currentFocus = Process.GetProcessById((int)pid);
				if (currentFocus != GamesAndMenu[^1])
				{
					IntPtr hWnd = GamesAndMenu[^1].Handle;
					ShowWindow(hWnd, 3);
					SetForegroundWindow(hWnd);
					GetWindowThreadProcessId(GetForegroundWindow(), out pid);
				}
			}
		}
	}
}
