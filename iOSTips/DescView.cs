// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace iOSTips
{
	public partial class DescView : UIView
	{
		public DescView (IntPtr handle) : base (handle)
		{
		}

		public void Update(string imageName ){
		
			imageView.Image = UIImage.FromFile (imageName);
		
		}
	}
}