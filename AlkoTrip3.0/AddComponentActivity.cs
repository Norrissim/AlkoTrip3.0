using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonoDroid.Utils;

namespace AlkoTrip3._0
{
    [Activity(Label = "AlkoTrip3.0/ Adding New Component")]
    public class AddComponentActivity:Activity
    {
        Button backFromAddComponent;
        Button addNewComp;
        EditText editCompanentName;
        EditText editCompanentDegree;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NewCompanent);

            backFromAddComponent = (Button)FindViewById(Resource.Id.backFromAddComp);
            addNewComp = (Button)FindViewById(Resource.Id.addNewComp);
            editCompanentName = (EditText)FindViewById(Resource.Id.editCompanentName);
            editCompanentDegree = (EditText)FindViewById(Resource.Id.editCompanentDegree);

            backFromAddComponent.Click += (sender, e) =>
            {
                StartActivity(typeof(AddCoctailActivity));
            };

            addNewComp.Click += (sender, e) =>
            {
                if (!editCompanentName.Text.Equals("") && !editCompanentDegree.Text.Equals(""))
                {
                    String tempDeg = editCompanentDegree.Text;
                    String tempName = editCompanentName.Text;
                    tempName = tempName.ToLower();
                    tempName = Core.FirstCharToUpper(tempName);
                    bool nameAgain = true;
                    foreach(Component j in Core.allComponents)
                    {
                        if(j.getName().Equals(tempName))
                        {
                            nameAgain = false;
                        }
                    }
                    if (nameAgain == false)
                    {
                        AlertDialog.Builder builder;
                        builder = new AlertDialog.Builder(this);
                        builder.SetTitle("already exists");
                        builder.SetMessage("Sorry, but this component already exists");
                        builder.SetCancelable(false);
                        builder.SetPositiveButton("OK", delegate { });
                        builder.Show();
                    }
                    else
                    {
                        Component tempComponent = new Component(tempName, Int32.Parse(tempDeg));
                        Core.allComponents.Add(tempComponent);
                        Core.writeComponentInFile(tempComponent);
                        Core.chooseExistedComponents.Add(tempComponent);
                        StartActivity(typeof(AddCoctailActivity));
                    }
                    
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
        }
    }
}