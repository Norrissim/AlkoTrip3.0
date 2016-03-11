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
    class ComponentLayout
    {
        private Component component;
        private Button less;
        private Button more;
        private TextView val;

        public ComponentLayout(Component component)
        {
            this.component = component;
        }

        public LinearLayout generateLayout(Context context)
        {
            var layout = new LinearLayout (context);
            layout.Orientation = Orientation.Horizontal;

            var aLabel = new TextView (context);
            aLabel.Text = "Hello, World!!!";

            var aButton = new Button (context);
            aButton.Text = "Say Hello!";

            aButton.Click +=(sender, e) => 
            {aLabel.Text="Hello Android!";};

            layout.AddView (aLabel);
            layout.AddView (aButton);
            return layout;
        }
    }
}