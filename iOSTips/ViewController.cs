﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using UIKit;
using Foundation;

namespace iOSTips
{
	public partial class ViewController : UIViewController
	{
		private List<Tip> Tips { get; set; }

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// 
			Tips = new List<Tip>();

			Tips.Add (new Tip { Name = "UITextField and Keyboard", Description = "" });
			Tips.Add (new Tip { Name = "Local Notification", Description = "" });
			Tips.Add (new Tip { Name = "Play Video", Description = "" });
			Tips.Add (new Tip { Name = "QR Code", Description = "" });
			Tips.Add (new Tip { Name = "Load XIB", Description = "" });
			Tips.Add (new Tip { Name = "Camera", Description = "" });
			Tips.Add (new Tip { Name = "Google Sign-In", Description = ""});
			Tips.Add (new Tip { Name = "Touch and Gesture", Description = "" });
			Tips.Add (new Tip { Name = "NSDefaults and KeyChain", Description = "" });
			Tips.Add (new Tip { Name = "File IO", Description = "" });
			Tips.Add (new Tip { Name = "SQLite", Description = "" });
			Tips.Add (new Tip { Name = "Web Service", Description = "" });
			Tips.Add (new Tip { Name = "Location", Description = "" });
			Tips.Add(new Tip { Name = "Accelerometer", Description = "" });

			var tableSource = new TipTableSource (Tips);
			tableSource.TipSelected += (object sender, TipSelectedEventArgs e) => {

				int index = e.Index ;

				switch( index ){
				case 0 :
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToKeyboardViewSegue", this);
						});
					}
					break;
				case 1:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToNotificationViewSegue", this);
						});
					}
					break;
				case 2:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToVideoViewSegue", this);
						});
					}
					break;
				case 3:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToQRViewSegue", this);
						});
					}
					break;
				case 4:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToLoadXibViewSegue", this);
						});
					}
					break;
				case 5:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToCameraViewSegue", this);
						});
					}
					break;
				case 6:
					{
						InvokeOnMainThread(()=>{
							PerformSegue("moveToGoogleSignInViewSegue", this);
						});

					}
					break;
				case 7: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToTouchGestureViewSegue", this);
						});
					}
					break;
				case 8: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToNSDefaultsViewSegue", this);
						});
					}
					break;
				case 9: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToFileIOViewSegue", this);
						});
					}
					break;
				case 10: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToSQLiteViewSegue", this);
						});
					}
					break;
				case 11: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToWebServiceViewSegue", this);
						});
					}
					break;
				case 12: 
					{
						InvokeOnMainThread (() => {
							PerformSegue ("moveToLocationViewSegue", this);
						});
					}
					break;
				case 13:
					{
						InvokeOnMainThread(() =>
						{
							PerformSegue("moveToAccelerometerViewSegue", this);
						});
					}
					break;
				}

					
			};
			tipsTable.Source = tableSource;

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


		public class TipTableSource : UITableViewSource {

			string TipCellViewIdentifier = "TipCellView";

			List<Tip> Tips;

			public TipTableSource (IEnumerable<Tip> tips){

				this.Tips = new List<Tip>() ;

				this.Tips.AddRange( tips );

			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return this.Tips.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				TipCellView cell = tableView.DequeueReusableCell (TipCellViewIdentifier) as TipCellView ;

				// Data
				var tip = this.Tips[indexPath.Row];
				cell.Update (tip);

				return cell;

			}

			public event EventHandler<TipSelectedEventArgs> TipSelected;

			public virtual void OnAirportSelected(TipSelectedEventArgs e){
				EventHandler<TipSelectedEventArgs> handler = TipSelected;

				if (null != handler) {
					handler( this, e);
				}

			}


			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				tableView.DeselectRow (indexPath, true);

				Tip tip = Tips[indexPath.Row];
				var args = new TipSelectedEventArgs { Index = indexPath.Row, SelectedTip = tip};

				OnAirportSelected (args);
			}

		}

	}



	public class TipSelectedEventArgs : EventArgs{
		public int Index { get; set; }
		public Tip SelectedTip { get; set;}
	}

	public class Tip{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

