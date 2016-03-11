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
    class Component
    {
        private String name;
        private int degree;
        private int lessButtonId;
        private int moreButtonId;
        private int volumeBarId;
        private int volume;

        public Component(String name, int degree)
        {
            this.name = name;
            this.degree = degree;
            this.lessButtonId = Core.GlobalIdNumber++;
            this.moreButtonId = Core.GlobalIdNumber++;
            this.volumeBarId = Core.GlobalIdNumber++;
            this.volume = 0;
        }

        public String getName()
        {
            return name;
        }

        public int getDegree()
        {
            return degree;
        }

        public int getLessId()
        {
            return lessButtonId;
        }

        public int getMoreId()
        {
            return moreButtonId;
        }

        public int getVolumeBarId()
        {
            return volumeBarId;
        }

        public int getVolume()
        {
            return volume;
        }

        public void setVolume(int volume)
        {
            this.volume = volume;
        }
    }
}