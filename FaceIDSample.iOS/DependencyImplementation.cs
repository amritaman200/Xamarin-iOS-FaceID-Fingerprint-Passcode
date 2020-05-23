using System;
using FaceIDSample;
using Foundation;
using LocalAuthentication;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FaceIDSample.iOS.DependencyImplementation))]
namespace FaceIDSample.iOS
{
    public class DependencyImplementation : UIViewController, IDependencyService
    {
        LAContextReplyHandler replyHandler;
        bool IsValidFace;
        /// <summary>String to use for display</summary>
        string BiometryType = "";
        public string GetName()
        {
            return "Ami123321";
        }
        public DependencyImplementation()
        {
            
        }

       

        public bool CheckFaceIDAvailable()
        {
            var context = new LAContext();
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError1))
            { // has Biometrics (Touch or Face)
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    string biometrictype = context.BiometryType == LABiometryType.FaceId ? "Face ID" : "Touch ID";

                    if (biometrictype == "Face ID")
                    {
                        return true;
                    }
                    else if (biometrictype == "Touch ID")
                    {
                        return false;
                    }
                    //context.LocalizedReason = "Authorize for access to secrets"; // iOS 11
                    // BiometryType = context.BiometryType == LABiometryType.TouchId ? "Touch ID" : "Face ID";
                    //buttonText = $"Login with {BiometryType}";
                }
                else
                {   // no FaceID before iOS 11
                    // buttonText = $"Login with Touch ID";
                }
            }
            return false;
        }

        public bool FaceIdAvailable()
        {
            return true;
        }

        public bool Authenticted()
        {
            var context = new LAContext();
            NSError AuthError;
            var localizedReason = new NSString("To access secrets");

            // because LocalAuthentication APIs have been extended over time, need to check iOS version before setting some properties
            context.LocalizedFallbackTitle = "Fallback"; // iOS 8

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                context.LocalizedCancelTitle = "Cancel"; // iOS 10
            }
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                Console.WriteLine("TouchID/FaceID available/enrolled");
                replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    //Make sure it runs on MainThread, not in Background
                    this.InvokeOnMainThread(() =>
                    {
                        if (success)
                        {
                            //Console.WriteLine($"You logged in with {BiometryType}!");

                            //PerformSegue("AuthenticationSegue", this);
                            IsValidFace = true;
                        }
                        else
                        {
                            IsValidFace = false;
                            //Console.WriteLine(error.LocalizedDescription);
                            //Show fallback mechanism here
                            //unAuthenticatedLabel.Text = $"{BiometryType} Authentication Failed";
                            //AuthenticateButton.Hidden = true;
                        }
                    });

                });
                //Use evaluatePolicy to start authentication operation and show the UI as an Alert view
                //Use the LocalAuthentication Policy DeviceOwnerAuthenticationWithBiometrics
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, localizedReason, replyHandler);
                return IsValidFace;
            }
            return IsValidFace;
        }

        public bool CheckFingerPrintAvailable()
        {
            var context = new LAContext();
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError1))
            { // has Biometrics (Touch or Face)
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    string biometrictype = context.BiometryType == LABiometryType.FaceId ? "Face ID" : "Touch ID";

                    if (biometrictype == "Face ID")
                    {
                        return false;
                    }
                    else if (biometrictype == "Touch ID")
                    {
                        return true;
                    }
                    //context.LocalizedReason = "Authorize for access to secrets"; // iOS 11
                    // BiometryType = context.BiometryType == LABiometryType.TouchId ? "Touch ID" : "Face ID";
                    //buttonText = $"Login with {BiometryType}";
                }
                else
                {   // no FaceID before iOS 11
                    // buttonText = $"Login with Touch ID";
                }
            }
            return false;
        }



    }
}
