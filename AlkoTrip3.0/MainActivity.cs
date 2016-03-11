using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AlkoTrip3._0
{
    [Activity(Label = "AlkoTrip3.0/ Main Menu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button newCoctail;
        private Button newComponent;
        private Button startParty;

        private void ChangePosible()
        {
            LinearLayout clLay = (LinearLayout)FindViewById(Resource.Id.layoutForCoct);
            clLay.RemoveAllViews();
            Core.posibleCoctails.Clear();
            bool flagok = false;
            foreach (Coctail t in Core.allCoctails)
            {
                foreach (Component j in t.getComponents())
                {
                    flagok = false;
                    foreach (Component k in Core.chooseComponents)
                    {
                        if (k.getName().Equals(j.getName()))
                        { flagok = true; }
                    }

                    if (flagok == true)
                    {
                        continue;
                    }
                    else
                    {
                        Core.posibleCoctails.Remove(t);
                        break;
                    }
                }
                if (flagok == true)
                {
                    Core.posibleCoctails.Add(t);
                }
            }

            Core.posibleCoctails.Sort((x, y) => x.getName().CompareTo(y.getName()));
            foreach (Coctail p in Core.posibleCoctails)
            {
                LinearLayout tempLayout1 = new LinearLayout(this);
                tempLayout1.Orientation = Orientation.Horizontal;

                LinearLayout.LayoutParams paras1 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                paras1.Weight = 1;
                paras1.Height = LinearLayout.LayoutParams.MatchParent;
                paras1.Width = LinearLayout.LayoutParams.MatchParent;

                LinearLayout.LayoutParams paras2 = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                paras2.Weight = 1;
                paras2.Height = LinearLayout.LayoutParams.WrapContent;
                paras2.Width = LinearLayout.LayoutParams.WrapContent;

                ImageView imView = new ImageView(this);
                imView.SetImageURI(p.getImageUri());
                ScrollView scView = new ScrollView(this);
                scView.LayoutParameters = paras1;
                LinearLayout infoVertLayout = new LinearLayout(scView.Context);
                infoVertLayout.Orientation = Orientation.Vertical;
                TextView nameText = new TextView(this);
                nameText.Text = "Name : " + p.getName();

                TextView ingrTextTitle = new TextView(this);
                ingrTextTitle.Text = "Components :";

                infoVertLayout.AddView(nameText);
                infoVertLayout.AddView(ingrTextTitle);
                foreach(Component q in p.getComponents())
                {
                    TextView ingrNameText = new TextView(this);
                    ingrNameText.Text = q.getName();
                    infoVertLayout.AddView(ingrNameText);
                }

                TextView discTitle = new TextView(this);
                discTitle.Text = "Discription : " + p.getDescription();
                infoVertLayout.AddView(discTitle);

                scView.AddView(infoVertLayout);
                tempLayout1.AddView(imView);
                tempLayout1.AddView(scView);

                LinearLayout ll1 = (LinearLayout)FindViewById(Resource.Id.layoutForCoct);
                ll1.AddView(tempLayout1);

                tempLayout1.Click += (sender , e) =>
                {
                    if (scView.LayoutParameters == paras1)
                        scView.LayoutParameters = paras2;
                    else
                        scView.LayoutParameters = paras1;
                };
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            if (!Core.init)
            {
                Core.allComponents.Clear();
                Core.readComponentsFromFile();

                Core.allCoctails.Clear();
                Core.readCoctailsFromFile();

                Core.init = true;
            }

            ChangePosible();

            newCoctail = (Button)FindViewById(Resource.Id.addCoct);

            Core.allComponents.Sort((x, y) => x.getName().CompareTo(y.getName()));
            foreach (Component i in Core.allComponents)
            {
                LinearLayout tempLayout = new LinearLayout(this);
                tempLayout.Orientation = Orientation.Horizontal;

                LinearLayout.LayoutParams paras = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                paras.Weight = 8;
                paras.BottomMargin = 10;
                paras.TopMargin = 10;
                paras.Height = LinearLayout.LayoutParams.MatchParent;
                paras.Width = LinearLayout.LayoutParams.MatchParent;
                Button b1 = new Button(this);
                b1.Text = "<";
                b1.Id = i.getLessId();
                Button b2 = new Button(this);
                b2.Text = ">";
                b2.Id = i.getMoreId();
                TextView tvVal = new TextView(this);
                tvVal.SetBackgroundColor(Android.Graphics.Color.White);
                tvVal.SetTextColor(Android.Graphics.Color.Black);
                tvVal.Text = i.getName() + " : " + i.getVolume() + " ml";
                tvVal.TextSize = 15;
                tvVal.Gravity = GravityFlags.CenterVertical;
                tvVal.Id = i.getVolumeBarId();
                tvVal.LayoutParameters = paras;

                tempLayout.AddView(b1);
                tempLayout.AddView(tvVal);
                tempLayout.AddView(b2);

                LinearLayout ll = (LinearLayout)FindViewById(Resource.Id.layoutForComponents);
                ll.AddView(tempLayout);

                b1.Click += (sender, e) =>
                {
                    if (i.getVolume() > 0)
                    {
                        i.setVolume(i.getVolume() - 50);
                        tvVal.Text = i.getName() + " : " + i.getVolume() + " ml";
                        if (i.getVolume() == 0)
                        {
                            Core.chooseComponents.Remove(i);
                        }
                    }
                    ChangePosible();
                };

                b2.Click += (sender, e) =>
                {
                    i.setVolume(i.getVolume() + 50);
                    tvVal.Text = i.getName() + " : " + i.getVolume() + " ml";
                    if (!Core.chooseComponents.Contains(i))
                    {
                        Core.chooseComponents.Add(i);
                    }
                    ChangePosible();
                };

                newCoctail.Click += (sender, e) =>
                {
                    StartActivity(typeof(AddCoctailActivity));
                };
            }

        }
    }
}

