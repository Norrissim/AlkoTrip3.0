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

namespace AlkoTrip3._0
{
    [Activity(Label = "AlkoTrip3.0/ Components List")]
    public class ComponentsListActivity : Activity
    {
        Button backFromList;
        Button chooseList;
        List<Component> tempList = new List<Component>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ComponentsList);

            backFromList = (Button)FindViewById(Resource.Id.backFromList);
            chooseList = (Button)FindViewById(Resource.Id.chooseList);

            Core.allComponents.Sort((x, y) => x.getName().CompareTo(y.getName()));
            foreach(Component i in Core.allComponents)
            {
                LinearLayout tempLayout = new LinearLayout(this);
                tempLayout.Orientation = Orientation.Horizontal;

                LinearLayout.LayoutParams paras = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                paras.Weight = 1;
                paras.Height = LinearLayout.LayoutParams.MatchParent;
                paras.Width = LinearLayout.LayoutParams.MatchParent;

                TextView tvName = new TextView(this);
                tvName.LayoutParameters = paras;
                tvName.TextSize = 20;
                tvName.Gravity = GravityFlags.CenterVertical;
                tvName.SetTextColor(Android.Graphics.Color.White);
                tvName.Text = i.getName() + " :";
                tvName.LayoutParameters = paras;

                CheckBox checkBox = new CheckBox(this);
                checkBox.Id = Core.GlobalIdNumber++;

                tempLayout.AddView(tvName);
                tempLayout.AddView(checkBox);

                LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.scrollLayout3);
                ll.AddView(tempLayout);

                checkBox.Click += (sender, e) =>
                {
                    if(checkBox.Selected == true)
                    {
                        checkBox.Selected = false;
                        tempList.Remove(i);
                    }
                    else
                    {
                        checkBox.Selected = true;
                        tempList.Add(i);
                    }
                };
            }

            backFromList.Click += (sender, e) =>
            {
                tempList.Clear();
                StartActivity(typeof(AddCoctailActivity));
            };

            chooseList.Click += (sender, e) =>
            {
                Core.chooseExistedComponents.Clear();
                foreach(Component i in tempList)
                {
                    Core.chooseExistedComponents.Add(i);
                }
                StartActivity(typeof(AddCoctailActivity));
            };

            
        }
    }
}