/*
 * Created by SharpDevelop.
 * User: IstvanT
 * Date: 6/5/2016
 * Time: 11:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Operations
{
	public static class NotifyOperation
	{
		// Global variables accessed by other classes/multiple functions.
		static string currentDate = DateTime.Now.ToString(@"MM\/dd");
		static string currentDir = @"\Items\" + currentDate;
		static Button addButton = new Button();
		
		
		
		public static void FillScreen(Form emptyScreen, int size)
			{
			int currentPos = 10;
			string[] files = Directory.GetFiles(currentDir, "*.txt");
			
			//based on the files in currentDir, we build our list
			foreach(string file in files)
				{
				BuildLabel(emptyScreen, file, currentPos);
				currentPos += 170;
				}
			
			CreateButton(emptyScreen, size);
			
			}

		private static void BuildLabel(Form buildScreen, string file, int pos)
			{
			Button header = new Button();
			Label body = new Label();
			Button doneButton = new Button();
			buildScreen.Controls.Add(header);
			buildScreen.Controls.Add(body);
			//buildScreen.Controls.Add(doneButton);
			
			
			
			// make the header from the file name. Header is 30x375px.

			header.Height = 30;
			header.Width = 280;
			header.Location = new Point(10, pos);
			header.Text = Path.GetFileNameWithoutExtension(file);
			header.BackColor = Color.Blue;
			header.Click += delegate(object sender, EventArgs e) 
				{ itemDone(sender, e, header.Text, buildScreen); };
			

			// Make the body from the file content. Body is 100x375px and has a 10px buffer above it.

			body.Height = 100;
			body.Width = 375;
			body.Location = new Point(10, (pos + 40));
			body.BackColor = Color.Yellow;
			StreamReader reader = new StreamReader(file);
			do
				{
				body.Text += reader.ReadLine();
				}
			while (reader.Peek() != -1);
			
			// make the Done! button	
			/*
			doneButton.Height = 30;
			doneButton.Width = 85;
			doneButton.Location = new Point(300, pos);
			doneButton.Text = "Done!";
			doneButton.Click += itemDone;
			*/
			}
		
		private static void CreateButton(Form screen, int height)
		{
			
			addButton.Location = new Point(20, height-60);
			addButton.Width = 355;
			addButton.Height = 50;
			addButton.Text = "Add an item";
			screen.Controls.Add(addButton);
			addButton.Click += addItemForm;
		}
		
		private static void addItemForm(object sender, EventArgs e)
			{
			// When button is clicked, a new form appears.
			SetUpAddForm.AddForm.SetAddForm();
			}
		
		public static void addItem(object sender, EventArgs e)
			{
			string name = SetUpAddForm.AddForm.itemTitle.Text + ".txt";
			StreamWriter writer = new StreamWriter(currentDir + @"\" + name );
			writer.WriteLine(SetUpAddForm.AddForm.itemDescription.Text);
    		writer.Close();
    		
    		SetUpAddForm.AddForm.addForm.Hide();
			SetUpAddForm.AddForm.itemTitle.Clear();
			SetUpAddForm.AddForm.itemDescription.Clear();
    		}
		
		private static void itemDone(object sender, EventArgs e, string fileName, Form parentScreen)
			{
			parentScreen.Close();
			string name = currentDir + @"\" + fileName + ".txt";
			File.Delete(name);
			}
		
	}
}
