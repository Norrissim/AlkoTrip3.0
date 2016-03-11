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
using Android.Graphics;

namespace AlkoTrip3._0
{
    class Coctail
    {
        private String name;
        private Android.Net.Uri imageUri;
        private String description;
        private int coctailButtonId;
        private List<Component> components;
        
        public Coctail(String name, Android.Net.Uri imageUri, String description, List<Component> components)
        {
            this.components = new List<Component>();
            this.name = name;
            this.imageUri = imageUri;
            this.description = description;
            this.coctailButtonId = Core.GlobalIdNumber++;
            foreach(Component i in components)
            {
                this.components.Add(i);
            }
        }

        public String getName()
        {
            return name;
        }

        public Android.Net.Uri getImageUri()
        {
            return imageUri;
        }

        public String getDescription()
        {
            return description;
        }

        public List<Component> getComponents()
        {
            return components;
        }

        public int getButtonId()
        {
            return coctailButtonId;
        }
    }
}