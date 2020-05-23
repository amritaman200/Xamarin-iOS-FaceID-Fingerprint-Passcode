using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FaceIDSample
{
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            On<iOS>().SetUseSafeArea(true);
            InitializeComponent();
            //string name = DependencyService.Get<IDependencyService>().GetName();
            //name_Label.Text = name;
            

        }

        //Fingerprint Event
        public void Fingerprint_Login(object sender, EventArgs e)
        {

            if (DependencyService.Get<IDependencyService>().CheckFingerPrintAvailable())
            {
                if (DependencyService.Get<IDependencyService>().Authenticted())
                {
                    DisplayAlert("Notice", "Your Are Authenticated", "Ok");
                }
                else
                {

                }
            }
            else if (!DependencyService.Get<IDependencyService>().CheckFingerPrintAvailable())
            {
                DisplayAlert("Notice", "FingerPrint Not Available", "Ok");
            }
        }

        //Facelogin Event
        public void Face_Login(object sender,EventArgs e)
        {
            if (DependencyService.Get<IDependencyService>().CheckFaceIDAvailable())
            {
                if (DependencyService.Get<IDependencyService>().Authenticted())
                {
                    DisplayAlert("Notice", "Your Are Authenticated", "Ok");
                }
                else
                {

                }
            }
            else
            {
                DisplayAlert("Notice", "Face ID Not Available", "Ok");
            }
        }
    }
}
