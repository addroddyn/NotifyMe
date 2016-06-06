// This is for designing the Main form.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Operations;

namespace SetUpMainForm
{
	public static class MainForm
	{
		// Global variables accessed by other classes/multiple functions.
		static string currentDate = DateTime.Now.ToString(@"MM\/dd");
		static string currentDir = @"\Items\" + currentDate;
		public static bool isFormOpen = false;
		public static Form screen = new Form();

		public static void SetUpForm (Point position)
		{
			// Set flag to true.
			isFormOpen = true;

			// Set up the Form according to the number of items it finds in the folder.
			screen.StartPosition = FormStartPosition.Manual;
			const int width = 400;
			int height = (CountItems() * 170) + 75;
			screen.Location = GetLocation(position, height);
			screen.Size = new Size(width, height);
			screen.FormBorderStyle = FormBorderStyle.FixedSingle;
			screen.ControlBox = false;
			screen.ShowInTaskbar = false;
			
			
			// Create the list.
			NotifyOperation.FillScreen(screen, height);
			
			// Show list and make it the active window
			screen.Show();
			screen.Activate();
			
			// Create the event for deactivation
			screen.Deactivate += formDeactivated;
			
		}
		
		// Get the location of the click, and, based on that, change the location of the window
		private static Point GetLocation(Point clickPosition, int listSize)
		{
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			Point startlocation = new Point();
			if (clickPosition.X < (screenWidth/2))
			{
				if (clickPosition.Y < (screenHeight/2))
				{
					startlocation.X = 100;
					startlocation.Y = 50;
					return startlocation;
				}
				else 
				{
					startlocation.X = 100;
					startlocation.Y = screenHeight-(listSize + 50);
					return startlocation;
				}
			}
			else
			{
				if (clickPosition.Y < (screenHeight/2))
				{
					startlocation.X = screenWidth - 500;
					startlocation.Y = 50;
					return startlocation;
				}
				else 
				{
					startlocation.X = screenWidth - 500;
					startlocation.Y = screenHeight-(listSize + 50);
					return startlocation;
				}
			}
				
		}

		//Close form when it's deactivated
		private static void formDeactivated(object sender, EventArgs e)
			{
			isFormOpen = false;
			screen.Hide();
			}

		private static int CountItems()
			{
			string[] files = Directory.GetFiles(currentDir, "*.txt");
			return files.Length;			
			}

	}
}
