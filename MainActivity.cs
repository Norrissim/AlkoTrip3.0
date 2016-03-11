using Android.App;
using Android.Widget;
using Android.OS;

namespace AlkoTrip3
{
	[Activity (Label = "AlkoTrip3.0", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		EditText editLitres;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button buttonLess = FindViewById<Button> (Resource.Id.less1);
			Button buttonMore = FindViewById<Button> (Resource.Id.more1);
			editLitres = FindViewById<EditText> (Resource.Id.val1);
			buttonLess.SetOnClickListener (this);
		}

		public void ButtonClickLess()
		{
			editLitres = FindViewById<EditText> (Resource.Id.val1);
			editLitres.SetText (3);
		}
	}
}


