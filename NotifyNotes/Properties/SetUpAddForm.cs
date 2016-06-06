

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace SetUpAddForm
{
	public static class AddForm
	{
		// Global variables used by other classes and multiple functions
		public static Form addForm = new Form();
		public static TextBox itemTitle = new TextBox();
		public static TextBox itemDescription = new TextBox();
		
		public static void SetAddForm()
		{
			
			
			addForm.StartPosition = FormStartPosition.CenterScreen;
			addForm.ControlBox = false;
			const int width = 300;
			const int height = 200;
			addForm.Size = new Size(width, height);
			addForm.ShowInTaskbar = false;
			addForm.FormBorderStyle = FormBorderStyle.FixedSingle;
			SetUpButtons();
			SetUpTextBox();
			addForm.Show();
		}
		
		private static void SetUpButtons()
		{
			// Set up the add button.
			Button addButton = new Button();
			addButton.Location = new Point(10, 160);
			addButton.Width = 135;
			addButton.Height = 30;
			addButton.Text = "Add item";
			addForm.Controls.Add(addButton);
			addButton.Click += Operations.NotifyOperation.addItem;
			
			// Set up the close button.
			Button closeButton = new Button();
			closeButton.Location = new Point(155, 160);
			closeButton.Width = 135;
			closeButton.Height = 30;
			closeButton.Text = "Close";
			addForm.Controls.Add(closeButton);
			closeButton.Click += CloseAddForm;
		}
		
		private static void SetUpTextBox()
		{
			// Set up the Title box
			itemTitle.Width = 280;
			itemTitle.Height = 30;
			itemTitle.Location = new Point(10, 10);
			addForm.Controls.Add(itemTitle);
			
			// Set up the Description box
			
			//itemDescription.Width = 280;
			//itemDescription.Height = 100;
			itemDescription.Size = new Size(280, 100);
			itemDescription.Location = new Point(10, 50);
			itemDescription.Multiline = true;
			itemDescription.AcceptsReturn = true;
			
			addForm.Controls.Add(itemDescription);
		}
		
		private static void CloseAddForm(object sender, EventArgs e)
			{
			addForm.Hide();
			itemTitle.Clear();
			itemDescription.Clear();
			}
	}
}
