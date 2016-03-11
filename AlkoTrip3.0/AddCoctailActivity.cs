using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.App;
using Android.Provider;

using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Android.Net;

namespace AlkoTrip3._0
{
    [Activity(Label = "AlkoTrip3.0/ Adding New Coctail")]
    public class AddCoctailActivity: Activity
    {
        Button backFromNewCoctail;
        Button CreateNewCotail;
        Button addPhoto;
        static String desc = "";
        static String nm = "";
        Button addCompExisted;
        Button addCompNew;
        private File _dir;
        private File _file;
        EditText editNameCoct;
        EditText editDiscription;
        TextView addingView;
        ImageView imageView;
        static Android.Net.Uri imageUri = null;

        private void CreateDirectoryForPictures()
        {
            _dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));

            StartActivityForResult(intent, 0);
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                imageUri = Android.Net.Uri.FromFile(_file);
                var imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                imageView.SetImageURI(imageUri);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.NewCoctail);

            backFromNewCoctail = (Button)FindViewById(Resource.Id.backFromNewCoctail);
            CreateNewCotail = (Button)FindViewById(Resource.Id.CreateNewCotail);
            addPhoto = (Button)FindViewById(Resource.Id.addPhoto);
            addCompExisted = (Button)FindViewById(Resource.Id.addCompExisted);
            addCompNew = (Button)FindViewById(Resource.Id.addCompNew);
            editNameCoct = (EditText)FindViewById(Resource.Id.editNameCoct);
            editDiscription = (EditText)FindViewById(Resource.Id.editDiscription);
            imageView = (ImageView)FindViewById(Resource.Id.imageView1);
            addingView = (TextView)FindViewById(Resource.Id.addingTextView);
            addingView.Text = "Adding New Coctail";

            CreateNewCotail.Visibility = ViewStates.Visible;
            addPhoto.Visibility = ViewStates.Visible;
            addCompExisted.Visibility = ViewStates.Visible;
            addCompNew.Visibility = ViewStates.Visible;

            editNameCoct.Text = nm;
            editDiscription.Text = desc;
            if (imageUri != null)
            {
                imageView.SetImageURI(imageUri);
            }

            Core.chooseExistedComponents.Sort((x, y) => x.getName().CompareTo(y.getName()));
            foreach (Component i in Core.chooseExistedComponents)
            {
                LinearLayout tempLayout = new LinearLayout(this);
                tempLayout.Orientation = Orientation.Horizontal;

                LinearLayout.LayoutParams paras = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                paras.Weight = 1;
                paras.BottomMargin = 10;
                paras.TopMargin = 10;
                paras.Height = LinearLayout.LayoutParams.MatchParent;
                paras.Width = LinearLayout.LayoutParams.MatchParent;

                TextView tvName = new TextView(this);
                tvName.LayoutParameters = paras;
                tvName.TextSize = 20;
                tvName.Gravity = GravityFlags.CenterVertical;
                tvName.SetTextColor(Android.Graphics.Color.White);
                tvName.Text = i.getName();
                tvName.LayoutParameters = paras;

                tempLayout.AddView(tvName);

                LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.scrollLayout2);
                ll.AddView(tempLayout);
            }

            if (Core.viewCoctailNow)
            {
                CreateNewCotail.Visibility = ViewStates.Invisible;
                addPhoto.Visibility = ViewStates.Invisible;
                addCompExisted.Visibility = ViewStates.Invisible;
                addCompNew.Visibility = ViewStates.Invisible;
                editNameCoct.Text = Core.viewCoctail.getName();
                editDiscription.Text = Core.viewCoctail.getDescription();
                addingView.Text = Core.viewCoctail.getName();
                imageView.SetImageURI(Core.viewCoctail.getImageUri());

                Core.viewCoctail.getComponents().Sort((x, y) => x.getName().CompareTo(y.getName()));
                foreach (Component i in Core.viewCoctail.getComponents())
                {
                    LinearLayout tempLayout = new LinearLayout(this);
                    tempLayout.Orientation = Orientation.Horizontal;

                    LinearLayout.LayoutParams paras = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                    paras.Weight = 1;
                    paras.BottomMargin = 10;
                    paras.TopMargin = 10;
                    paras.Height = LinearLayout.LayoutParams.MatchParent;
                    paras.Width = LinearLayout.LayoutParams.MatchParent;

                    TextView tvName = new TextView(this);
                    tvName.LayoutParameters = paras;
                    tvName.TextSize = 20;
                    tvName.Gravity = GravityFlags.CenterVertical;
                    tvName.SetTextColor(Android.Graphics.Color.White);
                    tvName.Text = i.getName();
                    tvName.LayoutParameters = paras;

                    tempLayout.AddView(tvName);

                    LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.scrollLayout2);
                    ll.AddView(tempLayout);
                }

                backFromNewCoctail.Click += (sender, e) =>
                {
                    Core.viewCoctailNow = false;
                    StartActivity(typeof(MainActivity));
                };
            }
            else
            {

                backFromNewCoctail.Click += (sender, e) =>
                {
                    desc = "";
                    nm = "";
                    imageUri = null;
                    Core.chooseExistedComponents.Clear();
                    StartActivity(typeof(MainActivity));
                };

                CreateNewCotail.Click += (sender, e) =>
                {
                    if (!editNameCoct.Text.Equals("") && !editDiscription.Text.Equals("") && Core.chooseExistedComponents.Count > 0 && imageUri != null)
                    {
                        String name = editNameCoct.Text;
                        String descr = editDiscription.Text;
                        Coctail tempCoct = new Coctail(name, imageUri, descr, Core.chooseExistedComponents);
                        Core.allCoctails.Add(tempCoct);
                        Core.writeCoctailInFile(tempCoct);
                        Core.chooseExistedComponents.Clear();
                        desc = "";
                        imageUri = null;
                        nm = "";

                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                            AlertDialog.Builder builder;
                            builder = new AlertDialog.Builder(this);
                            builder.SetTitle("Empty Fields");
                            builder.SetMessage("Please, fill all fields");
                            builder.SetCancelable(false);
                            builder.SetPositiveButton("OK", delegate { });
                            builder.Show();
                        }
                     );
                    }


                };

                if (IsThereAnAppToTakePictures())
                {   
                    CreateDirectoryForPictures();

                    addPhoto = FindViewById<Button>(Resource.Id.addPhoto);
                    imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                    addPhoto.Click += TakeAPicture;
                }

                addCompExisted.Click += (sender, e) =>
                {
                    desc = editDiscription.Text;
                    nm = editNameCoct.Text;
                    StartActivity(typeof(ComponentsListActivity));
                };

                addCompNew.Click += (sender, e) =>
                {
                    desc = editDiscription.Text;
                    nm = editNameCoct.Text;
                    StartActivity(typeof(AddComponentActivity));
                };
            }
        }

    }
}