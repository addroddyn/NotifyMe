
// Program start. Mostly created by IDE

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
namespace NotifyNotes
{
	public sealed class NotificationIcon
	{
		// The global variables needed for the program.
		public static bool isListOpen = false;
		static string currentDate = DateTime.Now.ToString(@"MM\/dd");
		static string currentDir = @"\Items\" + currentDate;


		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		
		#region Initialize icon and menu
		public NotificationIcon()
		{
			notifyIcon = new NotifyIcon();
			notificationMenu = new ContextMenu(InitializeMenu());
			
			notifyIcon.Click += IconClick;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			notifyIcon.ContextMenu = notificationMenu;
		}
		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("About", menuAboutClick),
				new MenuItem("Exit", menuExitClick)
			};
			return menu;
		}
		#endregion

		// Check for dir, if not present, create

		private static void CheckDir()
			{
			if (!(Directory.Exists(currentDir)))
				{
				MessageBox.Show(@"C:\NotesItems\ folder was not found. Creating it.");
				Directory.CreateDirectory(currentDir);
				}
			}
		
		#region Main - Program entry point
		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (Mutex mtx = new Mutex(true, "NotifyNotes", out isFirstInstance)) {
				if (isFirstInstance) {
					NotificationIcon notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					CheckDir();
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
				} else {
					// The application is already running
					// TODO: Display message box or change focus to existing application instance
				}
			} // releases the Mutex
			
			}
		#endregion
		
		#region Event Handlers
		private void menuAboutClick(object sender, EventArgs e)
		{
			MessageBox.Show("This is still a work in progress. Made by addroddyn, 2016.");
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void IconClick(object sender, EventArgs e)
		{
			if (!(SetUpMainForm.MainForm.isFormOpen))
				{
				Point clickPosition = new Point(Cursor.Position.X, Cursor.Position.Y);
				SetUpMainForm.MainForm.SetUpForm(clickPosition);
				}
		}
		#endregion
	}
}
