﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;

using SQLite;

using Debug = System.Diagnostics.Debug;

namespace iOSTips
{
	public partial class SQLiteViewController : UIViewController
	{
		private SQLiteWorker Worker { get; set; }

		public SQLiteViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// 
			Worker = new SQLiteWorker ("todoitems.db");

			btnCreateDatabaseAndDataTable.TouchUpInside += (sender, e) => {
				Worker.CreateDatabase ();
			};

			btnWriteRecord.TouchUpInside += (sender, e) => {
				var todo = new ToDo { 
					Name = $"todo at { DateTime.Now.ToShortDateString () }", 
					Description = $"{ DateTime.Now.ToLongTimeString ()  }" };

				Worker.AddToDoItem ( todo);
			};

			btnReadRecord.TouchUpInside += (sender, e) => {

				var list = Worker.ReadTodoItems ();

				foreach (var todo in list) {
					Debug.WriteLine ($"Name:{ todo.Name },Description:{ todo.Description }");
				}

			};

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class SQLiteWorker {

		public SQLiteWorker (string databaseName) { 
			
			DatabasePath = Path.Combine (DocumentsPath, databaseName);

			Connection = new SQLiteConnection (DatabasePath);
		}

		public string DatabasePath { get; set; }

		public SQLiteConnection Connection { get; set; }

		public bool CreateDatabase () { 
			
			Connection.CreateTable<ToDo> ();

			return File.Exists (DatabasePath);
		}


		public void AddToDoItem (ToDo todo)
		{
			Connection.Insert (todo);

			Debug.WriteLine ( $"Write { todo.Name },{ todo.Description }");
		}

		public List<ToDo> ReadTodoItems ()
		{
			var results = new List<ToDo> ();

			var list = Connection.Table<ToDo> ();

			foreach (var item in list) {
				results.Add (new ToDo {
					Name = item.Name,
					Description = item.Description
				});

			}

			return results;

		}
	
		public static string DocumentsPath {
			get {
				var documentsDirUrl = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last ();
				Debug.WriteLine ($"documentsPath:{ documentsDirUrl.Path }");

				return documentsDirUrl.Path;
			}
		}
	}

	public class ToDo
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[MaxLength (25)]
		public string Name { get; set; }


		[MaxLength (50)]
		public string Description { get; set; }
	}
}


